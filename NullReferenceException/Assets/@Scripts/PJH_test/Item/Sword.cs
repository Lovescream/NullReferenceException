using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void Attack();
    WeaponType WeponType { get; }
}
public enum WeaponType
{
    Sword,
    Bow,
}
public class Sword : MonoBehaviour, IWeapon
{
    private AudioSource _audioSource;
    [SerializeField]
    private Transform _pos;
    [SerializeField]
    private Vector2 _colSize;
    private WeaponType _type = WeaponType.Sword;
    private float _attackDamage;

    public WeaponType WeponType => _type;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(_pos.position, _colSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.CompareTag("Finish"))
            {
                //�� ü�� ���� �Լ� �ֱ�
                _audioSource.Play();
                Debug.Log("�� ü�� ����");
            }
            if (collider.GetComponent<IHarvestable>() != null)
            {
                collider.GetComponent<IHarvestable>().Harvest(_type);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_pos.position, _colSize);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().ChangePlayerWeapon(this, this.GetComponent<SpriteRenderer>().sprite);
            Debug.Log("�۵�");
        }
    }
}
