using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : BaseNature, IHarvestable
{
    [SerializeField] private float _maxHealth = 7f;
    protected override void Awake()
    {
        base.Awake();
        Init(_maxHealth);
    }
    public void HPDecrease(WeaponType weaponType)
    {
        if (weaponType == WeaponType.Pick && weaponType == WeaponType.Hand) //°î±ªÀÌ·Î ¹Ù²Ü¿¹Á¤ Pick
        {
            Health -= weaponType == WeaponType.Pick ? 2 : 1;
            AudioSource.Play(); 
            if (Health <= 0)
            {
                DropItem();
                StartCoroutine(RegrowTree(_maxHealth));
            }
        }
    }
}
