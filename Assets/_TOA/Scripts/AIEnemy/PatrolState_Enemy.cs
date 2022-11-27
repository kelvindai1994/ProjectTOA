using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState_Enemy : StateMachineBehaviour
{
    private Transform[] arrayPoint;
    private GameObject target;
    private NavMeshAgent agent;
    private SpawnerEnemy spawnerEnemy;
    
    private Vector3 Point;
    private bool randomMove;
    private int indexPoint;
   
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        spawnerEnemy = animator.GetComponentInParent<SpawnerEnemy>();
        arrayPoint = animator.transform.parent.GetChild(0).GetComponentsInChildren<Transform>();
        randomMove = spawnerEnemy.RandomMove;
        target = spawnerEnemy.Target;
        agent.stoppingDistance = 0.2f;

        if (randomMove)
        {
            Vector3 offsetArea = spawnerEnemy.OffsetAreaMove;
            Vector3 AreaMove = spawnerEnemy.AreaMove.position + offsetArea;
            float size = spawnerEnemy.AreaSize;
            Point = GetPoint(AreaMove, size);
            agent.SetDestination(Point);
        }
        else
        {
            indexPoint++;
            if(indexPoint >= arrayPoint.Length)
            {
                indexPoint = 0;
            }        
            agent.SetDestination(arrayPoint[indexPoint].position);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (randomMove)
        {
            Vector3 pos = animator.transform.position;
            pos.y = spawnerEnemy.AreaMove.position.y;
            if (Vector3.SqrMagnitude(Point - pos) < 0.1f)
            {
                animator.SetBool("isRun", false);
            }
        }
        else
        {
            if(agent.remainingDistance <= agent.stoppingDistance) 
            {
                animator.SetBool("isRun", false);
            }        
        }


        float distance = Vector3.Distance(animator.transform.position, target.transform.position);
        //checkInFront 
        bool inFront = spawnerEnemy.CheckInFront(animator.transform, target.transform);
        if (distance < 10f && inFront)
        {
            animator.SetBool("isChase", true);
        }

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
    Vector3 GetPoint(Vector3 AreaMove, float size)
    {
        float x = Random.Range(AreaMove.x + size, AreaMove.x - size);
        float z = Random.Range(AreaMove.z + size, AreaMove.z - size);
        return new Vector3(x,AreaMove.y,z);
    }
}
