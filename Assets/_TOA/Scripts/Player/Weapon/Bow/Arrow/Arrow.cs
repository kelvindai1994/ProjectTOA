using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon
{
    [SerializeField] private GameObject bowTrail;
    [SerializeField] private AudioSource hitEnemyAudio;
    [SerializeField] private AudioSource hitObjectAudio;
    private Rigidbody rb;
    

    private int arrowDamage;
    private bool isMiss;
    private bool isCrit;  

    private float xVelocity;
    private float yVelocity;
    private float zVelocity;
    private float combineVelocity;
    private float fallAngle;

    private bool hasCollisionOccured;
    #region UnityFunction
    public override void Awake()
    {
        base.Awake(); 
        rb = GetComponent<Rigidbody>();
        arrowDamage = Damage + (10 * (PlayerStats.Instance.Level - 1));

        Destroy(this.gameObject, 7f);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        if (hasCollisionOccured) return;
        if (collision.gameObject.CompareTag("EnemyParts"))
        {
            Transform enemy = collision.gameObject.transform;
            var hitbox = collision.gameObject.GetComponent<EnemyHitBox>();

            CheckMissAndCrit(enemy);
            //Show floating dmg
            FloatingDamage.Create(new Vector3(enemy.position.x, enemy.position.y + 1f, enemy.position.z - 0.5f), arrowDamage, isCrit, isMiss);
            //deal dmg to enemy
            hitbox.OnHit(arrowDamage, isCrit);
            //"animate" crosshair
            PlayerCrossHair.Instance.SetTrigger(true, isCrit);
            //stick arrow to enemy's body parts
            this.gameObject.transform.SetParent(enemy.transform, true);
            bowTrail.SetActive(false);

            //Audio
            hitEnemyAudio.Play();

            hasCollisionOccured = true;
        }
        else if (collision.gameObject.CompareTag("DefendObject"))
        {
            DefendObject.OnTakeDamge(arrowDamage * 100);
        }
        else
        {
            hitObjectAudio.Play();
        }
    }
    private void Update()
    {
        if (rb.velocity.magnitude <= 0) return;
        UpdateArrowFalingAngle();
    }
    #endregion
    #region PrivateFunction
    private void UpdateArrowFalingAngle()
    {
        xVelocity = rb.velocity.x;
        yVelocity = rb.velocity.y;
        zVelocity = rb.velocity.z;
        combineVelocity = Mathf.Sqrt(xVelocity * xVelocity + zVelocity * zVelocity);
        fallAngle = -1f * Mathf.Atan2(yVelocity, combineVelocity) * 180f / Mathf.PI;
        transform.eulerAngles = new Vector3(fallAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }
    private void CheckMissAndCrit(Transform enemy)
    {
        isMiss = Random.Range(0, 101) < enemy.GetComponentInParent<BaseInfoEnemy>().EvadeChance;
        if (isMiss)
        {
            arrowDamage *= 0;
        }
        else
        {
            isCrit = Random.Range(0, 101) < CritChance;
            arrowDamage *= (isCrit ? 2 : 1);
        }
    }
    #endregion

}
