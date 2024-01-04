using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _projectilePrefab; //�߻�ü ����?

    private AudioSource _audioSource;
    private WeaponType _type = WeaponType.Bow;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public WeaponType WeponType => _type;

    public void Attack()
    {
        _audioSource.Play();
        //�߻�ü �߻� �ϱ�
        //�߻�ü�� ź���� Ȯ���ϱ�
        //�߻�ü�� ������ �޾ƿ;���
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
