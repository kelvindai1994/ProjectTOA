using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalArena : MonoBehaviour
{
    public ParticleSystem portal;
    public List<GameObject> eliteEnemies;
    public BoxCollider bx;

    private bool triggerPortal;
    private void OnEnable()
    {
        portal = GetComponent<ParticleSystem>();
        bx = GetComponent<BoxCollider>();
        bx.enabled = false;

        triggerPortal = true;
    }

    private void Update()
    {
        if (triggerPortal)
        {
            for (int i = 0; i < eliteEnemies.Count; i++)
            {
                if (eliteEnemies[i].GetComponentInChildren<EnemyHealth>() == null)
                {
                    eliteEnemies.RemoveAt(i);
                }
            }
        }   

        if(eliteEnemies.Count <= 0 || eliteEnemies == null)
        {
            portal.Play();
            bx.enabled = true;      
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Change Scene");
        }
    }
}
