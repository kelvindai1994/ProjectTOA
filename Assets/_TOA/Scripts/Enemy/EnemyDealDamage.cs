using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealDamage : MonoBehaviour
{
    private float minDamage;
    private float maxDamage;
    private float avgDamge;
    private void Start()
    {
        minDamage = this.gameObject.GetComponentInParent<BaseInfoEnemy>().MinAtkDamage;
        maxDamage = this.gameObject.GetComponentInParent<BaseInfoEnemy>().MaxAtkDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            avgDamge = (Random.Range(minDamage, maxDamage + 1));
            PlayerStats.OnTakeDamage(avgDamge);
        }
    }
}
