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
        else Debug.LogWarning($"�κ��丮�� �Ҵ���� �ʾҽ��ϴ� {this.gameObject}");

        AssignSlot(inventorySystem);
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_Ui, InventorySlot>();

        if (slots.Length != inventorySystem.InventroySize) Debug.Log($"�κ��丮 ������ �鿣��� ����ȭ ���� �ʾѽ����. {this.gameObject}");

        for (int i = 0; i < inventorySystem.InventroySize; i++)
        {
            slotDictionary.Add(slots[i],inventorySystem.InventorySlots[i]);
            slots[i].Init(inventorySystem.InventorySlots[i]);
        }
    }

}
