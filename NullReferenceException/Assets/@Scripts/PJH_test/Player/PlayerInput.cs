using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerInput : MonoBehaviour
{
    private IWeapon _currentWeapon;
    private Animator _anim;

    [SerializeField] private Transform _chracter;
    [SerializeField] private SpriteRenderer _weponSprite;
    [SerializeField] private Transform _armPivot;

    private float _time;
    private float _coolTime = float.MaxValue;


    private Vector2 LookDirection;
    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _currentWeapon = GetComponent<Sword>();
    }
    private void Update()
    {
        AttackCoolTime();
    }

    public void OnFire()  //마우스 왼쪽 클릭 시 해당 무기의 Animation재생, 공격
    {
        if (_coolTime >= _time)
        {
            _anim.SetTrigger(_currentWeapon.WeponType.ToString());
            _currentWeapon.Attack();
        }
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
    public void OnMove(InputValue vector)
    {
        _anim.SetBool("Run", vector.Get<Vector2>().magnitude > .3);
    }
    public void ChangePlayerWeapon(IWeapon newWeapon, Sprite weaponImage)  //무기 변경
    {
        _currentWeapon = newWeapon;
        _weponSprite.sprite = weaponImage;
        //여기에 무기별 딜레이 타임 전달
        //_time = newWeapon.coolTime;
    }
    protected void OnLook(InputValue value)
    {
        LookDirection = (Camera.main.ScreenToWorldPoint(value.Get<Vector2>()) - this.transform.position).normalized;
        AimDirection();
    }

    public void AimDirection()
    {
        float rotZ = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg;
        _chracter.rotation = (Mathf.Abs(rotZ) > 90) ? Quaternion.Euler(0, 180f, 0) : Quaternion.Euler(0, 0, 0);
        _armPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        _weponSprite.flipY = (Mathf.Abs(rotZ) > 90) ? true : false;
        _anim.SetBool("FlipX", (Mathf.Abs(rotZ) > 90) ? true : false);
    }
}
