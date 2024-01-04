using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public SkillData skillData;
    public Image SkillIconSprite;

    protected virtual void SetDefaultSkillData(SkillData data)
    {
        skillData = data;

        if (skillData != null)
        {
            if (skillData.Key != null)
            {
                SkillIconSprite.sprite = Resources.Load<Sprite>("Sprite/SkillIcon/" + skillData.Key);
            }
            else
            {
                SkillIconSprite.sprite = Resources.Load<Sprite>("Sprite/SkillIcon/Null");
            }
        }
        else
        {
            SkillIconSprite.sprite = Resources.Load<Sprite>("Sprite/SkillIcon/Null");
            Debug.LogWarning("SetDefaultSkillData: skillData is null");
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



