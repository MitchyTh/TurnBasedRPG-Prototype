using UnityEngine;

[CreateAssetMenu(fileName = "DefenseSkill", menuName = "Scriptable Objects/DefenseSkill")]
public class DefenseSkill : SkillBase
{
    public override void UseSkill(Unit user, Unit target)
    {
        throw new System.NotImplementedException();
    }
    public int damageBlocked;
}
