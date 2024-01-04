using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ResultItemHolder : InventoryHolder
{
    [SerializeField] protected InventorySystem secondaryInventorySystem;

    public InventorySystem SecondaryInventorySystem => secondaryInventorySystem;

    public static UnityAction<InventorySystem> OnCraftringItemDisplayRequested;


    protected override void Awake()
    {
        base.Awake();

        secondaryInventorySystem = new InventorySystem(1);
    }

    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            OnCraftringItemDisplayRequested?.Invoke(secondaryInventorySystem);
        }
    }
}
