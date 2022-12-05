using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioTrack[] tracks;

    private Hashtable m_AudioTable;
    private Hashtable m_JobTable;

    #region Class
    
    private class AudioJob
    {
        public AudioAction action;
        public AudioType type;
        public bool fade;
        public float maxVolume;
        public float delay;
        public AudioJob(AudioAction _action, AudioType _type, bool _fade,float _maxVolume, float _delay)
        {
            action = _action;
            type = _type;
            fade = _fade;
            maxVolume = _maxVolume;
            delay = _delay;

        }
    }

    private enum AudioAction
    {
        Unknown,
        START,
        STOP,
        RESTART

    }
    #endregion

    #region UnityFunctions
    private void Awake()
    {
        if (!Instance)
        {
            Configure();
        }
    }
    private void OnDisable()
    {
        Dispose();
    }
    #endregion

    #region PublicFunctions
    public void PlayAudio(AudioType type, bool fade = false, float maxVolume = 1.0f, float delay = 0.0f)
    {
        AddJob(new AudioJob(AudioAction.START, type, fade, maxVolume, delay));
    }
    public void StopAudio(AudioType type, bool fade = false, float maxVolume = 1.0f, float delay = 0.0f)
    {
        AddJob(new AudioJob(AudioAction.STOP, type, fade, maxVolume, delay));
    }
    public void RestartAudio(AudioType type, bool fade = false, float maxVolume = 1.0f, float delay = 0.0f)
    {
        AddJob(new AudioJob(AudioAction.RESTART, type, fade, maxVolume, delay));
    }

    public AudioClip GetAudioClipFromAudioTrack(AudioType type, AudioTrack track)
    {
        foreach (AudioObject obj in track.audio)
        {
            if (obj.type == type)
            {
                return obj.clip;
            }

        }
        return null;
    }

    public AudioTrack GetTrackName(AudioTrack[] at, string trackName)
    {
        AudioTrack track = Array.Find(at, item => item.name == trackName);
        return track;
    }
    #endregion

    #region PrivateFunctions
    private void Configure()
    {
        Instance = this;
        m_AudioTable = new Hashtable();
        m_JobTable = new Hashtable();
        GenerateAudioTable();
    }

    private void Dispose()
    {
        foreach(DictionaryEntry entry in m_JobTable)
        {
            IEnumerator job = (IEnumerator)entry.Value;
            StopCoroutine(job);
        }
    }

    private void GenerateAudioTable()
    {
        foreach(AudioTrack track in tracks)
        {
            foreach(AudioObject obj in track.audio)
            {
                if (m_AudioTable.ContainsKey(obj.type))
                {
                    Debug.LogWarning(obj.type + " already existed !");
                }
                else
                {
                    m_AudioTable.Add(obj.type, track);
                }
            }
        }
    }
    private IEnumerator RunAudioJob(AudioJob job)
    {
        yield return new WaitForSecondsRealtime(job.delay);

        AudioTrack track = (AudioTrack)m_AudioTable[job.type];
        track.source.clip = GetAudioClipFromAudioTrack(job.type, track);

        switch (job.action)
        {
            case AudioAction.START:
                {
                    track.source.Play();
                    track.source.loop = track.loop;
                    break;
                }
                
            case AudioAction.STOP:
                {
                    if (!job.fade)
                    {
                        track.source.Stop();
                    }
                    break;
                }
            case AudioAction.RESTART:
                {
                    track.source.Stop();
                    track.source.Play();
                    track.source.loop = track.loop;
                    break;
                }
        }
        if (job.fade)
        {
            float init = job.action == AudioAction.START || job.action == AudioAction.STOP ? 0.0f : job.maxVolume;
            float target = init == 0 ? job.maxVolume : 0.0f;
            float duration = 1.0f;
            float timer = 0.0f;
            while(timer <= duration)
            {
                track.source.volume = Mathf.Lerp(init, target, timer / duration);
                timer += Time.deltaTime;
                yield return null;
            }
            
            if(job.action == AudioAction.STOP)
            {
                track.source.Stop();
            }
        }

        m_JobTable.Remove(job.type);

        yield return null;
    }


    private void AddJob(AudioJob job)
    {
        RemoveConflictingJobs(job.type);

        IEnumerator jobRunner = RunAudioJob(job);
        m_JobTable.Add(job.type, jobRunner);
        StartCoroutine(jobRunner);
    }

    private void RemoveJob(AudioType type)
    {
        if (!m_JobTable.ContainsKey(type))
        {
            Debug.LogWarning("This job " + type + " is not running !");
            return;
        }

        IEnumerator runningJob = (IEnumerator)m_JobTable[type];
        StopCoroutine(runningJob);
        m_JobTable.Remove(type);
    }

    private void RemoveConflictingJobs(AudioType type)
    {
        if (m_JobTable.ContainsKey(type))
        {
            RemoveJob(type);
        }

        AudioType conflictAudio = AudioType.Unknown;
        AudioTrack audioTrackNeeded = (AudioTrack)m_AudioTable[type];
        foreach(DictionaryEntry entry in m_JobTable)
        {
            AudioType audioType = (AudioType)entry.Key;
            AudioTrack audioTrackInUse = (AudioTrack)m_AudioTable[audioType];
            if(audioTrackNeeded.source == audioTrackInUse.source)
            {
                conflictAudio = audioType;
            }
        }
        if(conflictAudio != AudioType.Unknown)
        {
            RemoveJob(conflictAudio);
        }
    }
    #endregion


}
