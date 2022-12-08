using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalEvent : MonoBehaviour
{
    public ParticleSystem VFX_FireAtk;
    ParticleSystem[] childFx;

    [System.Obsolete]
    void PlayParticle()
    {
        childFx = VFX_FireAtk.transform.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem child in childFx)
        {
            child.Stop();
            child.loop = true;
            var mainFx = child.main;
            child.Play();
        }
    }

    [System.Obsolete]
    void EndParticle()
    {
        foreach (ParticleSystem child in childFx)
        {
            child.loop = false;
        }
    }
}
