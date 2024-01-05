using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingInventory : Inventory {

    public InventorySlot ResultSlot { get; protected set; }

    public CraftingInventory(int count = 9) : base(count) {
        ResultSlot = new();
    }
    

}