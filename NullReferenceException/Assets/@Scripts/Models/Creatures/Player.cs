using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class Player : Creature {

    #region Input
    private IWeapon _currentWeapon;
    private Animator _anim;

    [SerializeField] private Transform _chracter;
    [SerializeField] private Transform _armPivot;
    [SerializeField] private Transform _weponRotate;
    [SerializeField] private SpriteRenderer _weponSprite;
    [SerializeField] private Transform _bullet;

    private float _time = 1;
    private float _coolTime = float.MaxValue;

    protected override void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _currentWeapon = GetComponent<Sword>();
    }
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
        if (_coolTime >= _time && _currentWeapon != null)
        {
            _coolTime = 0;
            _anim.SetTrigger(_currentWeapon.WeponType.ToString());
            _currentWeapon.Attack();
            if (_currentWeapon.WeponType == WeaponType.Bow)
            {
                Projectile projectile = Main.Object.SpawnProjectile(_bullet.position).SetInfo(this);
                projectile.Velocity = LookDirection.normalized * 10f; // TODO::
            }
        }
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
    public void ChangePlayerWeapon(IWeapon newWeapon, Sprite weaponImage)  //무기 변경
    {
        _currentWeapon = newWeapon;
        _weponSprite.sprite = weaponImage;
        //여기에 무기별 딜레이 타임 전달
        //_time = newWeapon.coolTime;
    }
    public void AimDirection()
    {
        float rotZ = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg;
        _armPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        _weponSprite.flipY = (Mathf.Abs(rotZ) > 90) ? true : false;
    }
    private void AttackCoolTime() //공격 딜레이
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
    #endregion
}