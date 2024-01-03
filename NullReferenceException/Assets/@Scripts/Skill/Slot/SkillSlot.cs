using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public SkillData skillData;
    public Image SkillIconSprite;

    protected virtual void SetDefaultSkillData(SkillData data)
    {
        skillData = data;
        SkillIconSprite.sprite = Resources.Load<Sprite>("Sprites/SkillIcon/" + data.Key);
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
