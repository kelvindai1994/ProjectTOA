using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectVolumeHandler : SettingManager
{

    private AudioTrack playerTrack;


    #region ParentOverride
    public override void SettingGroupChange(float value, float savedValue)
    {
        base.SettingGroupChange(value, savedValue);
    }
    #endregion

    #region UnityFunctions
    private void Start()
    {
        playerTrack = AudioManager.Instance.GetTrackName(AudioManager.Instance.tracks, "Player_Effect");

        SetStartEffectVolume();
        LoadVolume();
    }
    #endregion

    #region PublicFunctions

    #region OnValueChange
    public void SetEffectVolume(float value)
    {
        playerTrack.source.volume = value;

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

        playerTrack.source.volume = PlayerPrefs.GetFloat(CONSTANT.PP_EFFECT_VOLUME);
    }
    #endregion


}
