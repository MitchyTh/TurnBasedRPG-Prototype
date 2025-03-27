using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;

    public int damage;

    public int maxHP;
    public int currentHP;
    public List<SkillBase> skills = new List<SkillBase>();

    private void Awake()
    {
        if (CompareTag("Player"))
        {
            PlayerStats playerStats = GetComponent<PlayerStats>();
            currentHP = playerStats.HP;
            damage = playerStats.damage;
            skills = playerStats.skillList;
            maxHP = playerStats.MaxHP;
        }
    }

    public bool TakeDamage(int dmg){
        currentHP -= dmg;

        if (CompareTag("Player")) //alters player health to persist across battles
        {
            PlayerStats playerStats = GetComponent<PlayerStats>();
            playerStats.TakeDamage(dmg);
        }

        if (currentHP <= 0){
            currentHP = 0;
            return true;
        }
        else{
            return false;
        }

    }
}
