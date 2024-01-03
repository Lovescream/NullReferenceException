using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private IWeapon _currentWeapon;
    private Animator _anim;

    private float _time;
    private float _coolTime = float.MaxValue;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
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
    public void ChangePlayerWeapon(IWeapon newWeapon)  //무기 변경
    {
        _currentWeapon = newWeapon;
        //여기에 무기별 딜레이 타임 전달
        //_time = newWeapon.coolTime;
    }
}
