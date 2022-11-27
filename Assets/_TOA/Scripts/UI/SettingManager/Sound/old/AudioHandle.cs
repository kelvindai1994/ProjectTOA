using System;
using UnityEngine;

public class AudioHandle : MonoBehaviour
{
    public static AudioHandle Instance;

    public SoundList[] bgm;
    public SoundList[] ui;
    public SoundList[] effect;

    private void Awake()
    {
        Instance = this;
        GetSoundList(bgm);
        GetSoundList(ui);
        GetSoundList(effect);

    }
    
    public void PlaySound(SoundList[] sl,string soundName)
    {
        SoundList sound = Array.Find(sl, item => item.name == soundName);
        if (sound != null)
        {
            sound.source.volume = sound.volume;
            sound.source.Play();
        }
        else
        {
            Debug.LogWarning("Sound " + name + "not found !");
        }
    }

    public SoundList GetSoundName(SoundList[] sl,string soundName)
    {
        SoundList sound = Array.Find(sl, item => item.name == soundName);
        return sound;
    }

    private void GetSoundList(SoundList[] sl)
    {
        foreach (SoundList s in sl)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
    }



   

   
}
