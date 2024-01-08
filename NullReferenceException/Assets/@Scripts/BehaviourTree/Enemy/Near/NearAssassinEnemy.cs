using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearAssassinEnemy : NearEnemyBT
{
    protected override void Awake()
    {
        base.Awake();
        this._detectDistance = 5;
        this._attackDistance = 1;
        this._movementSpeed = 3;
        this._actionDistance = 4;
        this.coolTime = 20f;
        this._originCoolTime = this.coolTime;
    }

    #region Override Node Method
    protected override INode.ENodeState SpecialAttack()
    {
        if (_isCoolTime && _detectedPlayer != null)
        {
            StartCoroutine(CoolTime());
            AssassinAttack();

            return INode.ENodeState.ENS_Success;
        }
        return INode.ENodeState.ENS_Failure;
    }
    #endregion

    #region SpecialAttack_Logic
    private void AssassinAttack()
    {
        if (_detectedPlayer != null)
        {
            Vector3 playerBackPos = _detectedPlayer.position - transform.position;
            // 플레이어가 오른쪽에 위치
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

            _animator.SetBool(_ATTACK_ANIM_Bool_NAME, true);
        }
    }
    #endregion
}
