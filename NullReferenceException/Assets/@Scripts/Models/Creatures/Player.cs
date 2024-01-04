using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Creature {

    #region Input

    protected void OnMove(InputValue value) {
        Velocity = value.Get<Vector2>().normalized * Status[StatType.MoveSpeed].Value;
    }
    protected void OnLook(InputValue value) {
        LookDirection = (Camera.main.ScreenToWorldPoint(value.Get<Vector2>()) - this.transform.position).normalized;
    }
    protected void OnFire() {
        Projectile projectile = Main.Object.SpawnProjectile(this.transform.position).SetInfo(this);
        projectile.Velocity = LookDirection.normalized * 10f; // TODO::
    }
    protected void OnInteraction() {
        Debug.Log($"[Player] OnInteraction()");
    }
    protected void OnKey_Z() {
        Inventory.Add(new(Main.Data.Items["IronSword"]));
    }
    protected void OnKey_X() {
        Inventory.Add(new(Main.Data.Items["IronHammer"]));
    }
    protected void OnKey_C() {
        Inventory.Add(new(Main.Data.Items["IronHelmet"]));
    }
    protected void OnKey_V() {
        Inventory.Add(new(Main.Data.Items["IronBoots"]));
    }

    protected void OnKey_K(){
        if(Main.Instance.Skill.isSkillList == true)
        {
           Main.Instance.Skill.SkillList.SetActive(false);
           Main.Instance.Skill.isSkillList = !Main.Instance.Skill.isSkillList;
        }
        else
        {
            Main.Instance.Skill.UpdateSkillSlotUI();
            Main.Instance.Skill.SkillList.SetActive(true);
            Main.Instance.Skill.isSkillList = !Main.Instance.Skill.isSkillList;
        }
    }

    protected void OnKey_Skill1()
    {
        Main.Instance.Skill.qSlot[0].GetComponent<QuickSlot>().UsingQuick();
    }
    protected void OnKey_Skill2()
    {
        Main.Instance.Skill.qSlot[1].GetComponent<QuickSlot>().UsingQuick();
    }
    protected void OnKey_Skill3()
    {
        Main.Instance.Skill.qSlot[2].GetComponent<QuickSlot>().UsingQuick();
    }
    #endregion

}