using System;
using UnityEngine;
using TMPro;

public class SaveConfirm : MonoBehaviour
{
    public static SaveConfirm Instance;
    public PopupSave popupSave;

    [Header("Warning")]
    public CanvasGroup ConfirmWarningCanvas;
    public TMP_Text CountDownText;

    private int savedQualityLevel;
    private int savedMode;
    private int savedResolution;


    private float maxCountDownTime = 15;
    private float CountDownTime;

    public bool isPromp;

    #region UnityFunctions
    private void Awake()
    {
        Instance = this;
        isPromp = false;
        CountDownTime = maxCountDownTime;
        ConfirmWarningCanvas = this.GetComponent<CanvasGroup>();

    }
    private void Start()
    {

        savedQualityLevel = SettingReferences.Instance.qualityDD.value;

        savedMode = SettingReferences.Instance.modeDD.value;

        savedResolution = SettingReferences.Instance.resolutionDD.value;


    }
    private void Update()
    {
        if (isPromp)
        {
            //popupSave.Show(null);
            //GameManager.Instance.SetCanvas(ConfirmWarningCanvas, true);
            if (CountDownTime > 0)
            {
                CountDownTime -= Time.deltaTime;
                CountDownText.SetText("Settings will be reverted in : {0:0}", (int)CountDownTime);
            }
            else
            {
                RevertButton();
            }
        }
    }
    #endregion

    #region PublicFunctions
    public bool SaveTrigger(bool trigger)
    {
        isPromp = trigger;
        return isPromp;
    }
    #region ButtonControl
    public void KeepChangeButton()
    {
        //Quality Changed
        savedQualityLevel = SettingReferences.Instance.qualityDD.value;
        //Play mode Changed
        savedMode = SettingReferences.Instance.modeDD.value;
        //Resolution Changed
        savedResolution = SettingReferences.Instance.resolutionDD.value;

        ResetState();

        popupSave.Hide();
    }

    public void RevertButton()
    {

        ResetDropDownSetting(SettingReferences.Instance.qualityDD, CONSTANT.PP_QUALITY, savedQualityLevel);
        QualitySettings.SetQualityLevel(savedQualityLevel);
        ResetDropDownSetting(SettingReferences.Instance.modeDD, CONSTANT.PP_MODE, savedMode);
        ResetDropDownSetting(SettingReferences.Instance.resolutionDD, CONSTANT.PP_RESOLUTION_INDEX, savedResolution);

        ResetState();

        popupSave.Hide();
    }
    #endregion

    #endregion

    #region PrivateFunctions
    private void ResetDropDownSetting(TMP_Dropdown dd, string key, int value)
    {
        if (dd.value != value)
        {
            dd.value = value;
            PlayerPrefs.SetInt(key, value);
        }
    }

    private void ResetState()
    {
        CountDownTime = maxCountDownTime;
        isPromp = false;

    }
    #endregion





}
