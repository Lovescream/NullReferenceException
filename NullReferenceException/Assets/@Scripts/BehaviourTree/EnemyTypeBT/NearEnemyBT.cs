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
                            new ActionNode(CheckAttacking), // ������?
                            new InverterNode
                            (
                                new List<INode>()
                                {
                                    new SequenceNode
                                    (
                                        new List<INode>()
                                        {
                                            new ActionNode(CheckCoolTime),  // ��Ÿ�� üũ
                                            new ActionNode(CheckSpecialAttackDistance), // ���� ��?
                                            new ActionNode(SpecialAttack) // Ư�� ����

                                        }
                                    )
                                }
                            ),
                            new ActionNode(CheckEnemyWithineAttackRange), // ���� ���� ��?
                            new ActionNode(DoAttack) // �Ϲ� ����
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

            Debug.Log("���� ����");
            _animator.SetTrigger(_ATTACK_ANIM_TIRGGER_NAME);
            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Failure;
    }
    #endregion
}
