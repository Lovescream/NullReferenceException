using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : SkillLogic
<<<<<<< HEAD
{    
=======
{
    float skillDuration;
>>>>>>> parent of 74d4a14 (Revert "Merge branch 'Develop1.0' into PJH_Weapon")
    protected override void UsingSkill(SkillData skill)
    {
        skillDuration = 9f + skill.SkillLv;
        base.UsingSkill(skill);
        Debug.Log("���ڽ� ��ũ��Ʈ");
        StartMarskOnOff(skillDuration, 0);
        StartCoroutine(SkillEf(skillDuration));
    }

    protected override IEnumerator SkillEf(float duration)
    {
        if(player == null)
        {
            FindPlayer();
        }
        player.isStealth = true;
        Debug.Log("���� On");
        yield return base.SkillEf(duration);
        player.isStealth = false;
        Debug.Log("���� Off");
    }
}
