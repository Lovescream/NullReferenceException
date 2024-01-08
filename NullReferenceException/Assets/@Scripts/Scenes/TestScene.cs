using UnityEngine;

public class TestScene : BaseScene {

    public Player Player { get; private set; }

    public Enemy GoblinMech;
    public Enemy Adventurer;
    public Enemy GiantFly;
    public Enemy RatfolkAxe;

    protected override bool Initialize() {
        if (!base.Initialize()) return false;

        Player = Main.Object.SpawnPlayer("Player_00", Vector2.zero);
        Main.Object.SpawnEnemy("Enemy_00", new(4, 0));
        Main.Object.SpawnEnemy("Enemy_01", new(0, 4));
        
        Enemy newGoblinMech = Instantiate(GoblinMech);
        newGoblinMech.SetInfo(Main.Data.Enemies["GoblinMech"]);

        Enemy newAdventurer = Instantiate(Adventurer);
        newAdventurer.SetInfo(Main.Data.Enemies["Adventurer"]);

        Enemy newGiantFly = Instantiate(GiantFly);
        newGiantFly.SetInfo(Main.Data.Enemies["GiantFly"]);

        Enemy newRatfolkAxe = Instantiate(RatfolkAxe);
        newRatfolkAxe.SetInfo(Main.Data.Enemies["RatfolkAxe"]);

        return true;
    }

}