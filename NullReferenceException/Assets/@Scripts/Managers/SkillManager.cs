using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public int selectSolot = 0;
    public SkillSlotUI[] skillSlots = new SkillSlotUI[10];
    public GameObject SkillUI;
    public GameObject SkillList;

    public void Awake()
    {
        Main.Instance.Skill = gameObject.GetComponent<SkillManager>();
    }

    public void Start()
    {
        for (int i = 1; i < 5; i++)
        {
            string skillKey = "Skill_A_L_" + i;

            if (Main.Data.Skils.ContainsKey(skillKey))
            {
                Main.Data.PlayerSkils[skillKey] = Main.Data.Skils[skillKey];
                Debug.Log(Main.Data.PlayerSkils[skillKey].SkillName);
            }
            else
            {
                Debug.LogWarning($"Skill key not found: {skillKey}");
            }
        }
        SkillUI.SetActive(true);
        SkillList.SetActive(false);
        InitializeSkillSlots();
    }

    private void InitializeSkillSlots()
    {
        int j = 1;
        for (int i = 0; i < skillSlots.Length; i++)
        {
            if (i < Main.Data.PlayerSkils.Count)
            {
                skillSlots[i].SetSkillData(Main.Data.PlayerSkils["Skill_A_L_" + j]);
                Debug.Log(skillSlots[j].skillData.SkillName);
                j++;
            }
            else
            {
                Debug.Log("¼¼ÆÃÀÌ¾ÈµÊ");
                skillSlots[i].SetSkillData(null);
            }
        }
    }
}
