public class EquipSkillSlots : SkillSlot
{
    public int index;

    protected override void OnClick()
    {
        Main.Skill.selectSolot = index;
    }
}
