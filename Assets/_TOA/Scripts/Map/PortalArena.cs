using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalArena : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.ShowNotify<NotifyLoading>();
            NotifyLoading.Instance.Load((int)SceneIndex.Arena);
        }
    }
}
