using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearTankerEnemy : NearEnemyBT
{
    private const string _CRASH_ANIM_TRIGGER_NAME = "IsCrash";

    protected override void Awake()
    {
        base.Awake();
        this._detectDistance = 4;
        this._attackDistance = 1;
        this._movementSpeed = 3;
        this._actionDistance = 3;
        this.coolTime = 20f;
    }

    #region override Node Method
    protected override INode.ENodeState CheckAttacking()
    {
        if (IsAnimationRunning(_ATTACK_ANIM_STATE_NAME) || isSpecialAttacking)
        {
            return INode.ENodeState.ENS_Running;
        }

        return INode.ENodeState.ENS_Success;
    }

    protected override INode.ENodeState SpecialAttack()
    {
        if (_isCoolTime && _detectedPlayer != null)
        {
            IdleAnimCheck();
            CheckPlayerRay();
            if (hitData.Length > 1 && hitData[1].collider.CompareTag("Player"))
            {
                StartCoroutine(CoolTime());
                StartCoroutine(CrashAttack());
            }

            return INode.ENodeState.ENS_Running;
        }
        return INode.ENodeState.ENS_Failure;
    }
    #endregion

    #region SpecialAttack_Logic
    private IEnumerator CrashAttack()
    {
        isSpecialAttacking = true;

        // Crash Ready
        _animator.SetTrigger(_CRASH_ANIM_TRIGGER_NAME);
        yield return new WaitForSeconds(1.5f);

        // 처음 IsCrash트리거가 발동될 때, 좌표를 기준으로 그쪽으로 돌진할지 - 1
        // 준비가 끝나고 나서 플레이어 좌표를 기준으로 돌진을 할지 - 2 (현재 방법)
        FlipSprite(transform.position, _detectedPlayer.position);
        _rigid.AddForce((_detectedPlayer.position - transform.position) * 5, ForceMode2D.Impulse);

        // Stun
        _animator.SetBool("IsStun", true);
        yield return new WaitForSeconds(3f);
        _animator.SetBool("IsStun", false);

        isSpecialAttacking = false;
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSpecialAttacking)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                _rigid.velocity = new Vector2(0, 0);
            }
            if (collision.gameObject.CompareTag("Player"))
            {
                _rigid.velocity = new Vector2(0, 0);
                // 데미지를 주는 로직
                // 넉백 적용
            }
        }
    }
}
