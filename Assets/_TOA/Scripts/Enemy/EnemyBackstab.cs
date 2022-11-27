using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackstab : MonoBehaviour
{
    Animator animator;
    Vector3 targetForward;

    //anim Hash
    int ParamAnim_Ambushed02 = Animator.StringToHash("Ambushed02");
    int ParamAnim_CanBackstabVictim = Animator.StringToHash("CanBackstabVictim");
    // Start is called before the first frame update
    private void Awake()
    {
        targetForward = transform.forward;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleSmoothForwardRotation();
    }

    private void HandleSmoothForwardRotation()
    {
        float rotationSpeed = 40f;
        transform.forward = Vector3.Lerp(transform.forward, targetForward, rotationSpeed * Time.deltaTime);
    }
    public void SetTargetForward(Vector3 targetForward)
    {
        this.targetForward = targetForward;
    }

    //Anim
    public void AnimBackstab_Ambushed02Victim()
    {
        animator.SetTrigger(ParamAnim_Ambushed02);
    }
    public bool CanBackstabVictim()
    {
        return animator.GetBool(ParamAnim_CanBackstabVictim);
    }
}
