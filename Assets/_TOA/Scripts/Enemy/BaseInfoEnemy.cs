using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInfoEnemy : MonoBehaviour
{
    public static BaseInfoEnemy Instance;
    private float baseHP, minAtkDamage, maxAtkDamage, atkRange, evadeChance, expOnDeath;
   
    public float BaseHP => baseHP;
    public float MinAtkDamage => minAtkDamage;
    public float MaxAtkDamage => maxAtkDamage;
    public float AtkRange => atkRange;
    public float EvadeChance => evadeChance;
    public float ExpOnDeath => expOnDeath;
    private void Awake()
    {
        Instance = this;
    }

    public void SetBaseInfo(float baseHP, float minAtkDamage, float maxAtkDamage, float atkRange, float evadeChance, float expOnDeath)
    {
        this.baseHP = baseHP;
        this.minAtkDamage = minAtkDamage;
        this.maxAtkDamage = maxAtkDamage;
        this.atkRange = atkRange;
        this.evadeChance = evadeChance;
        this.expOnDeath = expOnDeath;
    }
}
