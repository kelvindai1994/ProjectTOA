using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private bool isPaused;
    #region ParentOverride
    protected override void Awake()
    {
        base.Awake();
    }
    #endregion

    #region UnityFunctions
    private void Start()
    {
        if (!UIManager.HasInstance) return;
        UIManager.Instance.ShowNotify<NotifyLoading>();
        NotifyLoading.Instance.Load((int)SceneIndex.Menu);

        UIManager.Instance.ShowScreen<ScreenMenu>();
        UIManager.Instance.ShowPopup<PopupSetting>();
        UIManager.Instance.HideAllPopups();
    }

    private void Update()
    {
        EscButton();

        MouseControl();



        ChangeSceneTest();
    }
    #endregion

    #region PublicFunctions

    public void SetCanvas(CanvasGroup canvas, bool t)
    {
        canvas.alpha = t ? 1 : 0;
        canvas.blocksRaycasts = t;
        canvas.interactable = t;
    }

    public void SetGameFlowTime(float deltaTime)
    {
        Time.timeScale = deltaTime;
        if (deltaTime > 0)
        {
            isPaused = false;
        }
        else
        {
            isPaused = true;
        }
    }
    #endregion


    #region PrivateFunction

    private void EscButton()
    {
        if (!UIManager.HasInstance) return;
        if (SceneManager.GetActiveScene().buildIndex <= 1)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ScreenMenu menu = UIManager.Instance.GetExistScreen<ScreenMenu>();
                // Return to Menu while in Setting while not Ingame
                if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.Menu)
                {
                    menu.Show(null);
                    menu.BackButton();
                    UIManager.Instance.HideAllNotify();
                    UIManager.Instance.HideAllPopups();
                }
                // Return to Menu while selecting character
                if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.CharacterSelection)
                {
                    NotifyLoading.Instance.Load((int)SceneIndex.Menu);
                    UIManager.Instance.HideAllScreens();
                    menu.Show(null);
                }
            }
        }
        else
        {
            // Pause / Open Setting while Ingame 
            if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
            {

                UIManager.Instance.ShowPopup<PopupPause>();

                UIManager.Instance.HideAllNotify();
                UIManager.Instance.HideAllScreens();
                isPaused = true;

                SetGameFlowTime(0);
            }
            else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
            {

                UIManager.Instance.HideAllPopups();
                ScreenIngame ingame = UIManager.Instance.GetExistScreen<ScreenIngame>();

                ingame.Show(null);
                isPaused = false;

                SetGameFlowTime(1);
            }
        }
    }

    private void MouseControl()
    {
        if (SceneManager.GetActiveScene().buildIndex <= 1) 
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            if (isPaused)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }    
    }
   



    private void ChangeSceneTest()
    {
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            UIManager.Instance.ShowNotify<NotifyLoading>();
            NotifyLoading.Instance.Load((int)SceneIndex.Map1);
        }
    }
    #endregion
}