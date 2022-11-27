using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_ParticleAnim" , menuName = "SO_Particle/SO_AtkParticle")]
public class SO_AtkParticle : ScriptableObject
{
    public Mesh Mesh;
    public float StartTimeLife;
}
