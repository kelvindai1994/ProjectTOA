using UnityEngine;

[System.Serializable]
public class AudioObject
{
    public AudioType type;
    public AudioClip clip;
}
[System.Serializable]
public class AudioTrack
{
    public string name;
    public AudioSource source;
    public AudioObject[] audio;
    public bool loop;
}
