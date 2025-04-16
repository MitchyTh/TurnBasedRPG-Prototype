using UnityEngine;

[CreateAssetMenu(fileName = "HealingSkill", menuName = "Scriptable Objects/HealingSkill")]
public class HealingSkill : SkillBase
{
    public override int UseSkill(Unit user, Unit target)
    {
        int healAmount = Power;
        user.HealDamage(healAmount);
        return 2;
    } 
}
