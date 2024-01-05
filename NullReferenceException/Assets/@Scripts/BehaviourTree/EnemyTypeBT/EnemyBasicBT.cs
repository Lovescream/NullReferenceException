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
    [Header("CoolTime")]
    protected bool _isCoolTime = true;
    [SerializeField]
    protected float coolTime = 0;
    [SerializeField]
    protected float patrolReadyTime = 3f;

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
                    new SelectorNode
                    (
                        new List<INode>()
                        {
                            new ActionNode(MoveToOriginPosition), // 원래 자리로
                            /*
                            new SequenceNode
                            (
                                new List<INode>()
                                {
                                    new ActionNode(RandomPatrolPositionCheck),
                                    new ActionNode(MoveToPatrolPosition)
                                }
                            )
                            */
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
    // 제자리로 복귀하는 bool값을 만든다.
    // 몬스터가 제자리로 돌아오면 bool값은 false가 된다. (false상태 에서는 제자리로 돌아오지 않는다. Patrol노드로 바로 가게한다.)
    // 몬스터가 플레이어를 감지 하면 true로 바뀐다. (true상태는 우선 제자리로 돌아온다)
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
    // 랜덤 방향(0 ~ 360)으로 Ray를 쏜다. (Ray의 길이도 랜덤거리(움직일 만큼만 쏜다))
    // Ray에 걸리는 오브젝트가 없다면 그 방향으로 움직인다. (움직이는 도중에 플레이어가 감지되면 추적 해야함)
    // Ray에 걸리는 오브젝트가 있다면 다시 랜덤으로 방향을 돌려서 쏜다.
    // 위 내용을 반복.

    // 랜덤 좌표를 갱신하고 그 좌표로 레이를 쏴서 맞는게 없는지 확인

    /*
     * 
    protected INode.ENodeState RandomPatrolPositionCheck()
    {
        if(!_patrolMoveCheck)
        {
            Vector2 randomPos = Random.insideUnitCircle;
            randomPatrolPos = randomPos * _patrolDistance;
            hitData = Physics2D.RaycastAll(transform.position, randomPatrolPos, _patrolDistance);
            _patrolMoveCheck = true;
        }
        Debug.DrawRay(transform.position, randomPatrolPos, new Color(1, 0, 0));

        if (hitData.Length <= 1)
        {
            return INode.ENodeState.ENS_Success;
        }

        _patrolMoveCheck = false;
        return INode.ENodeState.ENS_Failure;
    }

    protected INode.ENodeState MoveToPatrolPosition()
    {
        if (_patrolMoveCheck)
        {
            if (Vector3.SqrMagnitude((Vector3)randomPatrolPos - transform.position) <= float.Epsilon * float.Epsilon)
            {
                IdleAnimCheck();
                StartCoroutine(PatrolReadTime());

                return INode.ENodeState.ENS_Failure;
            }

            RunAnimCheck();
            FlipSprite(transform.position, randomPatrolPos);
            transform.position = Vector3.MoveTowards(transform.position, randomPatrolPos, Time.deltaTime * _movementSpeed);
            return INode.ENodeState.ENS_Running;
        }

        return INode.ENodeState.ENS_Failure;
    }

    */

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

    protected IEnumerator PatrolReadTime()
    {
        WaitForFixedUpdate waitFrame = new WaitForFixedUpdate();

        while (patrolReadyTime > 0.1f)
        {
            Debug.Log(patrolReadyTime);
            patrolReadyTime -= Time.deltaTime;
            yield return waitFrame;
        }

        patrolReadyTime = 10f;

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
