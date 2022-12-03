using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;
public class NotifyDeath : BaseNotify
{
    [SerializeField] private Image backGround;

    [Space]
    [Header("Text")]
    [SerializeField] private CanvasGroup textCanvasGroup;
    [SerializeField] private TMP_Text txt;

    [Space]
    [Header("Button")]
    [SerializeField] private CanvasGroup btnCanvasGroup;


    private bool isDead;
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
        PlayerStats.OnDamage += PlayerDied;
    }
    private void OnDisable()
    {
        PlayerStats.OnDamage -= PlayerDied;
    }
    private void Start()
    {
        this.Hide();
        textCanvasGroup.alpha = 0f;
        btnCanvasGroup.alpha = 0f;
    }
    private void Update()
    {
        if (!isDead) return;
        AnimateTextAppear();
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
            UIManager.Instance.ShowScreen<ScreenMenu>();
        }
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
    }
    public void ExitButton()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    #endregion

    #region PrivateFunction

    private void PlayerDied(float amount)
    {
        float value = amount / PlayerStats.Instance.MaxHP;
        if(value <= 0f)
        {
            this.Show(null);
            UnlockCursor();
            isDead = true;
        }
    }
    private void AnimateTextAppear()
    {
        textCanvasGroup.alpha += 0.5f * Time.deltaTime;
        if(textCanvasGroup.alpha >= 1f)
        {
            textCanvasGroup.alpha = 1f;
            btnCanvasGroup.alpha += 0.5f * Time.deltaTime;
            if (btnCanvasGroup.alpha >= 1f)
            {
                btnCanvasGroup.alpha = 1f;
                Color color = backGround.color;
                color.a += 5f * Time.deltaTime;
                backGround.color = color;
            }
        }
    }
    private void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    
    #endregion
}
