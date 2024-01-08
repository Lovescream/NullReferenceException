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
                            new ActionNode(CheckAttacking), // ������?
                            new SequenceNode
                            (
                                new List<INode>()
                                {
                                    new ActionNode(CheckEnemyWithineAttackRange), // ���� ���� ��?
                                    new SequenceNode
                                    (
                                        new List<INode>()
                                        {
                                            new ActionNode(DeadZoneInsideCheck), // ������(actionDistance) ��?
                                            new RepeatNode
                                            (
                                                new List<INode>()
                                                {
                                                    // ����(1 ~ 3��), ������ ������
                                                }
                                            )
                                        }
                                    )
                                }
                            ),
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
