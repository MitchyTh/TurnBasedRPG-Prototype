using System.IO.Enumeration;
using UnityEngine;

public class SkillBase : ScriptableObject
{

    [SerializeField] string skillName;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] int power;

    [SerializeField] int accuracy;

}
