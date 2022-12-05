using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectVolumeHandler : SettingManager
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
        track = AudioManager.Instance.GetTrackName(AudioManager.Instance.tracks, "Effect");

        SetStartEffectVolume();
        LoadVolume();
    }
    #endregion

    #region PublicFunctions

    #region OnValueChange
    public void SetEffectVolume(float value)
    {
        track.source.volume = value;

        SettingGroupChange(SettingReferences.Instance.effectS.value, PlayerPrefs.GetFloat(CONSTANT.PP_EFFECT_VOLUME));
    }
    #endregion

    #endregion

    #region PrivateFunctions
    private void LoadVolume()
    {
        SettingReferences.Instance.effectS.value = PlayerPrefs.GetFloat(CONSTANT.PP_EFFECT_VOLUME);
    }
    private void SetStartEffectVolume()
    {
        if (!PlayerPrefs.HasKey(CONSTANT.PP_EFFECT_VOLUME))
        {
            PlayerPrefs.SetFloat(CONSTANT.PP_EFFECT_VOLUME, CONSTANT.DEFAULT_EFFECT_VOLUME);
        }

        track.source.volume = PlayerPrefs.GetFloat(CONSTANT.PP_EFFECT_VOLUME);
    }
    #endregion


}
