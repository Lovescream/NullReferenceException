using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulAbsorption: SkillLogic
{
    GameObject area;
    protected override void UsingSkill(SkillData skill)
    {
        FindPlayer();
        base.UsingSkill(skill);
        area = playerOBJ.GetComponent<SplashArea>().areaFieldOBJ[0];
        area.GetComponent<SoulAbsorptionArea>().dmg = skill.SkillLv * 4;
        skillDuration = 10f;
        StartCoroutine(SkillEf(skillDuration));
    }

    protected override IEnumerator SkillEf(float duration)
    {
        area.SetActive(true);
        yield return base.SkillEf(duration);
        area.SetActive(false);
    }
}
