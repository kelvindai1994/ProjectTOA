using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState_Enemy : StateMachineBehaviour
{
    private GameObject target = null;
    private SpawnerEnemy spawnerEnemy;

    private float timer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spawnerEnemy = animator.GetComponentInParent<SpawnerEnemy>();
        target = spawnerEnemy.Target;
        timer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        timer += Time.deltaTime;
        if(timer > 5)
        {
            animator.SetBool("isRun", true);
        }


        float distanceToTarget = Vector3.Distance(animator.transform.position, target.transform.position);
        //checkInFront 
        bool inFront = spawnerEnemy.CheckInFront(animator.transform, target.transform);
        if (distanceToTarget <= 15 && inFront)
        {
            animator.SetBool("isChase", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
