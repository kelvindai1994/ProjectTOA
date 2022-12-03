using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonArrow : Weapon
{
    private int dragonArrowDamage;
    private bool isCrit;
    private bool isMiss;


    public override void Awake()
    {
        base.Awake();
    }
    private void OnParticleTrigger()
    {
        CheckCollider();
    }
    private void CheckCollider()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, this.GetComponent<ParticleSystem>().trigger.radiusScale);
        foreach (Collider c in colliders)
        {
            if (c.CompareTag("Enemy"))
            {
                Debug.Log(c.name);
                Damage = (int)Random.Range(MinDmg, MaxDmg + 1);
                dragonArrowDamage = Damage + (10 * (PlayerStats.Instance.Level - 1));

                Transform enemy = c.gameObject.transform;
                EnemyHitBox hitbox = c.GetComponent<EnemyHitBox>();

                CheckMissAndCrit(enemy);

                FloatingDamage.Create(new Vector3(enemy.position.x, enemy.position.y + 2f, enemy.position.z - 0.5f), dragonArrowDamage, isCrit, isMiss);

                hitbox.OnHit(dragonArrowDamage, isCrit);

            }
        }
    }
    private void CheckMissAndCrit(Transform enemy)
    {
        isMiss = Random.Range(0, 101) < enemy.GetComponentInParent<BaseInfoEnemy>().EvadeChance;
        if (isMiss)
        {
            dragonArrowDamage *= 0;
        }
        else
        {
            isCrit = Random.Range(0, 101) < CritChance;
            dragonArrowDamage *= (isCrit ? 2 : 1);
        }
    }
}
