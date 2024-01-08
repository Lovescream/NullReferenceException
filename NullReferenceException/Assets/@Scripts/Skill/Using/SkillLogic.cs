using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLogic : MonoBehaviour
{
    public GameObject playerOBJ;
    public Player player;
    public Enemy enemy;

    protected float skillDuration;

    public float dmg = 0;

    protected void Start()
    {
        FindPlayer();
    }
    protected virtual void UsingSkill(SkillData skill)
    {
        string[] nameParts = skill.SkillName.Split('_');
        int skillLv = skill.SkillLv;
    }
    protected void MarskOn(int num)
    {
        //Main.Instance.Skill.screenMarsk[num].SetActive(true);
    }
    protected void MarskOff(int num)
    {
        //Main.Instance.Skill.screenMarsk[num].SetActive(false);
    }
    public void UsingSkills(SkillData skill)
    {
        UsingSkill(skill);
    }
    protected void FindPlayer()
    {
        playerOBJ = GameObject.FindWithTag("Player");
        if (playerOBJ != null)
        {
            player = playerOBJ.GetComponent<Player>();
        }
        else
        {
            Debug.LogError("Player 오브젝트를 찾을 수 없습니다.");
        }
    }
    public void StartMarskOnOff(float duration, int num)
    {
        StartCoroutine(MarskOnOff(duration, num));
    }
    protected IEnumerator MarskOnOff(float duration, int num)
    {
        MarskOn(num);
        yield return new WaitForSeconds(duration);
        MarskOff(num);
    }
    protected virtual IEnumerator SkillEf(float duration)
    {
        yield return new WaitForSeconds(duration);
    }
}
