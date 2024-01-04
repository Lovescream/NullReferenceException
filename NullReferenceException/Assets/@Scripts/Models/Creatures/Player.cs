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

    protected void OnKey_K(){
        if(Main.Instance.Skill.isSkillList == true)
        {
           Main.Instance.Skill.SkillList.SetActive(false);
           Main.Instance.Skill.isSkillList = !Main.Instance.Skill.isSkillList;
        }
        else
        {
            Main.Instance.Skill.UpdateSkillSlotUI();
            Main.Instance.Skill.SkillList.SetActive(true);
            Main.Instance.Skill.isSkillList = !Main.Instance.Skill.isSkillList;
        }
    }

    protected void OnQuick_Skill_Slot()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            Main.Instance.Skill.qSlot[0].GetComponent<QuickSlot>().UsingQuick();
            Debug.Log("F 키가 눌렸습니다.");
        }

        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            Main.Instance.Skill.qSlot[1].GetComponent<QuickSlot>().UsingQuick();
            Debug.Log("G 키가 눌렸습니다.");
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            Main.Instance.Skill.qSlot[2].GetComponent<QuickSlot>().UsingQuick();
            Debug.Log("R 키가 눌렸습니다.");
        }
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