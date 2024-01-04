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
    //Data�� �޾� ü��, Drop������
    private float _woodHealth;

    [SerializeField] private GameObject _dropItem;

    private void Awake()
    {
        _woodSprite = GetComponent<SpriteRenderer>();
        _woodHealth = 10;//Data�� �ʱ�ȭ �����ֱ� 
    }
    private void DropItem()
    {
        if (_woodHealth <= 0)
        {
            //������ Drop �� ������Ʈ ��Ȱ��ȭ
            //������Ʈ�� �����ð� �Ŀ� �����??
        }
    }
    public void Harvest(WeaponType weaponType)
    {
        if (weaponType == WeaponType.Sword) //������ �ٲܿ���
        {
            Debug.Log("���� �۵�");
            _woodHealth--;
            DropItem();
        }
    }
}
