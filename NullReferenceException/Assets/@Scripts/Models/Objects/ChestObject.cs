using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestObject : Thing, IInteractable {

    public Chest Chest { get; private set; }

    public void Interact() {
        if (Chest == null) return;

        UI_GameScene sceneUI = ((Main.Scene.CurrentScene as GameScene).UI as UI_GameScene);
        if (sceneUI.Popup_ChestInventory != null) {
            sceneUI.Popup_ChestInventory.SetPopupToFront();
            sceneUI.Popup_ChestInventory.SetInfo(Chest.Inventory);
            return;
        }
        sceneUI.Popup_ChestInventory = Main.UI.ShowPopupUI<UI_Popup_ChestInventory>();
        sceneUI.Popup_ChestInventory.SetInfo(Chest.Inventory);
    }

    public void SetInfo(Chest chest) {
        Initialize();

        this.Chest = chest;
    }

}

public class Chest {

    public Inventory Inventory { get; private set; }

    public Chest(int maxCount, Item[] items = null) {
        Inventory = new(maxCount);
        if (items == null) return;
        foreach (Item item in items) {
            Inventory.TryAdd(item);
        }
        
    }

}