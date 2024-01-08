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
}