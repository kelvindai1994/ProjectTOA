using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;
    //private Vector3 direction = new Vector3(-0.37f, 10f, 0.88f);
    public Transform up;

    public Transform destination;
    public Transform origin;

    private bool isMove;
    private bool hasMove;

    private float maxTimer = 10f;
    private float timer;
    private void Start()
    {
        timer = maxTimer;
    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            up.position = Vector3.MoveTowards(up.position, destination.position, speed * Time.deltaTime);
            if (CalculateDistance(up, destination) <= 0)
            {
                isMove = false;
                hasMove = true;

                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.SetParent(null);
            }
        }    
        if(!isMove && hasMove)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                up.position = Vector3.MoveTowards(up.position, origin.position, speed * Time.deltaTime);
                if (CalculateDistance(up, origin) <= 0)
                {
                    isMove = false;
                    hasMove = false;

                    

                    timer = maxTimer;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            player.transform.SetParent(up.transform, false);
            player.transform.localScale = Vector3.one;
            isMove = true;
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
