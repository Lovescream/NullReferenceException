using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sword : MonoBehaviour, IWeapon
{
    private AudioSource _audioClip;
    private WeaponType _type = WeaponType.Sword;

    public WeaponType WeponType => _type;

    public void Attack()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.GetComponent<Player>().ChangePlayerWeapon(this, this.GetComponent<SpriteRenderer>().sprite);
            Debug.Log("¿€µø");
        }
    }
}
