using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    private ScreenMenu menu;
    private PopupSetting setting;
    private PopupPause pause;
    private PopupSave save;

    #region UnityFunctions
    private void Start()
    {
        menu = UIManager.Instance.GetExistScreen<ScreenMenu>();
        setting = UIManager.Instance.GetExistPopup<PopupSetting>();
        save = UIManager.Instance.GetExistPopup<PopupSave>();
        pause = UIManager.Instance.GetExistPopup<PopupPause>();
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
        else
        {
            HideSaveConfirm();
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
    public void CloseButton()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.Menu)
        {
            setting.Hide();
            menu.Show(null);
        }
        else
        {
            setting.Hide();
            if(pause == null)
            {
                UIManager.Instance.ShowPopup<PopupPause>();
            }
            else
            {
                pause.Show(null);
            }    
        }
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
    #endregion

    #region PrivateFunctions

    private void ShowSaveConfirm()
    {
        if(save == null)
        {
            UIManager.Instance.ShowPopup<PopupSave>();
        }
        else
        {
            save.Show(null);
        }
        SaveConfirm.Instance.SaveTrigger(true);
    }

    private void HideSaveConfirm()
    {
        if (save == null)
        {
            UIManager.Instance.ShowPopup<PopupSave>();
            UIManager.Instance.HideAllPopups();
        }
        else
        {
            save.Show(null);
        }
        SaveConfirm.Instance.SaveTrigger(false);
    }


    private void ChangeControlGroup(bool t)
    {
        GameManager.Instance.SetCanvas(SettingReferences.Instance.SettingNavigate, !t);
        GameManager.Instance.SetCanvas(SettingReferences.Instance.SettingConfirm, t);
    }
    #endregion



}
