using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
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
        //발사체 발사 하기
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerInput>().ChangePlayerWeapon(this, this.GetComponent<SpriteRenderer>().sprite);
            Debug.Log("작동");
        }
    }
}
