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
    [SerializeField]
    protected float _patrolDistance;

    // Movement
    [Header("Move")]
    [SerializeField]
    protected float _movementSpeed;

    // CoolTime
    [Header("Time")]
    [SerializeField]
    protected float coolTime = 0;
    protected bool _isCoolTime = true;
    protected float _originCoolTime = 0;
    [SerializeField]
    protected float patrolReadyTime;

    // Components
    protected BehaviourTreeRunner _BTRunner = null;
    protected Rigidbody2D _rigid;
    protected Animator _animator = null;
    protected SpriteRenderer _sprite;
    protected RaycastHit2D[] hitData;

    // Position
    protected Transform _detectedPlayer = null;
    protected Vector3 _originPos = default;
    protected Vector2 randomPatrolPos = default;
    protected bool _patrolMoveCheck = false;

    // Animations
    protected const string _ATTACK_ANIM_STATE_NAME = "Attack";
    protected const string _ATTACK_ANIM_Bool_NAME = "IsAttack";
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
        patrolReadyTime = Random.Range(2f, 5f);
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
                    new SelectorNode
                    (
                        new List<INode>()
                        {
                            //new ActionNode(MoveToOriginPosition), // ���� �ڸ���                         
                            new SequenceNode
                            (
                                new List<INode>()
                                {
                                    new ActionNode(RandomPatrolPositionCheck),
                                    new ActionNode(MoveToPatrolPosition)
                                }
                            )
                        }
                    )
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
            _animator.SetBool(_ATTACK_ANIM_Bool_NAME, true);
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

                _animator.SetBool(_ATTACK_ANIM_Bool_NAME, false);
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
    // ���ڸ��� �����ϴ� bool���� �����.
    // ���Ͱ� ���ڸ��� ���ƿ��� bool���� false�� �ȴ�. (false���� ������ ���ڸ��� ���ƿ��� �ʴ´�. Patrol���� �ٷ� �����Ѵ�.)
    // ���Ͱ� �÷��̾ ���� �ϸ� true�� �ٲ��. (true���´� �켱 ���ڸ��� ���ƿ´�)
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
    // ���� ����(0 ~ 360)���� Ray�� ���. (Ray�� ���̵� �����Ÿ�(������ ��ŭ�� ���))
    // Ray�� �ɸ��� ������Ʈ�� ���ٸ� �� �������� �����δ�. (�����̴� ���߿� �÷��̾ �����Ǹ� ���� �ؾ���)
    // Ray�� �ɸ��� ������Ʈ�� �ִٸ� �ٽ� �������� ������ ������ ���.
    // �� ������ �ݺ�.
    protected bool _patrolFirstCheck = true;    // �ӽ� ����... (�����丵 �ʼ�)(Patrol�κ� ��ü �� �ٽ� �����丵 �ؾ���.)
    // ���� ��ǥ�� �����ϰ� �� ��ǥ�� ���̸� ���� �´°� ������ Ȯ��
    protected INode.ENodeState RandomPatrolPositionCheck()
    {
        // ���� ��� ������ ����
        if(!_patrolMoveCheck)
        {
            randomPatrolPos = Random.insideUnitCircle * patrolReadyTime;
            hitData = Physics2D.RaycastAll(transform.position, randomPatrolPos, _patrolDistance);
            _patrolMoveCheck = true;
        }
        Debug.DrawRay(transform.position, randomPatrolPos, new Color(1, 0, 0));

        // ��λ� ��ü�� �ִ��� �˻�
        if (hitData.Length <= 1)
        {
            return INode.ENodeState.ENS_Success;
        }

        _patrolMoveCheck = false;
        return INode.ENodeState.ENS_Failure;
    }

    protected INode.ENodeState MoveToPatrolPosition()
    {
        _animator.SetBool(_ATTACK_ANIM_Bool_NAME, false); // ���� �ִϸ��̼��� ����ǰ� �־��ٸ� ����������.
        if (Vector3.SqrMagnitude((Vector3)randomPatrolPos - transform.position) <= float.Epsilon * float.Epsilon)
        {
            IdleAnimCheck();
            // ���ڸ� �ӹ����� �ð� �ڷ�ƾ
            // �ڷ�ƾ�� �ѹ��� �۵��ؾ� �ϱ⿡ �ӽ� �������� bool������ ���Ƶ�;(���߿� �����丵 �ʿ�)
            if (_patrolFirstCheck) StartCoroutine(PatrolReadTime());

            return INode.ENodeState.ENS_Failure;
        }
        else
        {
            RunAnimCheck();
            FlipSprite(transform.position, randomPatrolPos);
            transform.position = Vector3.MoveTowards(transform.position, randomPatrolPos, Time.deltaTime * _movementSpeed);
            return INode.ENodeState.ENS_Running;
        }
    }

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

        coolTime = _originCoolTime;
        _isCoolTime = true;
    }

    protected IEnumerator PatrolReadTime()
    {
        _patrolFirstCheck = false;
        WaitForFixedUpdate waitFrames = new WaitForFixedUpdate();

        while (patrolReadyTime > 0.1f)
        {
            patrolReadyTime -= Time.deltaTime;
            yield return waitFrames;
        }

        patrolReadyTime = Random.Range(2f, 5f);

        _patrolFirstCheck = true;
        _patrolMoveCheck = false;
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
