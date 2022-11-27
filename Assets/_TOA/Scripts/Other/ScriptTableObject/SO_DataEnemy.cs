using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct EnemyData
{
    public GameObject Model;
    public float BaseHP;
    public float EvadeChance;
    public float MinAtckDamage;
    public float MaxAtkDamage;
    public float AtkRange;

    public float ExpOnDeath;
}
[CreateAssetMenu(menuName = "EnemyData" , fileName = "Data_")]
public class SO_DataEnemy : ScriptableObject
{
    public EnemyData[] enemyDatas;
}
