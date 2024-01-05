using UnityEngine;
using TMPro;
using System;

public class QuickSlot : SkillSlot
{
    public EquipSkillSlots equipSkillSlots;
    public TMP_Text coolTimeTxt;
    public GameObject panelMarsk;

    public SkillLogic skills;

    bool _isCoolTime = false;
    float _coolTime = 0;
    float _maxCoolTime;
    private void Update()
    {
        _maxCoolTime = skillData.CoolTime;

        SetSkillData(equipSkillSlots.skillData);

        if (_coolTime <= 0)
        {
            _isCoolTime = false;
            panelMarsk.SetActive(false);
        }
        else
        {
            _coolTime -= Time.deltaTime;
            float alpha = 0.0f + (_coolTime / _maxCoolTime);
            coolTimeTxt.text = _coolTime.ToString("F2");

            UpdatePanelAlpha(alpha);
        }
    }

    private void UpdatePanelAlpha(float alpha)
    {
        if (panelMarsk != null)
        {
            panelMarsk.SetActive(true);
            var panelColor = panelMarsk.GetComponent<UnityEngine.UI.Image>().color;
            panelColor.a = alpha;
            panelMarsk.GetComponent<UnityEngine.UI.Image>().color = panelColor;
        }
    }

    protected override void OnClick()
    {
        UsingQuick();
    }

    public void UsingQuick()
    {
        if (!_isCoolTime)
        {
            _coolTime = _maxCoolTime;
            _isCoolTime = true;
            UpdateSkills();
            skills.UsingSkills(skillData);
        }
    }

    public void UpdateSkills()
    {
        string[] nameParts = skillData.SkillName.Split('_');
        Type skillLogicType = Type.GetType(nameParts[1]);

        if (skillLogicType != null && skillLogicType.IsSubclassOf(typeof(SkillLogic)))
        {
            SkillLogic skillLogicComponent = gameObject.AddComponent(skillLogicType) as SkillLogic;
            skills = skillLogicComponent;
        }
        else
        {
            Debug.LogError($"Failed to find or add specific SkillLogic: {nameParts[1]}");
        }
    }
}
