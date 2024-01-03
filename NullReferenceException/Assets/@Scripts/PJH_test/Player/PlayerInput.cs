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

    public void OnFire()  //���콺 ���� Ŭ�� �� �ش� ������ Animation���, ����
    {
        if (_coolTime >= _time)
        {
            _anim.SetTrigger(_currentWeapon.WeponType.ToString());
            _currentWeapon.Attack();
        }
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
    public void ChangePlayerWeapon(IWeapon newWeapon)  //���� ����
    {
        _currentWeapon = newWeapon;
        //���⿡ ���⺰ ������ Ÿ�� ����
        //_time = newWeapon.coolTime;
    }
}
