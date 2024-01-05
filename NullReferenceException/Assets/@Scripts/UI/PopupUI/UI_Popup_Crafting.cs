using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_Crafting : UI_Popup {

    #region Enums

    enum Objects {
        CraftingSlots,
        ResultSlot,
    }
    enum Buttons {
        btnClose,
    }

    #endregion

    private CraftingInventory _inventory;

    private UI_InventorySlot _resultSlot;
    private readonly List<UI_InventorySlot> _slots = new();

    private Transform _content;

    #region MonoBehaviours

    protected override void OnDestroy() {
        base.OnDestroy();
        if (_inventory != null) _inventory.OnChanged -= Refresh;
    }
    protected override void OnDisable() {
        base.OnDisable();
        if (_inventory != null) _inventory.OnChanged -= Refresh;
    }

    #endregion

    #region Initialize / Set

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));

        _resultSlot = GetObject((int)Objects.ResultSlot).GetComponent<UI_InventorySlot>();
        _content = GetObject((int)Objects.CraftingSlots).transform;
        _content.gameObject.DestroyChilds();

        GetButton((int)Buttons.btnClose).onClick.AddListener(OnBtnClose);

        return true;
    }

    public void SetInfo() {
        Initialize();

        this._inventory = Main.Game.CraftingInventory;
        Refresh();

        if (_inventory != null) {
            _inventory.OnChanged -= Refresh;
            _inventory.OnChanged += Refresh;
        }
    }

    #endregion

    private void Refresh() {
        for (int i = 0; i < _inventory.Count; i++) {
            UI_InventorySlot slot;
            if (i >= _slots.Count) {
                slot = Main.UI.CreateSubItem<UI_InventorySlot>(_content);
                _slots.Add(slot);
            }
            else slot = _slots[i];
            slot.SetInfo(_inventory, _inventory[i]);
        }
        if (_slots.Count > _inventory.Count) {
            for (int i = _slots.Count - 1; i >= _inventory.Count; i--) {
                UI_InventorySlot slot = _slots[i];
                _slots.Remove(slot);
                Main.Resource.Destroy(slot.gameObject);
            }
        }

        _resultSlot.SetInfo(_inventory, _inventory.ResultSlot);
    }

    #region OnButtons

    private void OnBtnClose() {
        ClosePopup();
    }

    #endregion
}