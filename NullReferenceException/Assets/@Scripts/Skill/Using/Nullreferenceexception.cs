using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nullreferenceexception : SkillLogic
<<<<<<< HEAD
{    protected override void UsingSkill(SkillData skill)
    {
        skillDuration = 3f + skill.SkillLv * 0.1f;
        base.UsingSkill(skill);
        Debug.Log("널 스크립트");
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
        Debug.Log("무적 On");
        yield return base.SkillEf(duration);
        player.Invincibility = false;
        Debug.Log("무적 Off");
=======
{
    protected override void UsingSkill(SkillData skill)
    {
        base.UsingSkill(skill);
        Debug.Log("널 스크립트");
        StartMarskOnOff(10f + skill.SkillLv, 2);
>>>>>>> parent of 74d4a14 (Revert "Merge branch 'Develop1.0' into PJH_Weapon")
    }
}
