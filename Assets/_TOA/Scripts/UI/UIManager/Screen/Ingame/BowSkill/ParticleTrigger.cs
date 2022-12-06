using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    public GameObject colliderGO;

    private ParticleSystem ps;
    private List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }
    void OnParticleTrigger()
    {      
        // get
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        Debug.Log(numEnter);

        // iterate
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            // instantiate the Game Object
            Instantiate(colliderGO, p.position, Quaternion.identity);
            enter[i] = p;
        }

        // set
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }
}
