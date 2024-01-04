using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CragtingButton : MonoBehaviour
{
    [SerializeField] private Transform cragtingPanel;
    [SerializeField] private Transform ResultPanel;
    public items items;
    private List<InventorySlot_Ui> inventorySlots = new();
    private List<string> itemData = new List<string>();
    private string itemRecipe;
    InventorySlot_Ui[] transforms;
    private void Awake()
    {
        transform.gameObject.GetComponent<Button>().onClick.AddListener(PerformCrafting);
    }

    private void PerformCrafting()
    {
        transforms = cragtingPanel.GetComponentsInChildren<InventorySlot_Ui>();
        foreach (var item in transforms)
        {
            if (item.AssignedInventorySlot.ItemData !=null)
            {
                if (item.AssignedInventorySlot.StackSize < 2)
                {
                    itemData.Add($"{item.AssignedInventorySlot.ItemData.name}");
                }
                else
                {
                    itemData.Add($"{item.AssignedInventorySlot.ItemData.name}({item.AssignedInventorySlot.StackSize})");
                }
              
            }
            else
            {
                itemData.Add(" ");
            }
        }

        itemRecipe = string.Join(',', itemData);

        Debug.Log(itemRecipe);
        dd();
        foreach (var item in transforms)
        {
            item.ClearSlot();
        }
        itemData.Clear();
    }

    private void dd()
    {
        foreach (var item in items.inventoryItemDatas)
        {
            if (item.itemRecipe == itemRecipe)
            {
                InventorySlot inventorySlot = new InventorySlot();
                inventorySlot.UpdateInventorySlot(item, 1);
                ResultPanel.GetComponentInChildren<InventorySlot_Ui>().Init( inventorySlot);
            }
        }
    }
}
