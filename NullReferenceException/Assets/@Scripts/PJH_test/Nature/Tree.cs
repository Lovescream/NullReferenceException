using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : BaseNature, IHarvestable
{
    [SerializeField] private float _maxHealth = 5f;
    protected override void Awake()
    {
        base.Awake();
        Init(_maxHealth);
    }
    public void HPDecrease(WeaponType weaponType)
    {
        if (weaponType == WeaponType.Axe && weaponType == WeaponType.Hand)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
        {
            Sprite.color = ColorAlpha(0.5f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Sprite.color = ColorAlpha(1f);
    }
}
