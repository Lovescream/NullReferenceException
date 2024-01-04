using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Creature {

    #region Properties

    public new PlayerData Data => base.Data as PlayerData;

    public float Hunger {
        get => _hunger;
        set {
            if (_hunger == value) return;
            if (value <= 0) {
                _hunger = 0;
                State.Current = CreatureState.Dead;
            }
            else if (value >= Status[StatType.HungerMax].Value) {
                _hunger = Status[StatType.HungerMax].Value;
            }
            else _hunger = value;
            OnChangedHunger?.Invoke(_hunger);
        }
    }

    #endregion

    #region Fields

    // State, Status.
    private float _hunger;

    // Callbacks.
    public event Action<float> OnChangedHunger;

    #endregion

    #region Input

    protected void OnMove(InputValue value) {
        Velocity = value.Get<Vector2>().normalized * Status[StatType.MoveSpeed].Value;
    }
    protected void OnLook(InputValue value) {
        LookDirection = (Camera.main.ScreenToWorldPoint(value.Get<Vector2>()) - this.transform.position).normalized;
    }
    protected void OnFire() {
        Projectile projectile = Main.Object.SpawnProjectile(this.transform.position).SetInfo(this);
        projectile.Velocity = LookDirection.normalized * 10f; // TODO::
    }
    protected void OnInteraction() {
        Debug.Log($"[Player] OnInteraction()");
    }
    protected void OnKey_Z() {
        Inventory.Add(new(Main.Data.Items["IronSword"]));
    }
    protected void OnKey_X() {
        Inventory.Add(new(Main.Data.Items["IronHammer"]));
    }
    protected void OnKey_C() {
        Inventory.Add(new(Main.Data.Items["IronHelmet"]));
    }
    protected void OnKey_V() {
        Inventory.Add(new(Main.Data.Items["IronBoots"]));
    }

    #endregion

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