using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene {

    public Player Player { get; private set; }
    public UI_CursorSlot CursorSlotUI { get; set; }

    protected override bool Initialize() {
        if (!base.Initialize()) return false;

        Player = Main.Object.SpawnPlayer("Player_00", Vector2.zero);
        Main.Object.SpawnEnemy("Enemy_00", new(4, 0));
        Main.Object.SpawnEnemy("Enemy_01", new(0, 4));

        CursorSlotUI = Main.UI.ShowSceneUI<UI_CursorSlot>();
        UI = Main.UI.ShowSceneUI<UI_GameScene>();

        return true;
    }

}