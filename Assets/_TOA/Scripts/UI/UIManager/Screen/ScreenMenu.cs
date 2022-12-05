using UnityEditor;
using UnityEngine;

public class ScreenMenu : BaseScreen
{
    private int maxScrollSensitivity = 20;
    private int minScrollSensitivity = 0;
    #region UnityFunctions
    private void Start()
    {
        MenuReferences.Instance.MenuViewSR.scrollSensitivity = maxScrollSensitivity;

        GameManager.Instance.SetCanvas(MenuReferences.Instance.MenuCanvas, true);
        GameManager.Instance.SetCanvas(MenuReferences.Instance.PlaymodeCanvas, false);

        //getting the default postion of menu container
        MenuReferences.Instance.DefaultMenuPositionT.position = MenuReferences.Instance.MenuContainerRT.transform.position;
    }
    #endregion

    #region PublicFunctions

    #region ButtonControl
    public void NewGameButton()
    {
        MenuReferences.Instance.MenuViewSR.content = MenuReferences.Instance.PlaymodeContainerRT;
        MenuReferences.Instance.MenuViewSR.scrollSensitivity = minScrollSensitivity;

        GameManager.Instance.SetCanvas(MenuReferences.Instance.MenuCanvas, false);
        GameManager.Instance.SetCanvas(MenuReferences.Instance.PlaymodeCanvas, true);

    }
    public void SettingButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowPopup<PopupSetting>();
            UIManager.Instance.HideAllScreens();
        }
    }


    public void BackButton()
    {
        MenuReferences.Instance.MenuViewSR.content = MenuReferences.Instance.MenuContainerRT;
        MenuReferences.Instance.MenuViewSR.scrollSensitivity = maxScrollSensitivity;

        //default position
        MenuReferences.Instance.MenuContainerRT.transform.position = MenuReferences.Instance.DefaultMenuPositionT.position;

        GameManager.Instance.SetCanvas(MenuReferences.Instance.MenuCanvas, true);
        GameManager.Instance.SetCanvas(MenuReferences.Instance.PlaymodeCanvas, false);
    }


    public void ExitButton()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    #endregion

    #endregion





}
