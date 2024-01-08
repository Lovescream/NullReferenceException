using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    private WeaponType _type = WeaponType.Gun;
    public WeaponType WeponType => _type;

    public void Attack()
    {
        //발사체 발사 하기
        //발사체의 탄약을 확인하기
        //발사체의 정보를 받아와야함
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.GetComponent<Player>().ChangePlayerWeapon(this, this.GetComponent<SpriteRenderer>().sprite);
            Debug.Log("작동");
        }
    }
}
