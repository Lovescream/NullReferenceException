using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHarvestable
{
    void Harvest(WeaponType weaponType);
}
public class Wood : MonoBehaviour, IHarvestable
{
    private SpriteRenderer _woodSprite;
    //Data를 받아 체력, Drop아이템
    private float _woodHealth;

    [SerializeField] private GameObject _dropItem;

    private void Awake()
    {
        _woodSprite = GetComponent<SpriteRenderer>();
        _woodHealth = 10;//Data로 초기화 시켜주기 
    }
    private void DropItem()
    {
        if (_woodHealth <= 0)
        {
            //아이템 Drop 및 오브젝트 비활성화
            //오브젝트는 일정시간 후에 재생성??
        }
    }
    public void Harvest(WeaponType weaponType)
    {
        if (weaponType == WeaponType.Sword) //도끼로 바꿀예정
        {
            Debug.Log("나무 작동");
            _woodHealth--;
            DropItem();
        }
    }
}
