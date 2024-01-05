using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSkill : MonoBehaviour
{
    public int[] getLock;
    public int[] skillLvUp;
    int j;
    void Start()
    {
        gameObject.GetComponent<Player>().lvSkill = this;
    }
    public void LvUpSkillEvent(int lv)
    {
        int listSize = getLock.Length;
        j = 1;
        for (int i = 0; i < listSize; i++)
        {
            if (Main.Data.PlayerSkils["Skill_A_L_" + j] != null)
            {
                if (Main.Object.Player.Data.Lv == getLock[i])
                {
                    Main.Data.PlayerSkils["Skill_A_L_" + j].SkillLv = 1;
                }
                else if (Main.Object.Player.Data.Lv> getLock[i] && Main.Object.Player.Data.Lv % skillLvUp[i] == 0)
                {
                    Main.Data.PlayerSkils["Skill_A_L_" + j].SkillLvup();
                }
            }
            else
            {
                Debug.Log("없는 스킬데이터에 접근하려하였습니다.");
            }
            j++;
        }
    }
}
