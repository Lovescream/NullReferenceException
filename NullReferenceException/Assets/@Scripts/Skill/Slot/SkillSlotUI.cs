using UnityEngine;
using TMPro;

public class SkillSlotUI : SkillSlot
{
    public TMP_Text skillNameText;
    public TMP_Text skillCollTxt;

    bool skillLock = true;

    public EquipSkillSlots[] equipSkillSlots;

    protected override void SetDefaultSkillData(SkillData data)
    {
        base.SetDefaultSkillData(data);
        UpdateUI();
    }
    protected override void OnClick()
    {

        int selectedSlotIndex = Main.Instance.Skill.selectSolot;
        if (skillData.SkillLv == 0 || string.IsNullOrEmpty(skillData.Key))
        {
            Debug.Log("�� ���� Ȥ�� ��ų�� �رݵ��� ���� �����Դϴ�.");
        }
        else
        {
            if (equipSkillSlots[selectedSlotIndex].skillData == null ||
                (equipSkillSlots[0].skillData.Key != skillData.Key &&
                 equipSkillSlots[1].skillData.Key != skillData.Key &&
                 equipSkillSlots[2].skillData.Key != skillData.Key))
            {
                equipSkillSlots[selectedSlotIndex].SetSkillData(skillData);
            }
            else if (equipSkillSlots[selectedSlotIndex].skillData.Key == skillData.Key)
            {
                Debug.Log("������ ��ų ������");
            }
            else
            {
                Debug.Log("�ٸ� ���Կ��� �̹� ������ ��ų �����͸� ��� ���Դϴ�.");
            }
        }


    }

    public void UpdateUI()
    {
        if (skillData.Key != "")
        {
            skillNameText.text = "Name: " + skillData.SkillName + " Lv." + skillData.SkillLv;
            skillCollTxt.text = "Coll: " + skillData.CoolTime;
        }
        else
        {
            skillNameText.text = "Name: -";
            skillCollTxt.text = "Coll: -";
        }     
    }
}
