using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBase : MonoBehaviour
{
    //Param Anim Move
    protected int ParamAnim_CanMove = Animator.StringToHash("CanMove");
    protected int ParamAnim_Velocity_X = Animator.StringToHash("Velocity_X");
    protected int ParamAnim_Velocity_Z = Animator.StringToHash("Velocity_Z");
    protected int ParamAnim_Velocity = Animator.StringToHash("Velocity");
    protected int ParamAnim_MoveState = Animator.StringToHash("MoveState");
    protected int ParamAnim_CanSprint = Animator.StringToHash("CanSprint");
    protected int ParamAnim_CanRotation = Animator.StringToHash("CanRotation");
    //==== Slide
    protected int ParamAnim_CanSlide = Animator.StringToHash("CanSlide");
    protected int ParamAnim_SlideTriggerF = Animator.StringToHash("SlideTriggerF");
    protected int ParamAnim_SlideTriggerB = Animator.StringToHash("SlideTriggerB");
    protected int ParamAnim_SlideTriggerL = Animator.StringToHash("SlideTriggerL");
    protected int ParamAnim_SlideTriggerR = Animator.StringToHash("SlideTriggerR");

    //==== Equip
    protected int ParamAnim_CanEquip = Animator.StringToHash("CanEquip");
    protected int ParamAnim_isEquip = Animator.StringToHash("isEquip");
    protected int ParamAnim_EquipTrigger = Animator.StringToHash("EquipTrigger");

    //Attack
    protected int ParamAnim_CanAttack = Animator.StringToHash("CanAttack");
    protected int ParamAnim_AttackTrigger = Animator.StringToHash("AttackTrigger");
    protected int ParamAnim_AttackType = Animator.StringToHash("AttackType");

    protected Animator animator;
    public virtual void Start()
    {
        animator = GetComponent<Animator>();
    }
}
