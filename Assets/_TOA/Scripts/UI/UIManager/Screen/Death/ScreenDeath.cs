using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class ScreenDeath : BaseScreen
{
    [SerializeField] private Image backGround;

    [Space]
    [Header("Text")]
    [SerializeField] private CanvasGroup textCanvasGroup;
    [SerializeField] private TMP_Text txt;

    [Space]
    [Header("Button")]
    [SerializeField] private CanvasGroup btnCanvasGroup;

    private bool trigger;
    #region ParentOverride
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
    private void OnEnable()
    {
        PlayerStats.OnDeath += PlayerDieUI;
    }
    private void OnDisable()
    {
        PlayerStats.OnDeath -= PlayerDieUI;
    }
    private void Start()
    {
        trigger = true;
        textCanvasGroup.alpha = 0f;
        textCanvasGroup.interactable = false;
        btnCanvasGroup.blocksRaycasts = false;

        btnCanvasGroup.alpha = 0f;
        btnCanvasGroup.interactable = false;
        btnCanvasGroup.blocksRaycasts = false;
    }
    private void Update()
    {
        if (trigger)
        {
            AnimateTextAppear();
        }
        
    }
    #endregion

    #region PublicFunction
    public void RetryButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowNotify<NotifyLoading>();
            var currentScene = SceneManager.GetActiveScene().buildIndex;
            NotifyLoading.Instance.Load(currentScene);
            UIManager.Instance.HideAllScreens();
            UIManager.Instance.ShowScreen<ScreenIngame>();

        }
        ResetState();
    }
    public void MainMenuButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowNotify<NotifyLoading>();
            NotifyLoading.Instance.Load((int)SceneIndex.Menu);
            UIManager.Instance.HideAllScreens();
            UIManager.Instance.ShowScreen<ScreenMenu>();
        }

        ResetState();
    }
    public void ExitButton()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();

        ResetState();
    }
    #endregion

    #region PrivateFunction
    public void PlayerDieUI(bool die)
    {
        trigger = die;
    }
    private void AnimateTextAppear()
    {
        textCanvasGroup.alpha += 0.5f * Time.deltaTime;
        if (textCanvasGroup.alpha >= 1f)
        {
            textCanvasGroup.alpha = 1f;
            textCanvasGroup.interactable = true;
            btnCanvasGroup.blocksRaycasts = true;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            btnCanvasGroup.alpha += 0.5f * Time.deltaTime;
            if (btnCanvasGroup.alpha >= 1f)
            {
                btnCanvasGroup.alpha = 1f;
                btnCanvasGroup.interactable = true;
                btnCanvasGroup.blocksRaycasts = true;

                Color color = backGround.color;
                color.a += 1f * Time.deltaTime;
                backGround.color = color;


                if(color.a >= 255)
                {
                    trigger = false;
                }           
            }
        }
    }

    private void ResetState()
    {
        trigger = false;
        textCanvasGroup.alpha = 0f;
        btnCanvasGroup.alpha = 0f;

        Color color = backGround.color;
        color.a = 0f;
        backGround.color = color;

    }
    #endregion
}
