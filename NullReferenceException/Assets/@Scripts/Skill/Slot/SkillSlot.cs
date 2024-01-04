using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public SkillData skillData;
    public Image SkillIconSprite;

    protected virtual void SetDefaultSkillData(SkillData data)
    {
        skillData = data;
        
        if (skillData != null && skillData.Key != null)
        {
            SkillIconSprite.sprite = Resources.Load<Sprite>("Sprite/SkillIcon/" + skillData.Key);
        }
    }

    protected virtual void OnClick()
    {

    }

    public void SetSkillData(SkillData data)
    {
        SetDefaultSkillData(data);
    }

    public void OnSkillSlotClicked()
    {
        OnClick();
    }

}
