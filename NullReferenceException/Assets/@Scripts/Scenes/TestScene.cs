using UnityEngine;

public class TestScene : BaseScene {

    public Player Player { get; private set; }

    protected override bool Initialize() {
        if (!base.Initialize()) return false;

        Player = Main.Object.SpawnPlayer("Player_00", Vector2.zero);
        Main.Object.SpawnEnemy("Enemy_00", new(4, 0));
        Main.Object.SpawnEnemy("Enemy_01", new(0, 4));
        Main.Object.SpawnEnemy("GoblinMech", new(4, 4));

        return true;
    }

}