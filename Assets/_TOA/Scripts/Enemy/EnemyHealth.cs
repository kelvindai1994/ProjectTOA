using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    public static Action<int, bool> OnTakeDamage;
    public static Action<int> OnDamage;

    private Animator animator;

    private BaseInfoEnemy baseInfo;

    private FloatingHPBar healthBar;
    private float maxHealth;
    private int currentHealth;

    public float MaxHealth => maxHealth;
    #region UnityFunction
    private void Start()
    {
        if (baseInfo == null)
        {
            baseInfo = this.gameObject.GetComponent<BaseInfoEnemy>();
        }
        animator = this.gameObject.GetComponent<Animator>();

        healthBar = gameObject.GetComponentInChildren<FloatingHPBar>();
        maxHealth = baseInfo.BaseHP;
        currentHealth = (int)maxHealth;

        var capColliders = GetComponentsInChildren<CapsuleCollider>();
        foreach (var capCollider in capColliders)
        {
            EnemyHitBox hitBox = capCollider.gameObject.AddComponent<EnemyHitBox>();
            hitBox.enemyHealth = this;
        }

        
    }
    private void OnEnable()
    {
        OnTakeDamage += TakeDamage;
    }
    private void OnDisable()
    {
        OnTakeDamage -= TakeDamage;
    }
    #endregion

    #region PublicFunction
    public void TakeDamage(int amount, bool isCrit)
    {
        currentHealth -= amount;
        healthBar.UpdateHP((float)amount / maxHealth);
        animator.SetBool("isChase", true);
        if (isCrit)
        {
            animator.SetBool("Damage", true);
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    #endregion

    #region PrivateFunction
    private void Die()
    {
        //disable all collider
        var capColliders = GetComponentsInChildren<CapsuleCollider>();
        foreach (var capCollider in capColliders)
        {
            capCollider.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
        //player gain exp
        PlayerStats.OnKill(this.baseInfo.ExpOnDeath);
        //remove marker
        CompassBar.Instance.RemoveEnemyMarker(this.gameObject.GetComponent<EnemyMarker>());

        animator.SetTrigger("Dead");
        Destroy(this.gameObject, 10f);
    }
    #endregion

}
