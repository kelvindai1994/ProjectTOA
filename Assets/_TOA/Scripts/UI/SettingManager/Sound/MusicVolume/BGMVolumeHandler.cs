
using UnityEngine;

public class BGMVolumeHandler : SettingManager
{

    private AudioTrack track;

    #region ParentOverride
    public override void SettingGroupChange(float value, float savedValue)
    {
        base.SettingGroupChange(value, savedValue);
    }
    #endregion

    #region UnityFunctions
    private void Start()
    {
        track = AudioManager.Instance.GetTrackName(AudioManager.Instance.tracks, "BGM");
        
        SetStartMusicVolume();
        LoadVolume();

        AudioManager.Instance.PlayAudio(AudioType.ST_Menu, true, PlayerPrefs.GetFloat(CONSTANT.PP_MUSIC_VOLUME), 3.5f);

    }
    #endregion

    #region PublicFunctions

    #region OnValueChange
    public void SetMusicVolume(float value)
    {
        track.source.volume = value;

        //Using EventListener is better
        SettingGroupChange(SettingReferences.Instance.musicS.value, PlayerPrefs.GetFloat(CONSTANT.PP_MUSIC_VOLUME));

    }
    #endregion

    #endregion

    #region PrivateFunctions
    private void LoadVolume()
    {
        SettingReferences.Instance.musicS.value = PlayerPrefs.GetFloat(CONSTANT.PP_MUSIC_VOLUME);
    }

    private void SetStartMusicVolume()
    {
        if (!PlayerPrefs.HasKey(CONSTANT.PP_MUSIC_VOLUME))
        {
            PlayerPrefs.SetFloat(CONSTANT.PP_MUSIC_VOLUME, CONSTANT.DEFAULT_MUSIC_VOLUME);
        }
        track.source.volume = PlayerPrefs.GetFloat(CONSTANT.PP_MUSIC_VOLUME);
    }
    #endregion







}
