using UnityEngine;

public class UIVolumeHandle : SettingManager
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
        track = AudioManager.Instance.GetTrackName(AudioManager.Instance.tracks, "UI");

        SetStartUIVolume();
        LoadVolume();

    }
    #endregion

    #region PublicFunctions

    #region OnValueChange
    public void SetUIVolume(float value)
    {
        track.source.volume = value;

        SettingGroupChange(SettingReferences.Instance.uiS.value, PlayerPrefs.GetFloat(CONSTANT.PP_UI_VOLUME));

    }
    #endregion

    #endregion

    #region PrivateFunctions
    private void LoadVolume()
    {
        SettingReferences.Instance.uiS.value = PlayerPrefs.GetFloat(CONSTANT.PP_UI_VOLUME);
    }

    private void SetStartUIVolume()
    {
        if (!PlayerPrefs.HasKey(CONSTANT.PP_UI_VOLUME))
        {
            PlayerPrefs.SetFloat(CONSTANT.PP_UI_VOLUME, CONSTANT.DEFAULT_UI_VOLUME);
        }

        track.source.volume = PlayerPrefs.GetFloat(CONSTANT.PP_UI_VOLUME);
    }
    #endregion






}
