using System;
using System.Collections.Generic;

[Serializable]
public class ItemData : Data {
    public ItemType Type { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Cost { get; set; }
    public List<StatModifier> Modifiers { get; set; }
}

public enum ItemType {
    Weapon,
    Armor,
    COUNT,
}