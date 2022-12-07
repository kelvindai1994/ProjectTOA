using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.ShowNotify<NotifyLoading>();
            NotifyLoading.Instance.Load((int)SceneIndex.Map1);

            UIManager.Instance.ShowNotify<Notification>();
        }
    }
}
