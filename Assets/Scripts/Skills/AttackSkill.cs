using Mono.Cecil;
using UnityEditor.Rendering;
using UnityEngine;


[CreateAssetMenu(fileName = "AttackSkill", menuName = "Scriptable Objects/AttackSkill")]
public class AttackSkill : SkillBase
{
    public override void UseSkill(Unit user, Unit target)
    {
        int damageAmount = Power;
        target.TakeDamage(damageAmount);
        
    }
}
