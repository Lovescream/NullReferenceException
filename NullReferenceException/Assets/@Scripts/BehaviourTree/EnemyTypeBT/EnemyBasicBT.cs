using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicBT : MonoBehaviour
{
    #region Global Variable
    // Distance
    protected float _detectDistance;
    protected float _attackDistance;
    protected float _actionDistance;

    // Movement
    protected float _movementSpeed;

    // CoolTime
    protected bool _isCoolTime = true;
    protected float coolTime = 0;

    // Components
    protected BehaviourTreeRunner _BTRunner = null;
    protected Rigidbody2D _rigid;
    protected Animator _animator = null;
    protected RaycastHit2D[] hitData;

    // Position
    protected Transform _detectedPlayer = null;
    protected Vector3 _originPos = default;

    // Animations
    protected const string _ATTACK_ANIM_STATE_NAME = "Attack";
    protected const string _ATTACK_ANIM_TIRGGER_NAME = "IsAttack";
    #endregion

    #region Init Method
    protected virtual void Awake()
    {
        _BTRunner = new BehaviourTreeRunner(SettingBT());
        _animator = GetComponentInChildren<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _originPos = transform.position;

        InitStates();
    }

    protected void Update()
    {
        _BTRunner.Operate();
    }

    private void InitStates()
    {
        this._detectDistance = 0;
        this._attackDistance = 0;
        this._movementSpeed = 0;
        this._actionDistance = 0;
        this.coolTime = 0f;
    }
    #endregion

    #region BT Setting
    protected virtual INode SettingBT()
    {
        return new SelectorNode
            (
                new List<INode>()
                {
                    new SequenceNode
                    (
                        new List<INode>()
                        {
                            new ActionNode(CheckAttacking), // ������?
                            new ActionNode(CheckEnemyWithineAttackRange), // ���� ���� ��?
                            new ActionNode(DoAttack) // ����
                        }
                    ),
                    new SequenceNode
                    (
                        new List<INode>()
                        {
                            new ActionNode(CheckDetectEnemy), // �� �߰�?
                            new ActionNode(MoveToDetectEnemy) // ������ �̵�
                        }
                    ),
                    new ActionNode(MoveToOriginPosition) // ���� �ڸ���
                }
            );
    }
    #endregion

    #region Attack Node
    protected virtual INode.ENodeState CheckAttacking()
    {
        if (IsAnimationRunning(_ATTACK_ANIM_STATE_NAME))
        {
            return INode.ENodeState.ENS_Running;
        }

        return INode.ENodeState.ENS_Success;
    }

    protected INode.ENodeState CheckEnemyWithineAttackRange()
    {
        if (_detectedPlayer != null)
        {
            if (Vector3.SqrMagnitude(_detectedPlayer.position - transform.position) < (_attackDistance * _attackDistance))
            {
                return INode.ENodeState.ENS_Success;
            }
        }

        return INode.ENodeState.ENS_Failure;
    }

    protected virtual INode.ENodeState DoAttack()
    {
        if (_detectedPlayer != null)
        {
            _animator.SetTrigger(_ATTACK_ANIM_TIRGGER_NAME);
            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Failure;
    }
    #endregion

    #region Detect & Move Node
    protected INode.ENodeState CheckDetectEnemy()
    {
        // var overlapColliders = Physics.OverlapSphere(transform.position, _detectDistance, LayerMask.GetMask("Player"));
        var overlapColliders = Physics2D.OverlapCircleAll(transform.position, _detectDistance, LayerMask.GetMask("Player"));

        if (overlapColliders != null && overlapColliders.Length > 0)
        {
            _detectedPlayer = overlapColliders[0].transform;

            return INode.ENodeState.ENS_Success;
        }

        _detectedPlayer = null;

        return INode.ENodeState.ENS_Failure;
    }

    protected virtual INode.ENodeState MoveToDetectEnemy()
    {
        if (_detectedPlayer != null)
        {
            CheckPlayerRay();
            if (hitData.Length > 1 && hitData[1].collider.CompareTag("Player"))
            {
                if (Vector3.SqrMagnitude(_detectedPlayer.position - transform.position) < (_attackDistance * _attackDistance))
                {
                    return INode.ENodeState.ENS_Running;
                }

                transform.position = Vector3.MoveTowards(transform.position, _detectedPlayer.position, Time.deltaTime * _movementSpeed);

                return INode.ENodeState.ENS_Running;
            }
        }

        return INode.ENodeState.ENS_Failure;
    }
    #endregion

    #region Move Origin Position & Patrol Node
    protected INode.ENodeState MoveToOriginPosition()
    {
        if (Vector3.SqrMagnitude(_originPos - transform.position) <= float.Epsilon * float.Epsilon)
        {
            return INode.ENodeState.ENS_Success;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _originPos, Time.deltaTime * _movementSpeed);
            return INode.ENodeState.ENS_Running;
        }
    }

    // Patrol Node Method

    #endregion

    #region Node Internal Functions
    protected bool IsAnimationRunning(string stateName)
    {
        if (_animator != null)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            {
                var normalizedTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

                return normalizedTime != 0 && normalizedTime < 1f;
            }
        }

        return false;
    }

    protected IEnumerator CoolTime()
    {
        _isCoolTime = false;

        WaitForFixedUpdate waitFrame = new WaitForFixedUpdate();

        while (coolTime > 0.1f)
        {
            coolTime -= Time.deltaTime;
            yield return waitFrame;
        }

        coolTime = 20f;
        _isCoolTime = true;
    }

    protected void CheckPlayerRay()
    {
        hitData = Physics2D.RaycastAll(_rigid.position, _detectedPlayer.position - transform.position, _detectDistance);
        Debug.DrawRay(transform.position, _detectedPlayer.position - transform.position, new Color(1, 0, 0));

    }
    #endregion

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, _detectDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, _attackDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, _actionDistance);
    }
}
