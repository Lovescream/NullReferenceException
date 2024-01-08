using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IWeapon
{
    void Attack();
    WeaponType WeponType { get; }
}
public class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected Sprite WeaponImage;

    public WeaponType WeponType => WeaponType.Hand;

    public Sprite Image()
    {
        return WeaponImage;
    }
}
