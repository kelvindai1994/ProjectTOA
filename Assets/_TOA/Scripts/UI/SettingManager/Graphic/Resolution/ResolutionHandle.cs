using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ResolutionHandle : SettingManager
{
    private Resolution[] res;
    private List<string> res_DropOptions;

    private bool ApplyValueChange;

    #region ParentOverride
    public override void CheckValueChange(int value, int savedValue)
    {
        base.CheckValueChange(value, savedValue);
    }
    #endregion

    #region UnityFunctions
    private void Awake()
    {
        res = Screen.resolutions;
        res_DropOptions = new List<string>();
        GetUserResolution();
        SetStartResolution();

    }
    private void Start()
    {
        SettingReferences.Instance.resolutionDD.value = PlayerPrefs.GetInt(CONSTANT.PP_RESOLUTION_INDEX);
    }
    #endregion

    #region PublicFunctions

    #region OnValueChange
    public void SetResolution(int resolutionIndexDD)
    {
        if (ApplyValueChange == false)
        {
            ApplyValueChange = true;
            return;
        }
        CheckValueChange(resolutionIndexDD, PlayerPrefs.GetInt(CONSTANT.PP_RESOLUTION_INDEX));

        switch (PlayerPrefs.GetInt(CONSTANT.PP_MODE))
        {
            case 0:
                Screen.SetResolution(res[resolutionIndexDD].width, res[resolutionIndexDD].height, FullScreenMode.Windowed);
                break;
            case 1:
                Screen.SetResolution(res[resolutionIndexDD].width, res[resolutionIndexDD].height, FullScreenMode.ExclusiveFullScreen);
                break;
            case 2:
                Screen.SetResolution(res[resolutionIndexDD].width, res[resolutionIndexDD].height, FullScreenMode.ExclusiveFullScreen);
                break;
        }

        PlayerPrefs.SetInt(CONSTANT.PP_RESOLUTION_WIDTH, res[resolutionIndexDD].width);
        PlayerPrefs.SetInt(CONSTANT.PP_RESOLUTION_HEIGHT, res[resolutionIndexDD].height);
        PlayerPrefs.SetInt(CONSTANT.PP_RESOLUTION_INDEX, resolutionIndexDD);

    }
    #endregion

    #endregion

    #region PrivateFunctions
    private void SetStartResolution()
    {
        Resolution selectRes = new();

        if (!PlayerPrefs.HasKey(CONSTANT.PP_RESOLUTION_INDEX))
        {
            selectRes.width = Screen.currentResolution.width;
            selectRes.height = Screen.currentResolution.height;

            PlayerPrefs.SetInt(CONSTANT.PP_RESOLUTION_INDEX, (res.Length - 15));
            PlayerPrefs.SetInt(CONSTANT.PP_RESOLUTION_WIDTH, selectRes.width);
            PlayerPrefs.SetInt(CONSTANT.PP_RESOLUTION_HEIGHT, selectRes.height);

        }
        else
        {
            selectRes.width = PlayerPrefs.GetInt(CONSTANT.PP_RESOLUTION_WIDTH);
            selectRes.height = PlayerPrefs.GetInt(CONSTANT.PP_RESOLUTION_HEIGHT);
        }

        switch (PlayerPrefs.GetInt(CONSTANT.PP_MODE))
        {
            case 0:
                Screen.SetResolution(selectRes.width, selectRes.height, FullScreenMode.Windowed);
                break;
            case 1:
                Screen.SetResolution(selectRes.width, selectRes.height, FullScreenMode.ExclusiveFullScreen);
                break;
            case 2:
                Screen.SetResolution(selectRes.width, selectRes.height, FullScreenMode.ExclusiveFullScreen);
                break;
        }

    }

    private void GetUserResolution()
    {
        ApplyValueChange = false;
        SettingReferences.Instance.resolutionDD.ClearOptions();
        for (int i = 14; i < res.Length; i++)
        {
            string option = res[i].width + "X" + res[i].height + " , " + res[i].refreshRate + "hz";
            res_DropOptions.Add(option);
        }
        SettingReferences.Instance.resolutionDD.AddOptions(res_DropOptions);
    }
    #endregion





}
