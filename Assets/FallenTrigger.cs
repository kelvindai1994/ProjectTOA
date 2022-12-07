using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenTrigger : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats.OnTakeDamage(PlayerStats.Instance.MaxHP);
            Debug.Log("collider");
        }
    }
}
