using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : BaseNature, IHarvestable
{
    [SerializeField] private float _maxHealth = 6f;
    [SerializeField] private Animator _animator;
    private static readonly int Attcak = Animator.StringToHash("Attack");
    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        Init(_maxHealth);
    }
    public void HPDecrease(WeaponType weaponType)
    {
        if (weaponType == WeaponType.Axe || weaponType == WeaponType.Hand)
        {
            Health -= weaponType == WeaponType.Pick ? 2 : 1;
            Audio.time = 0.2f;
            Audio.Play();
            _animator.SetTrigger(Attcak);
            if (Health <= 0)
            {
                DropItem(new Vector3(0, 1, 0)); 
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
