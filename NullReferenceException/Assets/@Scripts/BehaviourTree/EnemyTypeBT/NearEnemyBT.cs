using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearEnemyBT : EnemyBasicBT
{
    protected bool isSpecialAttacking = false;

    protected override void Awake()
    {
        base.Awake();
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
                            new ActionNode(CheckAttacking), // 공격중?
                            new InverterNode
                            (
                                new List<INode>()
                                {
                                    new SequenceNode
                                    (
                                        new List<INode>()
                                        {
                                            new ActionNode(CheckCoolTime),  // 쿨타임 체크
                                            new ActionNode(CheckSpecialAttackDistance), // 범위 안?
                                            new ActionNode(SpecialAttack) // 특수 공격

                                        }
                                    )
                                }
                            ),
                            new ActionNode(CheckEnemyWithineAttackRange), // 공격 범위 안?
                            new ActionNode(DoAttack) // 일반 공격
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

    #region SpecialAttack_Node
    private INode.ENodeState CheckCoolTime()
    {
        if (_isCoolTime)
        {
            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Failure;
    }

    private INode.ENodeState CheckSpecialAttackDistance()
    {
        if (_detectedPlayer != null)
        {
            if (Vector3.SqrMagnitude(_detectedPlayer.position - transform.position) < (_actionDistance * _actionDistance))
            {
                return INode.ENodeState.ENS_Success;
            }
        }
        return INode.ENodeState.ENS_Failure;
    }

    protected virtual INode.ENodeState SpecialAttack()
    {
        return INode.ENodeState.ENS_Failure;
    }
    #endregion

    #region Near_Attack Node

    protected override INode.ENodeState DoAttack()
    {
        if (_detectedPlayer != null)
        {
            if (_isCoolTime)
            {
                return INode.ENodeState.ENS_Failure;
            }

            Debug.Log("공격 시전");
            _animator.SetTrigger(_ATTACK_ANIM_TIRGGER_NAME);
            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Failure;
    }
    #endregion
}
