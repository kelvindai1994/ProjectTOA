using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrow : Weapon
{
    private int fireArrowDamage;
    private bool isCrit;
    private bool isMiss;


    public override void Awake()
    {
        base.Awake();
        Destroy(this.gameObject, 0.3f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Damage = (int)Random.Range(MinDmg, MaxDmg + 1);
            fireArrowDamage = Damage + (10 * (PlayerStats.Instance.Level - 1));

            Transform enemy = other.gameObject.transform;
            EnemyHitBox hitbox = enemy.gameObject.GetComponent<EnemyHitBox>();

            CheckMissAndCrit(enemy);

            FloatingDamage.Create(new Vector3(enemy.position.x, enemy.position.y + 2f, enemy.position.z - 0.5f), fireArrowDamage, isCrit, isMiss);

            hitbox.OnHit(fireArrowDamage, isCrit);
        }
    }
    private void CheckMissAndCrit(Transform enemy)
    {
        isMiss = Random.Range(0, 101) < enemy.GetComponentInParent<BaseInfoEnemy>().EvadeChance;
        if (isMiss)
        {
            fireArrowDamage *= 0;
        }
        else
        {
            isCrit = Random.Range(0, 101) < CritChance;
            fireArrowDamage *= (isCrit ? 2 : 1);
        }
    }
}
