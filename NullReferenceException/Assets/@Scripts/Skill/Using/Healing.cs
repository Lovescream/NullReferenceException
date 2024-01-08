using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : SkillLogic
{
    protected override void UsingSkill(SkillData skill)
    {
        base.UsingSkill(skill);
        FindPlayer();
        Debug.Log("힐링 스크립트");
        StartMarskOnOff(0.1f, 1);
        player.Hp += skill.SkillLv*8;
    }
}
