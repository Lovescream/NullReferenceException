using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearAssassinEnemy : EnemyBasicBT
{
    /*
    private SpriteRenderer _sprite;

    private float coolTime = 20f;

    public AssassinEnemyAI() : base()
    {
        this._detectDistance = 8;
        this._attackDistance = 1;
        this._movementSpeed = 3;
        this._isCoolTime = true;
    }

    protected override void Awake()
    {
        base.Awake();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected override INode SettingBT()
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
                            new SelectorNode
                            (
                                new List<INode> ()
                                {
                                    new ActionNode(DoAttack),
                                    new ActionNode(SpecialAttack)
                                }
                            )
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

    protected override INode.ENodeState DoAttack()
    {
        if (_detectedPlayer != null)
        {
            if (_isCoolTime)
            {
                return INode.ENodeState.ENS_Failure;
            }

            _animator.SetTrigger(_ATTACK_ANIM_TIRGGER_NAME);
            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Failure;
    }

    private INode.ENodeState SpecialAttack()
    {
        if (_isCoolTime && _detectedPlayer != null)
        {
            StartCoroutine(CoolTime());
            AssassinAttack();

            return INode.ENodeState.ENS_Success;
        }
        return INode.ENodeState.ENS_Failure;
    }

    private void AssassinAttack()
    {
        if (_detectedPlayer != null)
        {
            Vector3 playerBackPos = _detectedPlayer.position - transform.position;
            // �÷��̾ �����ʿ� ��ġ
            if (playerBackPos.x > 0)
            {
                transform.position = new Vector3(_detectedPlayer.position.x + 1f, _detectedPlayer.position.y, 0);
                _sprite.flipX = true;
            }
            else
            {
                transform.position = new Vector3(_detectedPlayer.position.x - 1f, _detectedPlayer.position.y, 0);
                _sprite.flipX = false;
            }

            _animator.SetTrigger(_ATTACK_ANIM_TIRGGER_NAME);
        }
    }

    private IEnumerator CoolTime()
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
    } */
}
