using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature {

    public new EnemyData Data => base.Data as EnemyData;
    public float Exp => Data.Exp;

    protected override void SetStateEvent() {
        base.SetStateEvent();
        State.AddOnStay(CreatureState.Idle, () => {
            Velocity = Vector2.zero;
        });
    }
    protected override void SetStatus(bool isFullHp = true) {
        this.Status = new(Data);
        if (isFullHp) {
            Hp = Status[StatType.HpMax].Value;
            ExistPower = Status[StatType.ExistPowerMax].Value;
        }

        OnChangedHp -= ShowHpBar;
        OnChangedHp += ShowHpBar;
    }

    public override void OnHit(Creature attacker, float damage = 0, KnockbackInfo knockbackInfo = default)
    {
        base.OnHit(attacker, damage, knockbackInfo);

        if (Hp > 0)
            _animator.SetTrigger("IsHit");

        if (Hp <= 0)
        {
            Velocity = Vector2.zero;
            _collider.enabled = false;
            _rigidbody.simulated = false;
            _animator.SetTrigger("IsDead");
            StartCoroutine(DeadTime());
            
        }
    }

    IEnumerator DeadTime()
    {
        var normalizedTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        //float time = 1f;

        yield return new WaitForSeconds(normalizedTime);

        gameObject.SetActive(false);
    }
}