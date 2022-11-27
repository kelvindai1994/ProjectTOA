
using UnityEditor;
using UnityEngine;

public class PopupPause : BasePopup
{
    private ScreenIngame ingame;
    private ScreenMenu menu;
    private PopupSetting setting;
    #region ParentOveride
    public override void Show(object data)
    {
        base.Show(data);
    }
    public override void Hide()
    {
        base.Hide();
    }
    #endregion
    #region UnityFunction
    private void Start()
    {
        ingame = UIManager.Instance.GetExistScreen<ScreenIngame>();
        menu = UIManager.Instance.GetExistScreen<ScreenMenu>();
        setting = UIManager.Instance.GetExistPopup<PopupSetting>();
    }
    #endregion
    #region PublicFunction
    public void ResumeButton()
    {
        this.Hide();
        ingame.Show(null);

        GameManager.Instance.SetGameFlowTime(1);
    }
    public void SettingButton()
    {
        this.Hide();
        setting.Show(null);
    }
    public void MenuButton()
    {
        //save data

        //return to menu
        NotifyLoading.Instance.Load((int)SceneIndex.Menu);

        UIManager.Instance.HideAllPopups();

        UIManager.Instance.ShowScreen<ScreenMenu>();
        GameManager.Instance.SetGameFlowTime(1);

    }
    public void ExitButton()
    {
        //save data

        //exit the game
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    #endregion

    #region PrivateFunction

    #endregion
}
