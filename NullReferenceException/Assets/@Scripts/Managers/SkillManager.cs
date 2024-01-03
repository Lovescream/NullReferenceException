using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public int selectSolot = 0;
    public SkillSlotUI[] skillSlots;

    public void Awake()
    {
        for (int i = 0; i < skillSlots.Length; i++)
        {
            skillSlots[i] = GameObject.Find("Slot" + i).GetComponent<SkillSlotUI>();
        }
    }

    public void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Main.Data.PlayerSkils["Skill_A_L_"+i] = Main.Data.Skils["Skill_A_L_" + i];
        }
        InitializeSkillSlots();
    }

    private void InitializeSkillSlots()
    {
        for (int i = 0; i < skillSlots.Length; i++)
        {
            if (i < Main.Data.PlayerSkils.Count)
            {
                skillSlots[i].SetSkillData(Main.Data.PlayerSkils["Skill_A_L_" + i]);
            }
            else
            {
                skillSlots[i].SetSkillData(null);
            }
        }
    }
}
