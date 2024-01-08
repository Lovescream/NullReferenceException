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
    Gun,
}
public class BaseWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected Sprite WeaponImage;

    public WeaponType WeponType => WeaponType.Gun;

    public Sprite Image()
    {
        return WeaponImage;
    }
    public void Attack()
    {
        throw new System.NotImplementedException();
    }
}
