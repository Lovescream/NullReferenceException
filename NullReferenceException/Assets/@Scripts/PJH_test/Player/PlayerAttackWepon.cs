using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackWepon : MonoBehaviour
{
    private WeaponType _currentWeapon;
    [SerializeField] private SpriteRenderer _currentSprite;
    [SerializeField] private Transform _bulletPosition;

    private Animator _animator;
    private static readonly int Gun = Animator.StringToHash("Gun");
    private static readonly int Hand = Animator.StringToHash("Hand");
    private static readonly int Axe = Animator.StringToHash("Axe");
    private static readonly int Pick = Animator.StringToHash("Pick");
    [SerializeField] private float _time = 1;
    private float _coolTime = float.MaxValue;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _currentWeapon = WeaponType.Hand;
    }
    public void OnFire()
    {
        if (_coolTime >= _time)
        {
            Attack();
            _coolTime = 0;
        }
    }
    private void Update()
    {
        AttackCoolTime();
    }
    public void ChangeWeapon(Item item)
    {
        _currentWeapon = item.WeaponType;//아이템 타입
        _time = item.Modifiers[1].Value; //아이템 공격속도
        _currentSprite.sprite = item.Sprite;
    }
    private void AttackCoolTime()
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
    public void Attack()
    {
        //타입별 행동할 Animation 작동
        switch (_currentWeapon) 
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
            case WeaponType.Sword:
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
            if (collider.CompareTag("Enemy"))
            {
                //Player의 공격력 데이터에 접근해서 값 가져오기
                //적의 체력 --
                Debug.Log("적체력 감소");
            }
            else if (collider.CompareTag("Nature") && collider.isTrigger != true)
            {
                collider.GetComponent<IHarvestable>().HPDecrease(_currentWeapon);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckAttackType(collision);
    }
}
