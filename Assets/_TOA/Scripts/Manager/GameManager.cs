using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : Singleton<GameManager>
{

    #region UnityFunctions
    private void Start()
    {

        if (GameManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenMenu>();
            UIManager.Instance.ShowPopup<PopupSetting>();

        }
    }
    #endregion

    #region PublicFunctions
    public bool IsPaused()
    {
        return Time.timeScale == 0 ? true : false;
    }

    public void SetCanvas(CanvasGroup canvas, bool t)
    {
        canvas.alpha = t ? 1 : 0;
        canvas.blocksRaycasts = t;
        canvas.interactable = t;
    }
    #endregion



}