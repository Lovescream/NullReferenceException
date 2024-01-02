using UnityEngine;

public class TestScene : BaseScene {

    public Player Player { get; private set; }

    protected override bool Initialize() {
        if (!base.Initialize()) return false;

        Player = Main.Object.SpawnPlayer("Character_00", Vector2.zero);
        Main.Object.SpawnEnemy("Character_01", new(4, 0));
        Main.Object.SpawnEnemy("Character_02", new(0, 4));

        return true;
    }

}