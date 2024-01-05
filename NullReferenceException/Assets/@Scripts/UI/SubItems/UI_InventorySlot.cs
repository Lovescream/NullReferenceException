using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UI_InventorySlot : UI_Base, IPointerDownHandler {

    #region Enums

    enum Images {
        imgItem,
    }
    enum Texts {
        txtCount,
    }
    enum Objects {
        EquipMark,
    }

    #endregion

    #region Properties

    public Inventory Inventory { get; protected set; }
    public InventorySlot Slot { get; protected set; }
    public UI_CursorSlot CursorSlotUI => (Main.Scene.CurrentScene as GameScene).CursorSlotUI;

    #endregion

    #region MonoBehaviours

    protected override void OnDisable() {
        base.OnDisable();
        if (Slot != null) Slot.OnChanged -= Refresh;
    }
    protected override void OnDestroy() {
        base.OnDestroy();
        if (Slot != null) Slot.OnChanged -= Refresh;
    }
    public void OnPointerDown(PointerEventData eventData) {
        if (Inventory != null && this.Slot != null) OnClickSlot();
    }

    #endregion

    #region Initialize / Set

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));
        BindObject(typeof(Objects));

        return true;
    }

    public void SetInfo(Inventory inventory, InventorySlot slot) {
        Initialize();

        this.Inventory = inventory;
        this.Slot = slot;

        Refresh(Slot);
        if (slot != null) {
            slot.OnChanged -= Refresh;
            slot.OnChanged += Refresh;
        }
    }

    private void Refresh(InventorySlot slot) {
        if (Slot == null) return;

        if (Slot.IsEmpty()) {
            GetImage((int)Images.imgItem).sprite = null;
            GetObject((int)Objects.EquipMark).SetActive(false);
            GetText((int)Texts.txtCount).text = "";
        }
        else {
            GetImage((int)Images.imgItem).sprite = Main.Resource.LoadSprite($"{Slot.Item.Key}.sprite");
            GetObject((int)Objects.EquipMark).SetActive(Inventory is CreatureInventory c && c.IsEquip(Slot.Item));
            GetText((int)Texts.txtCount).text = $"{(Slot.Stack <= 1 ? "" : Slot.Stack)}";
        }
    }

    #endregion

    private void OnClickSlot() {
        bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed;

        if (!Slot.IsEmpty() && CursorSlotUI.CursorSlot.IsEmpty()) {
            if (isShiftPressed) {
                CursorSlotUI.SetNewSlot(Slot.PopItem(Slot.Item.Stack / 2));
            }
            else {
                CursorSlotUI.SelectSlot(this.Slot);
            }
        }
        else if (Slot.IsEmpty() && !CursorSlotUI.CursorSlot.IsEmpty()) {
            CursorSlotUI.PlaceToEmptySlot(this.Slot);
        }
        else if (!Slot.IsEmpty() && !CursorSlotUI.CursorSlot.IsEmpty()) {
            if (Slot.Item.Data == CursorSlotUI.CursorSlot.Item.Data) {
                CursorSlotUI.PlaceToSlot(this.Slot);
            }
            else {
                CursorSlotUI.SwapSlot(Slot);
            }
        }


        // Inventory Slot Click.
    }

    //private void OnClickItemSlot() {
    //    UI_Popup_ConfirmEquip popup = (Main.Scene.CurrentScene.UI as UI_GameScene).Popup_ConfirmEquip;
    //    if (popup == null) {
    //        popup = Main.UI.ShowPopupUI<UI_Popup_ConfirmEquip>();
    //        (Main.Scene.CurrentScene.UI as UI_GameScene).Popup_ConfirmEquip = popup;
    //    }
    //    popup.SetInfo(Inventory, Item);
    //    popup.SetPopupToFront();
    //}
}