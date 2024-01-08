using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingInventory : Inventory {

    public InventorySlot ResultSlot { get; protected set; }

    public CraftingInventory(int count = 9) : base(count) {
        ResultSlot = new();
        ResultSlot.IsCraftingResultSlot = true;
        ResultSlot.OnGetCraftingResult += OnGetCraft;
        GetAllSlots().ForEach(x => x.OnChanged += slot => Craft());
    }
    
    private void Craft() {
        ResultSlot.Remove();

        string currentRecipe = string.Join('|', GetAllSlots().Select(x => x.Item == null ? "" : x.Item.Key));

        ItemData itemData = Main.Data.Items.Values.Where(x => x.Recipe != null && x.Recipe.Equals(currentRecipe)).FirstOrDefault();
        if (itemData == null) return;

        ResultSlot.SetItem(new(itemData));
    }
    private void OnGetCraft() {
        List<InventorySlot> slots = GetAllSlots();
        foreach (InventorySlot slot in slots) {
            slot.TryRemove(1);
        }
    }
}