using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager {

    public CraftingInventory CraftingInventory { get; private set; }

    public void Initialize() {
        CraftingInventory = new(9);
    }

}