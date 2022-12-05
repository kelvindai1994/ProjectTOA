using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SettingManager : MonoBehaviour
{

    #region UnityFunctions
    private void Start()
    {
        ChangeControlGroup(false);
    }
    private void Update()
    {
        SettingReferences.Instance.masterPercentT.SetText(((int)(SettingReferences.Instance.masterS.value * 100)).ToString() + "%");
        SettingReferences.Instance.musicPercentT.SetText(((int)(SettingReferences.Instance.musicS.value * 100)).ToString() + "%");
        SettingReferences.Instance.uiPercentT.SetText(((int)(SettingReferences.Instance.uiS.value * 100)).ToString() + "%");
        SettingReferences.Instance.effectPercentT.SetText(((int)(SettingReferences.Instance.effectS.value * 100)).ToString() + "%");
    }
    #endregion

    #region PublicFunctions

    //Graphic
    public virtual void CheckValueChange(int value, int savedValue)
    {
        if (value != savedValue)
        {
            ShowSaveConfirm();
        }
    }
    //Sound
    public virtual void SettingGroupChange(float value, float savedValue)
    {
        if (value != savedValue)
        {
            ChangeControlGroup(true);
        }
        else
        {
            ChangeControlGroup(false);
        }
    }
    #endregion

    #region ButtonControl
    public void DefaultButton()
    {


        //Grahpic
        SettingReferences.Instance.qualityDD.value = CONSTANT.DEFAULT_QUALITY;
        SettingReferences.Instance.modeDD.value = CONSTANT.DEFAULT_MODE;
        Resolution[] res = Screen.resolutions;
        SettingReferences.Instance.resolutionDD.value = (res.Length - 15);
        //Sound
        SettingReferences.Instance.masterS.value = CONSTANT.DEFAULT_MASTER_VOLUME;
        SettingReferences.Instance.musicS.value = CONSTANT.DEFAULT_MUSIC_VOLUME;
        SettingReferences.Instance.uiS.value = CONSTANT.DEFAULT_UI_VOLUME;
        SettingReferences.Instance.effectS.value = CONSTANT.DEFAULT_EFFECT_VOLUME;

        HideSaveConfirm();
        ChangeControlGroup(false);
    }
    public void SaveButton()
    {
        PlayerPrefs.SetFloat(CONSTANT.PP_MASTER_VOLUME, AudioListener.volume);
        PlayerPrefs.SetFloat(CONSTANT.PP_MUSIC_VOLUME, SettingReferences.Instance.musicS.value);
        PlayerPrefs.SetFloat(CONSTANT.PP_UI_VOLUME, SettingReferences.Instance.uiS.value);
        PlayerPrefs.SetFloat(CONSTANT.PP_EFFECT_VOLUME, SettingReferences.Instance.effectS.value);

        ChangeControlGroup(false);
    }
    public void CancelButton()
    {
        SettingReferences.Instance.masterS.value = PlayerPrefs.GetFloat(CONSTANT.PP_MASTER_VOLUME);
        SettingReferences.Instance.musicS.value = PlayerPrefs.GetFloat(CONSTANT.PP_MUSIC_VOLUME);
        SettingReferences.Instance.uiS.value = PlayerPrefs.GetFloat(CONSTANT.PP_UI_VOLUME);
        SettingReferences.Instance.effectS.value = PlayerPrefs.GetFloat(CONSTANT.PP_EFFECT_VOLUME);

        ChangeControlGroup(false);
    }
    #endregion

    #region PrivateFunctions

    private void ShowSaveConfirm()
    {
        PopupSave popupSave = UIManager.Instance.GetExistPopup<PopupSave>();
        if (popupSave != null)
        {
            popupSave.Show(null);
        }
        else
        {
            UIManager.Instance.ShowPopup<PopupSave>();
        }
        SaveConfirm.Instance.SaveTrigger(true);
    }

    private void HideSaveConfirm()
    {
        PopupSave popupSave = UIManager.Instance.GetExistPopup<PopupSave>();
        if (popupSave != null)
        {
            popupSave.Hide();
            SaveConfirm.Instance.SaveTrigger(false);
        }
    }


    private void ChangeControlGroup(bool t)
    {
        GameManager.Instance.SetCanvas(SettingReferences.Instance.SettingNavigate, !t);
        GameManager.Instance.SetCanvas(SettingReferences.Instance.SettingConfirm, t);
    }
    #endregion



}
