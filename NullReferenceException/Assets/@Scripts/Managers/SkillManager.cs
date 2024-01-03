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
        for (int i = 1; i < 5; i++)
        {
            string skillKey = "Skill_A_L_" + i;

            if (Main.Data.Skils.ContainsKey(skillKey))
            {
                Main.Data.PlayerSkils[skillKey] = Main.Data.Skils[skillKey];
                Debug.Log(Main.Data.PlayerSkils[skillKey]);
            }
            else
            {
                Debug.LogWarning($"Skill key not found: {skillKey}");
            }
        }
        InitializeSkillSlots();
    }

    private void InitializeSkillSlots()
    {
        for (int i = 0; i < skillSlots.Length; i++)
        {
            if (i < Main.Data.PlayerSkils.Count)
            {
                skillSlots[i].SetSkillData(Main.Data.PlayerSkils["Skill_A_L_" + i+1]);
            }
            else
            {
                skillSlots[i].SetSkillData(null);
            }
        }
    }
}
