using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public SkillData skillData;
    public Image SkillIconSprite;

    protected virtual void SetDefaultSkillData(SkillData data)
    {
        skillData = data;
        if (Resources.Load<Sprite>("Sprites/SkillIcon/" + data.Key) != null)
        {
            SkillIconSprite.sprite = Resources.Load<Sprite>("Sprites/SkillIcon/" + data.Key);
        }
        else
        {
            Debug.LogError("Sprites/SkillIcon/" + data.Key + "is Null");
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
