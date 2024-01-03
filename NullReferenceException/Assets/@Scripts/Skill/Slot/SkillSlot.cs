using UnityEngine;

public class SkillSlot : MonoBehaviour
{
    public SkillData skillData;
    protected virtual void SetDefaultSkillData(SkillData data)
    {
        skillData = data;
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
