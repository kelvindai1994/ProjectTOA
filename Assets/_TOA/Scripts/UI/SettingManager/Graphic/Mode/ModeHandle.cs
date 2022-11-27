using UnityEngine;

public class ModeHandle : SettingManager
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
        SetStartPlayMode();
        SettingReferences.Instance.modeDD.value = PlayerPrefs.GetInt(CONSTANT.PP_MODE);
    }
    #endregion

    #region PublicFunctions

     #region OnValueChange
    public void SetPlayMode(int modeIndexDD)
    {

        CheckValueChange(modeIndexDD, PlayerPrefs.GetInt(CONSTANT.PP_MODE));

        int width = PlayerPrefs.GetInt(CONSTANT.PP_RESOLUTION_WIDTH);
        int height = PlayerPrefs.GetInt(CONSTANT.PP_RESOLUTION_HEIGHT);
        switch (modeIndexDD)
        {
            case 0:
                Screen.SetResolution(width, height, FullScreenMode.Windowed);
                break;
            case 1:
                Screen.SetResolution(width, height, FullScreenMode.ExclusiveFullScreen);
                break;
            case 2:
                Screen.SetResolution(width, height, FullScreenMode.MaximizedWindow);
                break;
        }
        PlayerPrefs.SetInt(CONSTANT.PP_MODE, modeIndexDD);
    }
    #endregion

    #endregion

    #region PrivateFunctions
    private void SetStartPlayMode()
    {
        if (!PlayerPrefs.HasKey(CONSTANT.PP_MODE))
        {
            PlayerPrefs.SetInt(CONSTANT.PP_MODE, CONSTANT.DEFAULT_MODE);
        }

        switch (PlayerPrefs.GetInt(CONSTANT.PP_MODE))
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                //Screen.SetResolution(width, height, FullScreenMode.Windowed);
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                //Screen.SetResolution(width, height, FullScreenMode.ExclusiveFullScreen);
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                //Screen.SetResolution(width, height, FullScreenMode.MaximizedWindow);
                break;
        }
    }
    #endregion




}
