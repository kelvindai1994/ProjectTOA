using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public BoxCollider[] weapons;

    #region UnityFunction
    private void Start()
    {
        weapons = gameObject.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.AddComponent<EnemyDealDamage>();
        }
    }
    #endregion

    #region PublicFunction
    public void ActivateWeaponEvent(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (index == i)
            {
                weapons[i].enabled = true;
                break;
            }
        }
    }
    public void DeactivateWeaponEvent()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].enabled = false;
        }
    }
    #endregion
}
