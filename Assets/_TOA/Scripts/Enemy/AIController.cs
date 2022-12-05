using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject player;

    public float moveSpeed;


    private bool playerEnter;

    private void Update()
    {
        if (playerEnter)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player !!!");
            playerEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerEnter = false;
    }
}
