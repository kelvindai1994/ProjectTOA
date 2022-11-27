using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;
    //private Vector3 direction = new Vector3(-0.37f, 10f, 0.88f);
    public Transform up;
    public Transform targetPos;
    public Transform resetPos;
    public Transform[] resetPlayerParent;

    public bool isUp;
    public bool isDown;

    private float maxTimer = 15f;
    private float timer;
    private void Start()
    {
        isDown = true;
        isUp = !isDown;
        timer = maxTimer;
    }

    private void Update()
    {

        if(isDown)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                up.position = Vector3.MoveTowards(up.position, targetPos.position, speed * Time.deltaTime);
                if (CalculateDistance(up, targetPos) <= 0)
                {
                    isDown = false;
                    isUp = !isDown;
                }
            }
        }
        if (isUp)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                up.position = Vector3.MoveTowards(up.position, resetPos.position, speed * Time.deltaTime);
                if (CalculateDistance(up, resetPos) <= 0)
                {
                    isDown = true;
                    isUp = !isDown;
                }
            }        
        }
    }

    #region PrivateFunction
    private float CalculateDistance(Transform origin, Transform target)
    {   
        float distance = Vector3.Distance(origin.position, target.position);
        return distance;
    }
    #endregion
}
