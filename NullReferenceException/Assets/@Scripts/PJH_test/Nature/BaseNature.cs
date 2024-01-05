using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHarvestable
{
    void HPDecrease(WeaponType weaponType);
}
public class BaseNature : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private Collider2D _collider;
    private AudioSource _audioSource;
    public float Health { get; set; }
    public SpriteRenderer Sprite { get => _sprite;}
    public AudioSource AudioSource { get => _audioSource; }

    [SerializeField] private GameObject _dropItem;

    protected virtual void Awake()
    {
        _sprite = this.GetComponent<SpriteRenderer>();
        _collider = this.GetComponent<Collider2D>();
        _audioSource = this.GetComponent<AudioSource>();
    }
    protected void Init(float hp)
    {
        Health = hp;
        _sprite.enabled = true;
        _collider.enabled = true;
    }
    protected void DropItem()
    {
        Instantiate(_dropItem, transform.position, Quaternion.identity);
        _sprite.enabled = false;
        _collider.enabled = false;
    }
    protected Color ColorAlpha(float colorValue)
    {
        Color col = Sprite.color;
        col.a = colorValue;
        return col;
    }
    protected IEnumerator RegrowTree(float health)
    {
        //랜덤한 시간부여?
        yield return new WaitForSecondsRealtime(4f);
        Debug.Log("재생성");
        Init(health);
    }
}
