using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimAttackControler : AnimBase
{
    //Backstab
    public EnemyBackstab enemyBackstab;
    public float OffsetBackStab;
    public float OffsetViewPlayer;
    public float DistanceBackStab;
    [Range(0,1)]public float OffsetDistanceBetweenPlayerAndEnemy;

    #region UnityFunction

    void FixedUpdate()
    {
        //Equip Weapon
        if (Input.GetKeyDown(KeyCode.Tab) && animator.GetBool(ParamAnim_CanEquip))
        {
            animator.SetTrigger(ParamAnim_EquipTrigger);
        }
        if (!animator.GetBool(ParamAnim_isEquip)) return;
        //Attack
        Attack();

        if (enemyBackstab == null) return;
        //BackStab
        if (CanBackstab(enemyBackstab.transform))
        {
            Debug.Log("ShowUI");

            //GUILayout

            if (Input.GetKeyDown(KeyCode.F))
            {
                EnemyBackstabTransform(enemyBackstab.transform);
            }
        }
       
    }

    #endregion
    

    #region PrivateFunction
    private void Attack()
    {
        if (!animator.GetBool("isRMPressed"))
        {
            if (Input.GetMouseButton(0))
            {
                SetAttack(1);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetAttack(2);
            }
        }
    }
    private bool CanBackstab(Transform enemyPos)
    {
        Vector3 dirToPlayer = (transform.position - enemyPos.position).normalized;
        float distance = Vector3.Distance(transform.position, enemyPos.position);
        float dotBackEnemy = Vector3.Dot(enemyPos.forward, dirToPlayer);
        float dotForwardPlayer = Vector3.Dot(transform.forward, dirToPlayer);
        //Debug.LogError("dotBackEnemy" + dotBackEnemy);
        //Debug.Log("dotForwardPlayer" + dotForwardPlayer);
        if (dotBackEnemy < -1f + OffsetBackStab && distance < DistanceBackStab && dotForwardPlayer < -1 + OffsetViewPlayer)
            return true;
     
        return false;
    }
    private void EnemyBackstabTransform(Transform enemyPos)
    {
        if (enemyPos == null) return;
        //CheckBackstab
        if (!CanBackstab(enemyPos)) return;
        //Anim Player todo
        SetAttack(101);
        //Anim Enemy todo
        enemyBackstab.SetTargetForward(transform.forward);
        enemyPos.position = transform.position + transform.forward * OffsetDistanceBetweenPlayerAndEnemy;
        if (enemyBackstab.CanBackstabVictim())
        {
            enemyBackstab.AnimBackstab_Ambushed02Victim();
        }
    }
    private void SetAttack(int attackType)
    {
        if (animator.GetBool(ParamAnim_CanAttack))
        {
            animator.SetTrigger(ParamAnim_AttackTrigger);
            animator.SetInteger(ParamAnim_AttackType, attackType);
        }
    }
    #endregion

}
