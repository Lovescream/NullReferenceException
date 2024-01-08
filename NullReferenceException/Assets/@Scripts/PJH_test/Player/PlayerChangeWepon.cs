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
    //�κ��丮���� ������ �����͸� ����

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
        //Ÿ�Ժ� �ൿ�� Animation �۵�
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
        //������ �����Ϳ� ����ִ� Ÿ������ ���� �߰����ֱ�
        //������ 5�� ������, ���� 7�� ������
        if (collider != null)
        {
            Debug.Log("AttackType�۵�");
            if (collider.CompareTag("Enemy"))
            {
                //Player�� ���ݷ� �����Ϳ� �����ؼ� �� ��������
                //���� ü�� --
                Debug.Log("��");
            }
            else if (collider.CompareTag("Nature") && collider.isTrigger != true)//���� Ÿ���� Axe ���� �߰�, ���� Ÿ���� Pick ��� �߰�
            {
                //Ÿ���� ������� ��������
                collider.GetComponent<IHarvestable>().HPDecrease(_currentWeapon.WeponType);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckAttackType(collision);
    }
}
