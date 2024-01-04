using System;
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

    public int Stack {
        get => _stack;
        set {
            if (value == _stack) return;
            if (value == 0) {
                _stack = 0;
                OnStackZero?.Invoke(this);
            }
            else {
                _stack = value;
                OnChangedStack?.Invoke(_stack);
            }
        }
    }
    public int RemainStack => Data == null ? -1 : Data.MaxStack - Stack;
    public List<StatModifier> Modifiers { get; protected set; }

    #endregion

    #region Fields

    private int _stack;

    public event Action<int> OnChangedStack;
    public event Action<Item> OnStackZero;

    #endregion

    public Item(ItemData data, Creature owner = null, int stack = 1) {
        this.Data = data;
        this.Owner = owner;

        Modifiers = Data.Modifiers.ConvertAll(x => x.Copy());
        Stack = stack;
    }

    public Item Copy(int stack = 0) => new(Data, Owner, stack == 0 ? Stack : stack);

    public int TryAddStack(int stack) {
        if (Stack + stack > MaxStack) {
            stack -= (MaxStack - Stack);
            Stack = MaxStack;
            return stack;
        }
        else {
            Stack += stack;
            return 0;
        }
    }
    public int TryRemoveStack(int stack) {
        if (Stack >= stack) {
            Stack -= stack;
            return 0;
        }
        else {
            stack -= Stack;
            Stack = 0;
            return stack;
        }
    }
}