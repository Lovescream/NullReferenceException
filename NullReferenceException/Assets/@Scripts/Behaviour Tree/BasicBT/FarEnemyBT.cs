using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarEnemyBT : EnemyBasicBT
{
    protected override void Awake()
    {
        base.Awake();
    }

    /*
    protected float _escapeTime = 30;
    protected bool _escapeRunCheck = false;

    Vector3 _escpaePos = Vector3.zero;

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
                            new ActionNode(CheckEnemyWithineAttackRange), // ���� ���� ��?
                            new InverterNode
                            (
                                new List<INode>()
                                {
                                    new SequenceNode
                                    (
                                        new List<INode>()
                                        {
                                            new ActionNode(DeadZoneInsideCheck), // ������(actionDistance) ��?
                                            new ActionNode(EscapeOutSideZone) // ����(1 ~ 3��), ������ ������
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
                if (!_escapeRunCheck)
                {
                    EscapeDistancePosition();
                    StartCoroutine(EscapeTimeDecrease());

                    hitData = Physics2D.RaycastAll(transform.position, _escpaePos, 1);
                }

                return INode.ENodeState.ENS_Success;
            }

            if (_escapeRunCheck)
                StopCoroutine(EscapeTimeDecrease());

            return INode.ENodeState.ENS_Failure;
        }

        return INode.ENodeState.ENS_Failure;
    }

    protected INode.ENodeState EscapeOutSideZone()
    {
        if (_detectedPlayer != null )
        {
            Debug.Log(_escapeRunCheck);
            Debug.DrawRay(transform.position, _escpaePos, new Color(0, 1, 0));
            // Debug.Log(hitData[1]);
            // �� hitData[1]�� RayCast�� ��������? �ƹ��͵� ����� �ϴµ�?
            if (hitData.Length <= 1 && _escapeRunCheck)
            {
                _animator.SetBool(_ATTACK_ANIM_Bool_NAME, false);
                Debug.Log("����");
                RunAnimCheck();
                transform.position = Vector3.MoveTowards(transform.position, _escpaePos, Time.deltaTime * _movementSpeed);
                return INode.ENodeState.ENS_Running;
            }

            IdleAnimCheck();
            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Failure;
    }
    #endregion

    #region Escape Logic
    protected void EscapeDistancePosition()
    {
        _escpaePos = (_detectedPlayer.position - transform.position).normalized;
        _escpaePos *= -1;
    }

    protected IEnumerator EscapeTimeDecrease()
    {
        _escapeRunCheck = true;
 
        WaitForFixedUpdate waitFrames = new WaitForFixedUpdate();

        while (_escapeTime > 0.1f)
        {
            _escapeTime -= Time.deltaTime;
            yield return waitFrames;
        }

        _escapeRunCheck = false;
    }
    #endregion
    */
}
