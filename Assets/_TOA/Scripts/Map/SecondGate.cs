using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondGate : MonoBehaviour
{
    public GameObject portal;

    private void Start()
    {
        portal.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            portal.SetActive(true);
        }
    }
}
