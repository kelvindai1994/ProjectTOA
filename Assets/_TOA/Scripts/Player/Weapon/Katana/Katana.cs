using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{   
    public static Katana instance;

    [Header("WEAPON STATS PARAMETERS")]
    [SerializeField] private float minDamage;
    [SerializeField] private float maxDamage;
    [SerializeField] private int critChance;

    private float damage;

    private BoxCollider bx;

    public float Damage { get { return damage; }  }

    #region UnityFunction
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        bx = GetComponent<BoxCollider>();
        damage = Random.Range(minDamage, maxDamage);
    }
    #endregion

}
