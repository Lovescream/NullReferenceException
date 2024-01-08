using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery : SkillLogic
{
    float dotHeal;
    protected override void UsingSkill(SkillData skill)
    {
        base.UsingSkill(skill);
        skillDuration = 3f + skill.SkillLv;
        dotHeal = skill.SkillLv * 2;
        Debug.Log("리커버리");
        StartCoroutine(SkillEf(skillDuration));
    }

    protected override IEnumerator SkillEf(float duration)
    {
        while (true)  // 무한 반복
        {
            if (player == null)
            {
                FindPlayer();
            }

            player.Hp += dotHeal;
            Debug.Log("도트힐");
            StartMarskOnOff(0.1f, 1);
            yield return new WaitForSeconds(1f);
            duration -= 1f;

            if (duration <= 0f)
            {
                break;
            }
        }
        yield return base.SkillEf(duration);
    }
}
