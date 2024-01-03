using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public SkillData skillData;
    public Image SkillIconSprite;

    protected virtual void SetDefaultSkillData(SkillData data)
    {
        skillData = data;
        
        if (skillData.Key != "")
        {
            SkillIconSprite.sprite = Resources.Load<Sprite>("Sprite/SkillIcon/" + skillData.Key);
        }
        else
        {
            SkillIconSprite.sprite = Resources.Load<Sprite>("Sprite/SkillIcon/Null");
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
