using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondGate : MonoBehaviour
{
    public GameObject target;
    public GameObject portal;
    public List<GameObject> eliteEnemies;

    private bool triggerPortal;

    private void Start()
    {
        portal.SetActive(false);
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

        if ((eliteEnemies.Count <= 0 || eliteEnemies == null) && target)
        {
            portal.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            triggerPortal = true;
            target = other.gameObject;
        }
    }
}
