using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerChangeWepon : MonoBehaviour
{
    //������ �����͸� ����
    [SerializeField] private Collider2D collider2D;

    public void CheckAttackType(Collider2D collider) 
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
            else if (collider.CompareTag("Nature"))//���� Ÿ���� Axe ���� �߰�, ���� Ÿ���� Pick ��� �߰�
            {
                //������ ü�� --
                collider.GetComponent<IHarvestable>().HPDecrease(WeaponType.Sword);
                collider.GetComponent<IHarvestable>().HPDecrease(WeaponType.Bow);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collider2D.enabled == true)
        {
            CheckAttackType(collision);
        }
    }
}
