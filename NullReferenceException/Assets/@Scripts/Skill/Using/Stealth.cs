using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : SkillLogic
{
    protected override void UsingSkill(SkillData skill)
    {
        base.UsingSkill(skill);
        Debug.Log("���ڽ� ��ũ��Ʈ");
        StartMarskOnOff(9f + skill.SkillLv, 0);
    }
}
