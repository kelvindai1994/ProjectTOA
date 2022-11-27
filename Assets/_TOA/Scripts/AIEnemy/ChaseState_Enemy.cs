using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState_Enemy : StateMachineBehaviour
{
    private GameObject target;
    private SpawnerEnemy spawnerEnemy;
    private NavMeshAgent agent;
    
    private float AtkRange;
    private float maxChaseDistance = 30f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        AtkRange = animator.GetComponent<BaseInfoEnemy>().AtkRange;
        spawnerEnemy = animator.GetComponentInParent<SpawnerEnemy>();
        target = spawnerEnemy.Target;
        agent.stoppingDistance = AtkRange;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(target.transform.position);
        if (agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            animator.SetBool("isChase", false);
            return;
        }
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            animator.SetBool("isAtk", true);
        }
        float distance = Vector3.Distance(agent.transform.position, spawnerEnemy.gameObject.transform.position);
        if(distance >= maxChaseDistance)
        {
            animator.SetBool("isChase", false);
            Transform reset = agent.gameObject.GetComponentInParent<PointSpawner>().gameObject.transform;
            Debug.Log("Reset " + reset.name);
            agent.SetDestination(reset.position);
        }
        //if(agent.remainingDistance >= 10)
        //{
        //    animator.SetBool("isChase", false);
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
