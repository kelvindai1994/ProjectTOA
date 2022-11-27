using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInfoEnemy : MonoBehaviour
{
    public static BaseInfoEnemy Instance;
    private float baseHP, atkDamage, atkRange, evadeChance, expOnDeath;
   
    public float BaseHP => baseHP;
    public float AtkDamage => atkDamage;
    public float AtkRange => atkRange;
    public float EvadeChance => evadeChance;
    public float ExpOnDeath => expOnDeath;
    private void Awake()
    {
        Instance = this;
    }

    public void SetBaseInfo(float baseHP, float atkDamage, float atkRange, float evadeChance, float expOnDeath)
    {
        this.baseHP = baseHP;
        this.atkDamage = atkDamage;
        this.atkRange = atkRange;
        this.evadeChance = evadeChance;
        this.expOnDeath = expOnDeath;
    }
}
