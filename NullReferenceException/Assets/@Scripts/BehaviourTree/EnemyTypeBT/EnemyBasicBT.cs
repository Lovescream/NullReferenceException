using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicBT : MonoBehaviour
{
    #region Global Variable
    // Distance
    [Header("Distance")]
    [SerializeField]
    protected float _detectDistance;
    [SerializeField]
    protected float _attackDistance;
    [SerializeField]
    protected float _actionDistance;

    // Movement
    [Header("Move")]
    [SerializeField]
    protected float _movementSpeed;

    // CoolTime
    [Header("CoolTime")]
    protected bool _isCoolTime = true;
    [SerializeField]
    protected float coolTime = 0;

    // Components
    protected BehaviourTreeRunner _BTRunner = null;
    protected Rigidbody2D _rigid;
    protected Animator _animator = null;
    protected SpriteRenderer _sprite;
    protected RaycastHit2D[] hitData;

    // Position
    protected Transform _detectedPlayer = null;
    protected Vector3 _originPos = default;

    // Animations
    protected const string _ATTACK_ANIM_STATE_NAME = "Attack";
    protected const string _ATTACK_ANIM_TIRGGER_NAME = "IsAttack";
    protected const string _RUN_ANIM_STATE_NAME = "Run";
    protected const string _RUN_ANIM_BOOL_NAME = "IsRun";
    #endregion

    #region Init Method
    protected virtual void Awake()
    {
        _BTRunner = new BehaviourTreeRunner(SettingBT());
        _animator = GetComponentInChildren<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _originPos = transform.position;

        //InitStates();
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
                            new ActionNode(CheckAttacking), // 공격중?
                            new ActionNode(CheckEnemyWithineAttackRange), // 공격 범위 안?
                            new ActionNode(DoAttack) // 공격
                        }
                    ),
                    new SequenceNode
                    (
                        new List<INode>()
                        {
                            new ActionNode(CheckDetectEnemy), // 적 발견?
                            new ActionNode(MoveToDetectEnemy) // 적한테 이동
                        }
                    ),
                    new ActionNode(MoveToOriginPosition) // 원래 자리로
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
                Debug.Log("발동");
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
                    IdleAnimCheck();

                    return INode.ENodeState.ENS_Running;
                }

                RunAnimCheck();
                FlipSprite(transform.position, _detectedPlayer.position);
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
            IdleAnimCheck();
            return INode.ENodeState.ENS_Success;
        }
        else
        {
            RunAnimCheck();
            FlipSprite(transform.position, _originPos);
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

    protected void RunAnimCheck()
    {
        if (!_animator.GetBool(_RUN_ANIM_BOOL_NAME))
        {
            _animator.SetBool(_RUN_ANIM_BOOL_NAME, true);
        }
    }

    protected void IdleAnimCheck()
    {
        if (_animator.GetBool(_RUN_ANIM_BOOL_NAME))
        {
            _animator.SetBool(_RUN_ANIM_BOOL_NAME, false);
        }
    }

    protected void FlipSprite(Vector3 myPos, Vector3 targetPos)
    {
        Vector3 distance = (targetPos - myPos).normalized;

        _sprite.flipX = distance.x < 0f;
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
