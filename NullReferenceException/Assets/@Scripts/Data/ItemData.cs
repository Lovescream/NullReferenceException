using System;
using System.Collections.Generic;

[Serializable]
public class ItemData : Data {
    public ItemType Type { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int MaxStack { get; set; }
    public string Recipe { get; set; }
    public EquipType EquipType { get; set; }
    public List<StatModifier> Modifiers { get; set; }
    public string Effect { get; set; }
}

public enum ItemType {
    Materials,
    Equipments,
    Consumables,
    Etc,
}
public enum EquipType {
    None,
    OneHanded,
    TwoHanded,
    Armor,
}