using UnityEngine;
using System.Collections.Generic;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance;

    public int HP = 30;
    public int MaxHP = 30;
    public int damage = 10;
    public List<SkillBase> skillList = new List<SkillBase>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateStats(PlayerStats stats)
    {
        HP = stats.HP;
        MaxHP = stats.MaxHP;
        damage = stats.damage;
        skillList = stats.skillList;
    }
}
