using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    private WeaponType _type = WeaponType.Gun;
    public WeaponType WeponType => _type;

    public void Attack()
    {
        //�߻�ü �߻� �ϱ�
        //�߻�ü�� ź���� Ȯ���ϱ�
        //�߻�ü�� ������ �޾ƿ;���
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.GetComponent<Player>().ChangePlayerWeapon(this, this.GetComponent<SpriteRenderer>().sprite);
            Debug.Log("�۵�");
        }
    }
}
