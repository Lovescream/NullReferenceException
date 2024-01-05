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
    Axe,
    Pick,
    Hand,
    Sword,
    Bow,
}
public class BaseWeapon : MonoBehaviour
{
    
}
