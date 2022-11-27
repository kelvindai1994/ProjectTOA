using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float MinDmg;
    [SerializeField] protected float MaxDmg;
    [SerializeField] protected float CritChance;

    protected int Damage;
    protected BoxCollider bx;
    public virtual void Start()
    {
        bx = GetComponent<BoxCollider>();
        Damage = (int)Random.Range(MinDmg, MaxDmg + 1);
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;
        bx.isTrigger = true;
    }
}
