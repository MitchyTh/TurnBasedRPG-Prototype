using System.IO.Enumeration;
using UnityEngine;

public abstract class SkillBase : ScriptableObject
{

    [SerializeField] string skillName;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] int power;

    [SerializeField] int accuracy;

    public string SkillName => skillName;
    public string Description => description;
    public int Power => power;
    public int Accuracy => accuracy;


    public abstract int UseSkill(Unit user, Unit target);
}
