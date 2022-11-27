using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class NotifyLoading : BaseNotify
{
    public static NotifyLoading Instance;

    public Slider progressBar;
    public TMP_Text loadingText; 
    #region ParentOVerride
    public override void Hide()
    {
        base.Hide();
    }
    public override void Show(object data)
    {
        base.Show(data);
    }
    #endregion

    #region UnityFunction
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region PublicFunction
    public void Load(int Index)
    {
        StartCoroutine(LoadProgress(Index));
    }
    #endregion

    #region PrivateFunction
    private IEnumerator LoadProgress(int Index)
    {
        var load = SceneManager.LoadSceneAsync(Index);
        while (!load.isDone)
        {
            progressBar.value = load.progress;
            loadingText.text = string.Format("Loading Enviroments: {0}", progressBar.value * 100f);
            yield return null;
        }
        UIManager.Instance.HideAllNotify();
    }
    #endregion
}
