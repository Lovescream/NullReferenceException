using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerChangeWepon : MonoBehaviour
{
    private IWeapon _currentWeapon;
    private Animator _animator;
    private static readonly int Gun = Animator.StringToHash("Gun");
    private static readonly int Hand = Animator.StringToHash("Hand");
    private static readonly int Axe = Animator.StringToHash("Axe");
    private static readonly int Pick = Animator.StringToHash("Pick");
    //인벤토리에서 무기의 데이터를 받음

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        //Test
        _currentWeapon = GetComponent<BaseWeapon>();
    }
    public void OnFire()
    {
        Attack();
    }
    public void Attack()
    {
        //타입별 행동할 Animation 작동
        switch (_currentWeapon.WeponType) 
        {
            case WeaponType.Axe:
                _animator.SetTrigger(Axe);
                break;
            case WeaponType.Hand:
                _animator.SetTrigger(Hand);
                break;
            case WeaponType.Gun:
                _animator.SetTrigger(Gun);
                break;
            case WeaponType.Pick:
                _animator.SetTrigger(Pick);
                break;
        }
    }
    
    private void CheckAttackType(Collider2D collider) 
    {
        //무기의 데이터에 들어있는 타입으로 조건 추가해주기
        //나무는 5번 때리기, 돌은 7번 때리기
        if (collider != null)
        {
            Debug.Log("AttackType작동");
            if (collider.CompareTag("Enemy"))
            {
                //Player의 공격력 데이터에 접근해서 값 가져오기
                //적의 체력 --
                Debug.Log("적");
            }
            else if (collider.CompareTag("Nature") && collider.isTrigger != true)//무기 타입은 Axe 도끼 추가, 무기 타입은 Pick 곡괭이 추가
            {
                //타입을 어떤식으로 가져올지
                collider.GetComponent<IHarvestable>().HPDecrease(_currentWeapon.WeponType);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckAttackType(collision);
    }
}
