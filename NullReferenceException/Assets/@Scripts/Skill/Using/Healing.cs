using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : SkillLogic
{
    protected override void UsingSkill(SkillData skill)
    {
        base.UsingSkill(skill);
        Debug.Log("���� ��ũ��Ʈ");
        StartMarskOnOff(0.1f, 1);
    }
}
