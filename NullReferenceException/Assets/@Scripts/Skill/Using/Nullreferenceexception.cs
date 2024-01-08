using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nullreferenceexception : SkillLogic
{
    protected override void UsingSkill(SkillData skill)
    {
        base.UsingSkill(skill);
        Debug.Log("널 스크립트");
        StartMarskOnOff(10f + skill.SkillLv, 2);
    }
}
