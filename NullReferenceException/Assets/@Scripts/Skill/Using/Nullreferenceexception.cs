using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nullreferenceexception : SkillLogic
<<<<<<< HEAD
{    protected override void UsingSkill(SkillData skill)
    {
        skillDuration = 3f + skill.SkillLv * 0.1f;
        base.UsingSkill(skill);
        Debug.Log("�� ��ũ��Ʈ");
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
        Debug.Log("���� On");
        yield return base.SkillEf(duration);
        player.Invincibility = false;
        Debug.Log("���� Off");
=======
{
    protected override void UsingSkill(SkillData skill)
    {
        base.UsingSkill(skill);
        Debug.Log("�� ��ũ��Ʈ");
        StartMarskOnOff(10f + skill.SkillLv, 2);
>>>>>>> parent of 74d4a14 (Revert "Merge branch 'Develop1.0' into PJH_Weapon")
    }
}
