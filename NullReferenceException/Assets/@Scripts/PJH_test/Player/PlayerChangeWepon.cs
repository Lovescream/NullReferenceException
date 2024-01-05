using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerChangeWepon : MonoBehaviour
{
    private IWeapon _currentWeapon;
    private Animator _animator;
    private static readonly int Bow = Animator.StringToHash("Bow");
    private static readonly int Hand = Animator.StringToHash("Hand");
    private static readonly int Axe = Animator.StringToHash("Axe");
    //������ �����͸� ����

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void Attack()
    {
        //Ÿ�Ժ� �ൿ�� Animation �۵�
        switch (_currentWeapon.WeponType) 
        {
            case WeaponType.Axe:
                _animator.SetTrigger(Axe);
                break;
            case WeaponType.Hand:
                _animator.SetTrigger(Hand);
                break;
            case WeaponType.Bow:
                _animator.SetTrigger(Hand);
                break;
        }
    }
    
    private void CheckAttackType(Collider2D collider) 
    {
        //������ �����Ϳ� ����ִ� Ÿ������ ���� �߰����ֱ�
        //������ 5�� ������, ���� 7�� ������
        if (collider != null)
        {
            if (collider.CompareTag("Enemy"))
            {
                //Player�� ���ݷ� �����Ϳ� �����ؼ� �� ��������
                //���� ü�� --
                Debug.Log("��");
            }
            else if (collider.CompareTag("Nature") && collider.isTrigger != true)//���� Ÿ���� Axe ���� �߰�, ���� Ÿ���� Pick ��� �߰�
            {
                //������ ü�� --
                collider.GetComponent<IHarvestable>().HPDecrease(WeaponType.Sword);
                collider.GetComponent<IHarvestable>().HPDecrease(WeaponType.Bow);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckAttackType(collision);
    }
}
