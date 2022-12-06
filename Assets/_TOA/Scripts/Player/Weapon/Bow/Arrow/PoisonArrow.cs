using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArrow : Weapon
{
    private int poisonArrowDamage;
    private bool isCrit;
    private bool isMiss;

    public override void Awake()
    {
        base.Awake();
        Destroy(this.gameObject, 1.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(other.name);
            Damage = (int)Random.Range(MinDmg, MaxDmg + 1);
            poisonArrowDamage = Damage + (10 * (PlayerStats.Instance.Level - 1));

            Transform enemy = other.gameObject.transform;
            EnemyHitBox hitbox = enemy.gameObject.GetComponent<EnemyHitBox>();

            CheckMissAndCrit(enemy);

            FloatingDamage.Create(new Vector3(enemy.position.x, enemy.position.y + 2f, enemy.position.z - 0.5f), poisonArrowDamage, isCrit, isMiss);

            hitbox.OnHit(poisonArrowDamage, isCrit);
        }
    }
    private void CheckMissAndCrit(Transform enemy)
    {
        isMiss = Random.Range(0, 101) < enemy.GetComponentInParent<BaseInfoEnemy>().EvadeChance;
        if (isMiss)
        {
            poisonArrowDamage *= 0;
        }
        else
        {
            isCrit = Random.Range(0, 101) < CritChance;
            poisonArrowDamage *= (isCrit ? 2 : 1);
        }
    }
}
