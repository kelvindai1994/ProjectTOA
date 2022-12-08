using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossControler : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;
    public Animator animator;

    [Space]
    [Tooltip("Spot Distance")]
    public float maxSpotDistance;
    [Tooltip("Chase Distance")]
    public float maxChaseDistance;
    [Tooltip("Attack Distance")]
    public float maxAtkDistance;
    [Tooltip("Fire Breath Distance")]
    public float minfireBreathDistance;

    //Delay between fire breath
    private const float MAX_FIRE_BREATH_TIME = 15f;
    private float fireBreathDelay;
    private bool canFireBreath = true;

    //-------
    private bool hasSpot;
    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {       
        float distanceToTarget = CalculateDistance(target.transform, this.gameObject.transform);
        Debug.Log(distanceToTarget);
        if (distanceToTarget <= maxSpotDistance && !hasSpot)
        {
            //play threat animation
            animator.Play("Threat");
            hasSpot = true;
        }
        if (hasSpot)
        {
            gameObject.transform.LookAt(target.transform);

            if (distanceToTarget <= maxChaseDistance && distanceToTarget > maxAtkDistance)
            {
                //FireBreath
                if(distanceToTarget >= minfireBreathDistance && canFireBreath)
                {
                    animator.SetBool("isFireBreath", true);
                    animator.Play("Forward_Breath");

                    canFireBreath = false;
                    fireBreathDelay = MAX_FIRE_BREATH_TIME;
                }
                else if (!canFireBreath)
                {
                    //chase target
                    animator.SetBool("isChase", true);

                    agent.SetDestination(target.transform.position);
                }
            }
            if (distanceToTarget <= maxAtkDistance)
            {
                //Start attacking
                animator.SetBool("isChase", false);
                animator.SetBool("isAttack", true);

                int atkNum = Random.Range(1, 5);
                animator.SetInteger("AttakType", atkNum);
                animator.SetTrigger("Attack");
            }
        }


        if (!canFireBreath)
        {
            animator.SetBool("isFireBreath", false);
            fireBreathDelay -= Time.deltaTime;
            if(fireBreathDelay <= 0)
            {
                canFireBreath = true;
            }
        }
        
    }


    private float CalculateDistance(Transform self, Transform target)
    {
        float distance = Vector3.Distance(self.position, target.position);
        return distance;
    }
}
