using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay chestPanel;
    public DynamicInventoryDisplay PlayerBackpackPanel;
    public DynamicInventoryDisplay CraftingItemPanel;
    public DynamicInventoryDisplay ResultItemPanel;
    private void Awake()
    {
        chestPanel.gameObject.SetActive(false);
        PlayerBackpackPanel.gameObject.SetActive(false);
        CraftingItemPanel.transform.parent.parent.parent.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryHolder.OnDnamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlaerBackpackDisplayRequested += DisplayPlayerBackpack;
        CraftringItemHolder.OnCraftringItemDisplayRequested += DisplayCrafting;
        ResultItemHolder.OnCraftringItemDisplayRequested += DisplayResult;
    }

    private void OnDisable()
    {
        InventoryHolder.OnDnamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnPlaerBackpackDisplayRequested -= DisplayPlayerBackpack;
        CraftringItemHolder.OnCraftringItemDisplayRequested -= DisplayCrafting;
        ResultItemHolder.OnCraftringItemDisplayRequested -= DisplayResult;
    }

    private void Update()
    {
        if (chestPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            chestPanel.gameObject.SetActive(false);

        if (PlayerBackpackPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            PlayerBackpackPanel.gameObject.SetActive(false);

        if (CraftingItemPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            CraftingItemPanel.transform.parent.parent.parent.gameObject.SetActive(false);
    }

    private void DisplayInventory(InventorySystem invToDisplay)
    {
        chestPanel.gameObject.SetActive(true);
        chestPanel.RefreshDynamicInventory(invToDisplay);
    }

    private void DisplayPlayerBackpack(InventorySystem invToDisplay)
    {
        PlayerBackpackPanel.gameObject.SetActive(true);
        PlayerBackpackPanel.RefreshDynamicInventory(invToDisplay);
    }

    private void DisplayCrafting(InventorySystem invToDisplay)
    {
        CraftingItemPanel.transform.parent.parent.parent.gameObject.SetActive(true);
        CraftingItemPanel.RefreshDynamicInventory(invToDisplay);
    }

    private void DisplayResult(InventorySystem invToDisplay)
    {
        ResultItemPanel.RefreshDynamicInventory(invToDisplay);
    }
}
