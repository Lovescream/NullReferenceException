using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : SkillLogic
{
    protected override void UsingSkill(SkillData skill)
    {
        Main.Object.Player.isFireball = true;
    }
}
