using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerChangeWepon : MonoBehaviour
{
    //무기의 데이터를 받음
    [SerializeField] private Collider2D collider2D;

    public void CheckAttackType(Collider2D collider) 
    {
        //무기의 데이터에 들어있는 타입으로 조건 추가해주기
        //나무는 5번 때리기, 돌은 7번 때리기
        if (collider != null)
        {
            if (collider.CompareTag("Enemy"))
            {
                //Player의 공격력 데이터에 접근해서 값 가져오기
                //적의 체력 --
                Debug.Log("적");
            }
            else if (collider.CompareTag("Nature"))//무기 타입은 Axe 도끼 추가, 무기 타입은 Pick 곡괭이 추가
            {
                //나무의 체력 --
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
