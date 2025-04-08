using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public BattleSystem BattleSystem;
    public SkillBase SkillBase;

    public void OnClick()
    {
        BattleSystem.UseSkill(SkillBase);
    }
}
