using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeParamStateBehaviour : StateMachineBehaviour
{
    public string ParamName;
    public bool SetDefaulValue;
    public float CanAttackAfter;

    bool _waitForExit;
    bool _onTransitionExitTriggered;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _waitForExit = false;
        _onTransitionExitTriggered = false;
        animator.SetBool(ParamName, !SetDefaulValue);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (CheckOnTransitionExit(animator, layerIndex))
        {
            OnTransitionExit(animator);
        }

        if (!_onTransitionExitTriggered && stateInfo.normalizedTime >= CanAttackAfter)
        {
            animator.SetBool(ParamName,SetDefaulValue);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_onTransitionExitTriggered)
        {
            animator.SetBool(ParamName,!SetDefaulValue);
        }
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

    #region PrivateFunction
    private bool CheckOnTransitionExit(Animator animator, int layerIndex)
    {
        if (!_waitForExit && animator.GetAnimatorTransitionInfo(layerIndex).fullPathHash == 0)
        {
            _waitForExit = true;
        }

        if (!_onTransitionExitTriggered && _waitForExit && animator.IsInTransition(layerIndex))
        {
            _onTransitionExitTriggered = true;
            return true;
        }
        return false;
    }

    private void OnTransitionExit(Animator animator)
    {
        animator.SetBool(ParamName, !SetDefaulValue);
    }
    #endregion

}
