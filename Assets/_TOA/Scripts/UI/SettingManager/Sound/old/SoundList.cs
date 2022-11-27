using UnityEngine;

[System.Serializable]
public class SoundList
{
    public string name;
    public AudioClip clip;
    [HideInInspector]
    public AudioSource source;
    [Range(0f, 1f)]
    public float volume = 1f;
    public bool loop = false;
}


