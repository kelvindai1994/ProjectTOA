using UnityEngine;

public class QualityHandle : SettingManager 
{

    #region ParentOverride
    public override void CheckValueChange(int value, int savedValue)
    {
        base.CheckValueChange(value, savedValue);
    }

    #endregion

    #region UnityFunctions
    private void Start()
    {
        SetStartQuality();
        SettingReferences.Instance.qualityDD.value = PlayerPrefs.GetInt(CONSTANT.PP_QUALITY);
    }
    #endregion

    #region PublicFunctions

    #region OnValueChange
    public void SetQualityValue(int qualityIndexDD)
    {
        CheckValueChange(qualityIndexDD, PlayerPrefs.GetInt(CONSTANT.PP_QUALITY));

        QualitySettings.SetQualityLevel(qualityIndexDD);
        PlayerPrefs.SetInt(CONSTANT.PP_QUALITY, qualityIndexDD);


    }
    #endregion

    #endregion

    #region PrivateFunctions
    private void SetStartQuality()
    {
        if (!PlayerPrefs.HasKey(CONSTANT.PP_QUALITY))
        {
            PlayerPrefs.SetInt(CONSTANT.PP_QUALITY, CONSTANT.DEFAULT_QUALITY);
        }
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt(CONSTANT.PP_QUALITY));
    }
    #endregion


}
