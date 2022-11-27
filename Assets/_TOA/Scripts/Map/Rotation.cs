using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField]
    private Vector3 spinDirector;
    void Start()
    {
        
       
    }

   
    void Update()
    {
        OnRotation();
    }

    private void OnRotation()
    {
        transform.Rotate(spinDirector);
    }
}
