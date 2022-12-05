using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public float offmin = 10f;
    public float offmax = 10f;

    public float onMin = 0.25f;
    public float onMax = 0.8f;
    public Light light;
    public AudioClip[] ThunderSound;
    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine("LightningControl");
    }

 
    void Update()
    {
        
    }

    IEnumerator LightningControl()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(offmin, offmax));
            light.enabled = true;

            StartCoroutine("Soundfx");
            yield return new WaitForSeconds(Random.Range(onMin, onMax));
            light.enabled = false;
        }
    }

    IEnumerator Soundfx()
    {
        yield return new WaitForSeconds(Random.Range(.25f, 1.75f));
        audioSource.PlayOneShot(ThunderSound[Random.Range(0, ThunderSound.Length)]);
    }
}
