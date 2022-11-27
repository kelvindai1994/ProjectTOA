using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseStats, AnotherScript
{

    public override void TakeDamge(float _hp)
    {
        base.TakeDamge(_hp);
        Debug.Log("Enemy take " + _hp);
    }

    public void ShjtFunction()
    {
        Debug.Log("do something");
    }

}
