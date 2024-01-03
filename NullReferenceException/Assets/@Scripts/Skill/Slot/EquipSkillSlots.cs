using UnityEngine;
using UnityEngine.UI;

public class EquipSkillSlots : SkillSlot
{
    public int index;
    public GameObject marsk;

    private void Update()
    {
        if (Main.Instance.Skill.selectSolot == index)
        {
            marsk.SetActive(true);
        }
        else
        {
            marsk.SetActive(false);
        }
    }

    protected override void OnClick()
    {
        Main.Instance.Skill.selectSolot = index;
    }
}
