using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    private AudioSource _audioSource;
    [SerializeField]
    private Transform _pos;

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
}
