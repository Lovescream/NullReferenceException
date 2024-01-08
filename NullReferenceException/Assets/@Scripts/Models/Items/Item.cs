using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    #region Properties

    public ItemData Data { get; protected set; }
    public Creature Owner { get; protected set; }

    public string Key => Data.Key;
    public ItemType Type => Data.Type;
    public string Name => Data.Name;
    public string Description => Data.Description;
    public int MaxStack => Data.MaxStack;
    public string Recipe => Data.Recipe;
    public EquipType EquipType => Data.EquipType;
    public string Effect => Data.Effect;
    public WeaponType WeaponType { get; set; }
    public Sprite Sprite { get; set; }
    public List<StatModifier> Modifiers { get; protected set; }

    #endregion

    public Item(ItemData data, Creature owner = null) {
        this.Data = data;
        this.Owner = owner;

        Modifiers = Data.Modifiers.ConvertAll(x => x.Copy());
    }
}