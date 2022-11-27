using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForDragUpandDown : MonoBehaviour
{
    public float speed;
    public float freq;
    private Vector3 InitialPos;

    private void Start()
    {
        InitialPos = transform.position;
    }
    private void Update()
    {
        this.transform.position = new Vector3(InitialPos.x, Mathf.Sin(Time.time * freq + 2) * speed+ InitialPos.y, InitialPos.z);
        
    }
}
