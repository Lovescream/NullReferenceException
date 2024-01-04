using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot {

    #region Properties

    public Item Item {
        get => _item;
        set {
            if (value == _item) return;
            if (value == null) {
                _item = null;
            }
            else {
                _item = value;
            }
            OnChanged?.Invoke(this);
        }
    }
    public int Stack => Item == null ? -1 : Item.Stack;

    #endregion

    #region Fields

    private Item _item;

    // Callbacks.
    public event Action<InventorySlot> OnChanged;

    #endregion

    public bool IsEmpty() => Item == null;
    public bool IsItem(ItemData data) => Item != null && Item.Data == data;
    public bool IsItem(Item item) => Item == item;

    public bool SetItem(Item item) {
        if (Item != null) return false;
        Item = item;

        Item.OnChangedStack += OnChangedStack;
        Item.OnStackZero += OnStackZero;

        return true;
    }

    public int TryAdd(int stack = 1) {
        if (this.Item == null) return -1;
        return this.Item.TryAddStack(stack);
    }
    public int TryAdd(ItemData data, int stack = 1) {
        if (this.Item == null) {
            if (stack > data.MaxStack) {
                SetItem(new(data, stack: data.MaxStack));
                return stack - data.MaxStack;
            }
            else {
                SetItem(new(data, stack: stack));
                return 0;
            }
        }
        else if (this.Item.Data != data) return -1;
        else return TryAdd(stack);
    }
    public int TryAdd(Item item) {
        if (this.Item == null) {
            SetItem(item);
            return 0;
        }
        else if (this.Item.Data != item.Data) return -1;
        else return this.Item.TryAddStack(item.Stack);
    }

    public int TryRemove(int stack = 1) {
        if (this.Item == null) return -1;
        return this.Item.TryRemoveStack(stack);
    }
    public int TryRemove(ItemData data, int stack = 1) {
        if (this.Item == null || this.Item.Data != data) return -1;
        return this.Item.TryRemoveStack(stack);
    }
    public int TryRemove(Item item, int stack = 1) {
        if (this.Item != item) return -1;
        return this.Item.TryRemoveStack(stack);
    }

    public Item PopItem(int stack = 1) {
        if (this.Item == null || this.Stack < stack) return null;
        return Item.Copy(stack);
    }

    #region Callbacks.

    private void OnChangedStack(int stack) {
        OnChanged?.Invoke(this);
    }
    private void OnStackZero(Item item) {
        item.OnChangedStack -= OnChangedStack;
        item.OnStackZero -= OnStackZero;
        Item = null;
    }

    #endregion
}