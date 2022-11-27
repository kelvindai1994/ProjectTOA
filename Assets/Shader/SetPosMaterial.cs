using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosMaterial : MonoBehaviour
{
    public Material[] mat;
    // Start is called before the first frame update
    void Update()
    {
        foreach (var m in mat)
        {
            m.SetVector("_PlayerPos",transform.position);
        }
    }

}
