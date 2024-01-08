using UnityEngine;
using TMPro;

public class SkillSlotUI : SkillSlot
{
    public TMP_Text skillNameText;
    public TMP_Text skillCollTxt;
    public TMP_Text skillExpTxt;

    public GameObject marsk;

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
        if (skillExpTxt != null && skillData.Key != null)
        {
            string[] nameParts = skillData.SkillName.Split('_');
            string[] keyParts = skillData.Key.Split('_');
            skillNameText.text = "Name: " + nameParts[0] + " Lv." + skillData.SkillLv;
            skillCollTxt.text = "Coll: " + skillData.CoolTime.ToString("F2");
            if (skillData.SkillGetType == SkillGetType.Pickup)
            {
                skillExpTxt.text = $"Exp :{skillData.SkillExp}/100 ({skillData.SkillExp % 100}%)";
            }
            else
            {
               // skillExpTxt.text = $"다음 스킬 레벨업 {Main.Object.Player.lvSkill.skillLvUp[int.Parse(keyParts[3]) - 1] - Main.Object.Player.Data.Lv % Main.Object.Player.lvSkill.skillLvUp[int.Parse(keyParts[3])-1]}";
            }
        }
        else
        {
            skillNameText.text = "Name: -";
            skillCollTxt.text = "Coll: -";
            skillExpTxt.text = "Exp : -";
        }
        
        if( skillData.SkillLv == 0)
        {
            marsk.SetActive(true);
        }
        else
        {
            marsk.SetActive(false);
        }
    }
}
