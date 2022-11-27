using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    public EnemyHealth enemyHealth;

    private void Start()
    {
        if(enemyHealth == null)
        {
            enemyHealth = this.gameObject.GetComponent<EnemyHealth>();
        }
    }

    public void OnHit(int amount, bool isCrit)
    {
        enemyHealth.TakeDamage(amount, isCrit);
    }
}
