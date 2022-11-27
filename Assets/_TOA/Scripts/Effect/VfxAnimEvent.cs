using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VfxAnimEvent : MonoBehaviour
{
    public MotionTrail motionTrail;
    public ParticleSystem particleSystem;
    public ParticleSystemRenderer particleSystemRenderer;
    public void OnParticle(SO_AtkParticle sO_AtkParticle)
    {
        particleSystem.startLifetime = sO_AtkParticle.StartTimeLife;
        particleSystemRenderer.mesh = sO_AtkParticle.Mesh;
        particleSystem.Emit(1);
    }
    public void EnableMotionTrail()
    {
        motionTrail.enabled = true;
    }
    public void DisableMotionTrail()
    {
        motionTrail.enabled = false;
    }
}
