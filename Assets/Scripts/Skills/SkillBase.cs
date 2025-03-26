using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "Player/Create new skill")]
public class SkillBase : ScriptableObject
{

    [SerializeField] string skillName;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] int power;

    [SerializeField] int accuracy;


}
