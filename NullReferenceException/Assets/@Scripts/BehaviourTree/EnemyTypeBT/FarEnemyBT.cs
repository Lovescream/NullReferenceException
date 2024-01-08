using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarEnemyBT : EnemyBasicBT
{
    protected override void Awake()
    {
        base.Awake();
    }

    #region BT Setting
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
                            new SequenceNode
                            (
                                new List<INode>()
                                {
                                    new ActionNode(CheckEnemyWithineAttackRange), // 공격 범위 안?
                                    new SequenceNode
                                    (
                                        new List<INode>()
                                        {
                                            new ActionNode(DeadZoneInsideCheck), // 데드존(actionDistance) 안?
                                            new RepeatNode
                                            (
                                                new List<INode>()
                                                {
                                                    // 도망(1 ~ 3초), 데드존 밖으로
                                                }
                                            )
                                        }
                                    )
                                }
                            ),
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
                            //new ActionNode(MoveToOriginPosition), // 원래 자리로                         
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

    #region Dead_Zone(ActionDistance) Check Node
    protected INode.ENodeState DeadZoneInsideCheck()
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

    protected INode.ENodeState EscapeOutSideZone()
    {
        return INode.ENodeState.ENS_Failure;
    }
    #endregion
}
