using UnityEngine;

[CreateAssetMenu(fileName = "HealingSkill", menuName = "Scriptable Objects/HealingSkill")]
public class HealingSkill : SkillBase
{
    public override void UseSkill(Unit user, Unit target)
    {
        throw new System.NotImplementedException();
    }
    public int damageHealed; 
}
