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
    protected SphereCollider spx;
    public virtual void Awake()
    {
        if (this.gameObject.GetComponent<BoxCollider>())
        {
            bx = GetComponent<BoxCollider>();
        }
        if (this.gameObject.GetComponent<SphereCollider>())
        {
            spx = GetComponent<SphereCollider>();
        }
        Damage = (int)Random.Range(MinDmg, MaxDmg + 1);
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;
        bx.isTrigger = true;
    }

}
