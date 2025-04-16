using UnityEngine;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public int HP => PlayerStatsManager.Instance.HP;
    public int MaxHP => PlayerStatsManager.Instance.MaxHP;
    public int damage => PlayerStatsManager.Instance.damage;
    public List<SkillBase> skillList => PlayerStatsManager.Instance.skillList;

    public bool TakeDamage(int dmg)
    {
        PlayerStatsManager.Instance.HP -= dmg;
        if (PlayerStatsManager.Instance.HP <= 0)
        {
            PlayerStatsManager.Instance.HP = PlayerStatsManager.Instance.lossHP;
            return true;
        }
        return false;
    }
    public bool HealDamage(int health) {
    PlayerStatsManager.Instance.HP += health;
        if (PlayerStatsManager.Instance.HP > PlayerStatsManager.Instance.MaxHP)
        {
            PlayerStatsManager.Instance.HP = PlayerStatsManager.Instance.MaxHP;
            return true;
        }
        return false;
    }
}
