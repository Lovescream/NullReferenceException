using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nullreferenceexception : SkillLogic
{
    protected override void UsingSkill(SkillData skill)
    {
        base.UsingSkill(skill);
        Debug.Log("�� ��ũ��Ʈ");
        StartMarskOnOff(10f + skill.SkillLv, 2);
    }
}
