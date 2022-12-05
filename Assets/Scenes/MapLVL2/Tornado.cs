using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public Transform TornadoCenter;
    public float pullForce;
    public float refreshRate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "OJB")
        {
            StartCoroutine(pullObject(other,true));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "OJB")
        {
            StartCoroutine(pullObject(other, false));
        }
    }
    IEnumerator pullObject(Collider x, bool isPull)
    {
        if (isPull)
        {
            Vector3 ForceDir = TornadoCenter.position - x.transform.position;
            x.GetComponent<Rigidbody>().AddForce( ForceDir.normalized * pullForce * Time.deltaTime);
            yield return refreshRate;
            StartCoroutine(pullObject(x, isPull));

        }
    }
}
