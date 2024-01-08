using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nullreferenceexception : SkillLogic
{    protected override void UsingSkill(SkillData skill)
    {
        skillDuration = 3f + skill.SkillLv * 0.1f;
        base.UsingSkill(skill);
        Debug.Log("澄 胶农赋飘");
        StartMarskOnOff(10f + skill.SkillLv, 2);
        StartCoroutine(SkillEf(skillDuration));
    }

    protected override IEnumerator SkillEf(float duration)
    {
        if (player == null)
        {
            FindPlayer();
        }
        player.Invincibility = true;
        Debug.Log("公利 On");
        yield return base.SkillEf(duration);
        player.Invincibility = false;
        Debug.Log("公利 Off");
    }
}
