using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class Player : Creature {

    #region Properties

    public new PlayerData Data => base.Data as PlayerData;
    public LevelUpSkill lvSkill;

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
    public float Exp {
        get { return _exp; }
        set { _exp = value; }
    }
    public float MaxExp {
        get { return _maxExp; }
        set { _maxExp = value; }
    }

    #endregion

    #region Fields
    // State, Status.
    private float _hunger;
    private float _exp = 0;
    private float _maxExp =100;

    // Callbacks.
    public event Action<float> OnChangedHunger;
    public event Action<float> OnChangedExp;

    #endregion

    #region Input
    [SerializeField] private Transform _chracter;
    [SerializeField] private Transform _armPivot;
    [SerializeField] private Transform _weponRotate;
    [SerializeField] private SpriteRenderer _wepon;
    [SerializeField] private SpriteRenderer _weponSprite;
    [SerializeField] private Transform _bullet;

    private float _time = 1;
    private float _coolTime = float.MaxValue;

    private void Update()
    {
        AttackCoolTime();
    }
    protected void OnMove(InputValue value) {
        Velocity = value.Get<Vector2>().normalized * Status[StatType.MoveSpeed].Value;
    }
    protected void OnLook(InputValue value) {
        LookDirection = (Camera.main.ScreenToWorldPoint(value.Get<Vector2>()) - this.transform.position).normalized;
        AimDirection();
    }
    protected void OnFire() {
        
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
    public void AimDirection()
    {
        float rotZ = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg;
        _armPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        _weponSprite.flipY = (Mathf.Abs(rotZ) > 90) ? true : false;
        _wepon.flipY = (Mathf.Abs(rotZ) > 90) ? true : false;
    }
    private void AttackCoolTime() //���� ������
    {
        if (_coolTime >= _time)
        {
            _coolTime = float.MaxValue;
        }
        else
        {
            _coolTime += Time.deltaTime;
        }
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
        }

        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            Main.Instance.Skill.qSlot[1].GetComponent<QuickSlot>().UsingQuick();
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            Main.Instance.Skill.qSlot[2].GetComponent<QuickSlot>().UsingQuick();
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

    public void AddExp(int exp)
    {
        _exp = _exp + exp;
        while(_exp >= _maxExp)
        {
            _exp -= _maxExp;
            _maxExp *= 1.5f;
            Data.Lv++;
            lvSkill.LvUpSkillEvent(Data.Lv);
        }
    }
}