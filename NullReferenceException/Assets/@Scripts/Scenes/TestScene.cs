using UnityEngine;

public class TestScene : BaseScene {

    public Player Player { get; private set; }

    // Ex
    public Enemy EnemyPrefab;

    protected override bool Initialize() {
        if (!base.Initialize()) return false;

        Player = Main.Object.SpawnPlayer("Player_00", Vector2.zero);
        Main.Object.SpawnEnemy("Enemy_00", new(4, 0));
        Main.Object.SpawnEnemy("Enemy_01", new(0, 4));

        // Ex
        Enemy newEnemy = Instantiate(EnemyPrefab);
        newEnemy.SetInfo(Main.Data.Enemies["Enemy_00"]);

        return true;
    }

}