using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum HandType {
    MainHand,
    SubHand,
    Armor,
    COUNT,
}

public class CreatureInventory : Inventory {

    #region Properties

    public Creature Owner { get; protected set; }

    public Dictionary<HandType, Item> Equips { get; protected set; } = new();

    #endregion

    #region Fields

    public event Action<Item> OnEquipChanged;

    #endregion

    public CreatureInventory(Creature owner, int maxCount) : base(maxCount) {
        this.Owner = owner;
        for (int i = 0; i < (int)HandType.COUNT; i++) {
            Equips[(HandType)i] = null;
        }
    }

    public bool IsEquip(Item item) => Equips.ContainsValue(item);

    public bool Equip(Item item, bool change = true) {
        Item prevMain, prevSub;
        switch (item.EquipType) {
            case EquipType.OneHanded:
                prevMain = Equips[HandType.MainHand];
                if (prevMain != null) {
                    prevSub = Equips[HandType.SubHand];
                    if (prevSub != null && (!change || !UnEquip(prevSub))) return false;
                    Equips[HandType.SubHand] = item;
                }
                else {
                    Equips[HandType.MainHand] = item;
                }
                break;
            case EquipType.TwoHanded:
                prevMain = Equips[HandType.MainHand];
                prevSub = Equips[HandType.SubHand];
                if (prevSub != null && (!change || !UnEquip(prevSub))) return false;
                if (prevMain != null && (!change || !UnEquip(prevMain))) return false;
                Equips[HandType.MainHand] = item;
                break;

            case EquipType.Armor:
                Item prevArmor = Equips[HandType.Armor];
                if (prevArmor != null && (!change || !UnEquip(prevArmor))) return false;

                Equips[HandType.Armor] = item;
                break;
            default:
                return false;
        }
        Owner.Status.AddModifiers(item.Modifiers);
        OnEquipChanged?.Invoke(item);
        return true;
    }
    public bool UnEquip(Item item) {
        if (!Equips.ContainsValue(item)) return false;

        Equips[Equips.FirstOrDefault(x => x.Value.Equals(item)).Key] = null;
        Owner.Status.RemoveModifiers(item.Modifiers);
        OnEquipChanged?.Invoke(item);

        return true;
    }

    //public bool UnEquipWeapon(Item item) {
    //    Item prevMain = Equips[HandType.MainHand];
    //    if (!prevMain.Equals(item)) {
    //        Item prevSub = Equips[HandType.SubHand];
    //        if (!prevSub.Equals(item)) return false;
    //        Equips[HandType.SubHand] = null;
    //    }
    //    else {
    //        Equips[HandType.MainHand] = null;
    //    }
    //    Owner.Status.RemoveModifiers(item.Modifiers);
    //    OnEquipChanged?.Invoke(item);

    //    return true;
    //}
    //public bool UnEquipArmor(Item item) {
    //    Item prevArmor = Equips[HandType.Armor];
    //    if (!prevArmor.Equals(item)) return false;

    //    Equips[HandType.Armor] = null;
    //    Owner.Status.RemoveModifiers(item.Modifiers);
    //    OnEquipChanged?.Invoke(item);

    //    return true;
        
    //}

    //public bool CanEquip(Item item) {
    //    if (!_maxCounts.TryGetValue(item.Type, out int maxCount)) return false;
    //    if (!_equips.TryGetValue(item.Type, out List<Item> equips)) return false;
    //    if (equips.Count >= maxCount) return false;
    //    if (equips.Contains(item)) return false;
    //    return true;
    //}

    //public bool Equip(Item item, bool change = true) {
    //    if (_equips[item.Type].Count >= _maxCounts[item.Type]) {
    //        if (!change) return false;
    //        UnEquip(_equips[item.Type][0]);
    //    }

    //    _equips[item.Type].Add(item);

    //    Owner.Status.AddModifiers(item.Modifiers);

    //    OnEquipChanged?.Invoke(item);

    //    return true;
    //}
    //public bool UnEquip(Item item) {
    //    if (!_equips.TryGetValue(item.Type, out List<Item> equips)) return false;
    //    if (!equips.Remove(item)) return false;

    //    Owner.Status.RemoveModifiers(item.Modifiers);

    //    OnEquipChanged(item);

    //    return true;
    //}
}