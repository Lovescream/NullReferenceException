
using System;

public enum SkillGetType
{
    Lvup,
    Pickup
}
public enum SkillUsingType
{
    Active,
    Passive
}

[Serializable]
public class SkillData : Data
{
    public string SkillName { get; set; }
    public SkillUsingType SkillUsingType { get; set; }
    public SkillGetType SkillGetType { get; set; }
    public int SkillLv { get; set; }
    public int SkillExp { get; set; }
    public float CoolTime { get; set; }
    public float LvUpCollTime { get; set; }

    public void AddSkillExp(int exp)
    {
        SkillExp += exp;
        SkillLvup();
    }

    public void SkillLvup()
    {
        if (SkillGetType == SkillGetType.Lvup)
        {
            SkillExp += 100;
        }

        while (SkillExp >= 100)
        {
            if (SkillLv != 0)
            {
                CoolTime -= LvUpCollTime;
            }
            SkillLv++;
            SkillExp -= 100;

        }
    }
}
