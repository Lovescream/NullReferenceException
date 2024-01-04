using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOBJ : MonoBehaviour
{
    string skillName;
    public string key;
    public SkillData skill;
    public int skillExp; // �浹�� �÷��̾� ��ų�� �ο� �� ����ġ

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SkillData playerSkill = FindItem(key, Main.Data.PlayerSkils);

            if (playerSkill != null)
            {
                AddSkillExp(playerSkill);
            }
            else
            {
                AddSkillToPlayerSkills();
            }
            Destroy(gameObject);
        }
    }

    void AddSkillExp(SkillData playerSkill)
    {
        if (playerSkill.SkillLv == 0)
        {
            playerSkill.SkillLv = 1;
            Debug.Log($"{playerSkill.SkillName}�� ���� ����");
        }
        else
        {
            playerSkill.AddSkillExp(skillExp);
            Debug.Log($"{playerSkill.SkillName}�� {skillExp} ����ġ �߰�");
        }
    }

    void AddSkillToPlayerSkills()
    {
        if (!Main.Data.PlayerSkils.ContainsKey(key))
        {
            Main.Data.PlayerSkils.Add(key, skill);
            Debug.Log($"�÷��̾� ��ų�� {skill.SkillName} �߰�");
        }
        else
        {
            Debug.LogWarning($"�÷��̾� ��ų�� Ű {key}�� ���� ��ų�� �̹� �����մϴ�");
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
