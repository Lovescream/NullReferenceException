using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlot_Ui[] slots;

    protected override void Start()
    {
        base.Start();

        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.PrimaryInventorySystem;
            inventorySystem.OnIventorySlotChanged += UpdateSlot;
        }
        else Debug.LogWarning($"인벤토리에 할당되지 않았습니다 {this.gameObject}");

        AssignSlot(inventorySystem);
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_Ui, InventorySlot>();

        if (slots.Length != inventorySystem.InventroySize) Debug.Log($"인벤토리 슬롯이 백엔드와 동기화 되지 않앗스빈다. {this.gameObject}");

        for (int i = 0; i < inventorySystem.InventroySize; i++)
        {
            slotDictionary.Add(slots[i],inventorySystem.InventorySlots[i]);
            slots[i].Init(inventorySystem.InventorySlots[i]);
        }
    }

}
