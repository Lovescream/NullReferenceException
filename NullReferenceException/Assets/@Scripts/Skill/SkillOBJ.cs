using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOBJ : MonoBehaviour
{
    string skillName;
    public string key;
    public SkillData skill;
    public int skillExp; // 충돌시 플레이어 스킬에 부여 할 경험치

    void Start()
    {
        string objectName = gameObject.name.Replace("(Clone)", "").Trim();
        string[] nameParts = objectName.Split('-');

        if (nameParts.Length > 1)
        {
            key = nameParts[1];
        }
        else
        {
            Debug.LogWarning($"No key found in the object name: {objectName}");
            key = "DefaultKey";
        }

        skill = FindItem(key, Main.Data.Skils);
        if (skill != null)
        {
            Debug.Log($"SkillName of {gameObject.name}: {skill.SkillName}");
        }
    }

    SkillData FindItem(string key, Dictionary<string, SkillData> skillList)
    {
        if (skillList.TryGetValue(key, out SkillData foundSkill))
        {
            return foundSkill;
        }
        else
        {
            Debug.LogWarning($"Skill not found: {key}");
            return null;
        }
    }
}
