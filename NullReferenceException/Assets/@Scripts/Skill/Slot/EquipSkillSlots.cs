using UnityEngine;
using UnityEngine.UI;

public class EquipSkillSlots : SkillSlot
{
    public int index;
    public GameObject marsk;
    public GameObject returnIndex;
    public bool isCool = false;

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
        returnIndex.GetComponent<SkillManager>().selectSolot = index;
    }
}
