using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
