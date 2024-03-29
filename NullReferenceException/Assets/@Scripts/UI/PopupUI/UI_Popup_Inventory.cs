using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_Inventory : UI_Popup {

    #region Enums

    enum Objects {
        Content,
    }
    enum Buttons {
        btnClose,
    }
    enum Texts {
        txtCount,
    }

    #endregion

    #region Fields

    private CreatureInventory _inventory;

    private readonly List<UI_InventorySlot> _slots = new();

    private Transform _content;

    #endregion

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
        BindText(typeof(Texts));

        _content = GetObject((int)Objects.Content).transform;
        _content.gameObject.DestroyChilds();

        GetButton((int)Buttons.btnClose).onClick.AddListener(OnBtnClose);

        return true;
    }

    public void SetInfo(CreatureInventory inventory) {
        Initialize();

        this._inventory = inventory;
        Refresh();

        if (_inventory != null) {
            _inventory.OnChanged -= Refresh;
            _inventory.OnChanged += Refresh;
        }
    }

    #endregion

    public void Refresh() {
        float ratio = _inventory.NotEmptyCount / (float)_inventory.Count;
        string color = ratio >= 0.75f ? (ratio >= 0.9f ? "red" : "yellow") : "white";
        GetText((int)Texts.txtCount).text = $"<color={color}>{_inventory.NotEmptyCount}</color> / {_inventory.Count}";

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
    }

    #region OnButtons

    private void OnBtnClose() {
        ClosePopup();
    }

    #endregion
}