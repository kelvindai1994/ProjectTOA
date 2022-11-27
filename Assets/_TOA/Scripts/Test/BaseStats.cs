using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : MonoBehaviour
{
    public float HP { get; set; }

    public float Damage { get; set; }

    public virtual void TakeDamge(float _hp)
    {
        HP -= _hp;
    }
}
