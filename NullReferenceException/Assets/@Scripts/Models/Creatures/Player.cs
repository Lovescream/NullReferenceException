using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class Player : Creature {

    #region Input
    [SerializeField] private Transform _chracter;
    [SerializeField] private Transform _armPivot;
    [SerializeField] private Transform _weponRotate;
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
    }
    private void AttackCoolTime() //°ø°Ý µô·¹ÀÌ
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