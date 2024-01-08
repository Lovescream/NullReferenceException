using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UI_CursorSlot : UI_Scene {

    public InventorySlot SelectedSlot { get; private set; }
    public InventorySlot CursorSlot { get; private set; }

    public CreatureInventory PlayerInventory => (Main.Scene.CurrentScene as GameScene).Player.Inventory;

    private UI_InventorySlot _slotUI;

    protected override void SetOrder() {
        this.GetComponent<Canvas>().sortingOrder = 100;
    }

    #region MonoBehaviours

    void Update() {
        if (CursorSlot.IsEmpty()) return;

        _slotUI.transform.position = Mouse.current.position.ReadValue();
        if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject()) {
            ClearSlot();
        }
    }

    #endregion

    #region Initialize / Set

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        _slotUI = this.GetComponentInChildren<UI_InventorySlot>();
        _slotUI.SetInfo(null, CursorSlot);
        CursorSlot = new();
        _slotUI.gameObject.SetActive(!CursorSlot.IsEmpty());
        return true;
    }

    #endregion

    public void SelectSlot(InventorySlot slot) {
        Initialize();
        ClearSlot();
        SelectedSlot = slot;
        if (SelectedSlot.IsCraftingResultSlot) {
            CursorSlot.SetItem(SelectedSlot.Item);
            SelectedSlot.OnGetCraftingResult?.Invoke();
            _slotUI.SetInfo(null, CursorSlot);
            _slotUI.gameObject.SetActive(!CursorSlot.IsEmpty());
            return;
        }
        SelectedSlot.SwapSlot(CursorSlot);
        _slotUI.SetInfo(null, CursorSlot);
        _slotUI.gameObject.SetActive(!CursorSlot.IsEmpty());
    }
    public void SetNewSlot(Item item) {
        Initialize();
        ClearSlot();
        CursorSlot.SetItem(item);
        _slotUI.SetInfo(null, CursorSlot);
        _slotUI.gameObject.SetActive(!CursorSlot.IsEmpty());
    }

    public void PlaceToEmptySlot(InventorySlot slot) {
        Initialize();
        slot.SwapSlot(CursorSlot);
        _slotUI.SetInfo(null, CursorSlot);
        _slotUI.gameObject.SetActive(!CursorSlot.IsEmpty());
        SelectedSlot = null;
    }
    public void PlaceToSlot(InventorySlot slot) {
        Initialize();
        int remainStack = slot.TryAdd(CursorSlot.Item);
        if (remainStack > 0)
            CursorSlot.Item.Stack = remainStack;
        else {
            CursorSlot = new();
        }
        _slotUI.SetInfo(null, CursorSlot);
        _slotUI.gameObject.SetActive(!CursorSlot.IsEmpty());
        SelectedSlot = null;
    }

    public void SwapSlot(InventorySlot slot) {
        Initialize();
        slot.SwapSlot(CursorSlot);
        _slotUI.SetInfo(null, CursorSlot);
        _slotUI.gameObject.SetActive(!CursorSlot.IsEmpty());
        SelectedSlot = slot;
    }

    public void ClearSlot() {
        if (SelectedSlot != null) {
            SelectedSlot.SwapSlot(CursorSlot);
            SelectedSlot = null;
        }
        else {
            if (CursorSlot.IsEmpty()) return;
            PlayerInventory.TryAdd(CursorSlot.Item);
            CursorSlot = new();
            _slotUI.SetInfo(null, CursorSlot);
            _slotUI.gameObject.SetActive(!CursorSlot.IsEmpty());
        }
    }


    private bool IsPointerOverUIObject() {
        PointerEventData eventData = new(EventSystem.current);
        eventData.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}