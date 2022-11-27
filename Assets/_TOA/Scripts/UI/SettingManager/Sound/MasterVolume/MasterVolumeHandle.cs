using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterVolumeHandle : SettingManager
{
    #region ParentOverride
    public override void SettingGroupChange(float value, float savedValue)
    {
        base.SettingGroupChange(value, savedValue);
    }
    #endregion

    #region UnityFunctions
    private void Start()
    {
        SetStartMasterVolume();
        LoadVolume();
    }
    #endregion

    #region PublicFunctions

    #region OnValueChange
    public void SetMasterVolume(float value)
    {
        AudioListener.volume = value;

        SettingGroupChange(AudioListener.volume, PlayerPrefs.GetFloat(CONSTANT.PP_MASTER_VOLUME));
    }
    #endregion

    #endregion

    #region PrivateFunctions
    private void LoadVolume()
    {
        SettingReferences.Instance.masterS.value = PlayerPrefs.GetFloat(CONSTANT.PP_MASTER_VOLUME);
    }

    private void SetStartMasterVolume()
    {
        if (!PlayerPrefs.HasKey(CONSTANT.PP_MASTER_VOLUME))
        {
            PlayerPrefs.SetFloat(CONSTANT.PP_MASTER_VOLUME, CONSTANT.DEFAULT_MASTER_VOLUME);
        }
        AudioListener.volume = PlayerPrefs.GetFloat(CONSTANT.PP_MASTER_VOLUME);
    }
    #endregion








}
