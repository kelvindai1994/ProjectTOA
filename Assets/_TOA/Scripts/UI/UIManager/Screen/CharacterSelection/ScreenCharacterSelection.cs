using TMPro;
using UnityEngine;

public class ScreenCharacterSelection : BaseScreen
{
    public TMP_Text heroName;
    public TMP_Text heroDescription;

    #region UnityFunction
    private void Start()
    {
        heroName.enabled = false;
        heroDescription.enabled = false;
    }
    private void Update()
    {
        if (!UIManager.HasInstance) return;
        if (!SelectCharacter.Instance) return;
        if (SelectCharacter.Instance.GetSelectedCharacter() == -1)
        {
            heroName.enabled = false;
            heroDescription.enabled = false;
            return;
        }
        else
        {
            UpdateText();
        }
    }
    #endregion

    #region PublicFunction
    #region ButtonControl
    public void StartGameButton()
    {
        if (!PlayerPrefs.HasKey("SelectedCharacter"))
        {
            Debug.LogError("No character selected !!!");
            return;
        }
        UIManager.Instance.ShowNotify<NotifyLoading>();
        NotifyLoading.Instance.Load((int)SceneIndex.TownMap);
        UIManager.Instance.HideAllScreens();
        UIManager.Instance.ShowScreen<ScreenIngame>();
    }
    public void BackButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowNotify<NotifyLoading>();
            NotifyLoading.Instance.Load((int)SceneIndex.Menu);
            UIManager.Instance.HideAllScreens();
            UIManager.Instance.ShowScreen<ScreenMenu>();
        }
    }
    #endregion
    #endregion

    #region PrivateFunction
    private void UpdateText()
    {
        heroName.enabled = true;
        heroDescription.enabled = true;
        switch (SelectCharacter.Instance.GetSelectedCharacter())
        {
            case 0:
                {
                    TextsChange("TALIYA", ("Daughter of the forest" + "\n" + "Mastered the way of bow"));
                    break;
                }
            case 1:
                {
                    TextsChange("TANAKA", ("Sword Master" + "\n" + "He who hailed from the East in search of stronger foe"));
                    break; 
                }
        }
    }
    private void TextsChange(string name, string description)
    {
        heroName.SetText(name);
        heroDescription.SetText(description);
    }
    #endregion



}
