using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondGate : MonoBehaviour
{
    public GameObject portal;
    public List<GameObject> eliteEnemies;

    private bool triggerPortal;
    private void OnEnable()
    {
        portal.SetActive(false);

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

        if (eliteEnemies.Count <= 0 || eliteEnemies == null)
        {
            portal.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.ShowNotify<NotifyLoading>();
            NotifyLoading.Instance.Load((int)SceneIndex.Arena);
        }
    }
}
