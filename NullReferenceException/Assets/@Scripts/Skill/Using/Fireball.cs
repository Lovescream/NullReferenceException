using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : SkillLogic
{
    protected override void UsingSkill(SkillData skill)
    {
        if (player == null)
        {
            FindPlayer();
        }
        player.isFireball = true;
        dmg = 10 + (skill.SkillLv * 8);
        player.fireBallDmg = dmg;
    }
}
