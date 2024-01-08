using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public int selectSolot = 0;
    public SkillSlotUI[] skillSlots;
    public GameObject SkillUI;
    public GameObject SkillList;
    public GameObject[] qSlot;
    public GameObject[] screenMarsk;

    public bool isSkillList = false;

    int maxLS = 4;
    int maxGS = 6;

    public void Awake()
    {
       // Main.Instance.Skill = gameObject.GetComponent<SkillManager>();
    }

    public void Start()
    {
        for (int i = 1; i < maxLS+1; i++)
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
        for (int i = 1; i < maxGS+1; i++)
        {
            string skillKey = "Skill_A_G_" + i;

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
        for (int i = 0; i < maxLS; i++)
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
        j = 1;
        for(int i = maxLS; i < maxLS + maxGS; i++)
        {
            if (i < Main.Data.PlayerSkils.Count)
            {
                skillSlots[i].SetSkillData(Main.Data.PlayerSkils["Skill_A_G_" + j]);
                Debug.Log(skillSlots[i].skillData.SkillName);
                j++;
            }
            else
            {
                Debug.Log("¼¼ÆÃÀÌ¾ÈµÊ");
                skillSlots[i].SetSkillData(null);
            }
        }
    }

    public void UpdateSkillSlotUI()
    {
        foreach (SkillSlotUI skillSlot in skillSlots)
        {
            skillSlot.UpdateUI();
        }
    }
}
