using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamStateInfoBehaviour : StateMachineBehaviour
{
    [System.Serializable]
    public struct SetParamStateData
    {
        public string ParamName;
        public bool SetDefaultState;
    }
    public SetParamStateData[] ParamStateDatas;
    public float TimeState;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (SetParamStateData paramStateData in ParamStateDatas)
        {

        }
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= TimeState)
        {
            foreach (SetParamStateData paramStateData in ParamStateDatas)
            {
                animator.SetBool(paramStateData.ParamName, paramStateData.SetDefaultState);
            }
        }
    }
}
