using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButtons> tabButtons;
    public List<CanvasGroup> objectToSwap;

    public TabButtons selected;

    [Header("Sprite Swap")]
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;

    #region UnityFunctions
    private void Start()
    {
        SetStartTabActive();
        SetActivePage(selected);
    }
    #endregion

    #region Events
    public void OnTabEnter(TabButtons btn)
    {
        ResetTab();
        if (selected == null || btn != selected)
        {
            btn.background.sprite = tabHover;
        }
    }

    public void OnTabSelected(TabButtons btn)
    {
        selected = btn;
        ResetTab();
        btn.background.sprite = tabActive;
        SetActivePage(btn);
        PlayerPrefs.SetInt(CONSTANT.PP_TAB, selected.index);
    }

    public void OnTabExit(TabButtons btn)
    {
        ResetTab();
    }
    #endregion

    #region PrivateFunctions
    private void ResetTab()
    {
        foreach (TabButtons btn in tabButtons)
        {
            if (selected != null && btn == selected) continue;
            btn.background.sprite = tabIdle;
        }
    }

    private void SetStartTabActive()
    {
        if (!PlayerPrefs.HasKey(CONSTANT.PP_TAB))
        {
            PlayerPrefs.SetInt(CONSTANT.PP_TAB, CONSTANT.DEFAULT_TAB);
        }
        int index = PlayerPrefs.GetInt(CONSTANT.PP_TAB);
        tabButtons[index].background.sprite = tabActive;
        selected = tabButtons[index];
    }
    private void SetActivePage(TabButtons btn)
    {
        for (int i = 0; i < objectToSwap.Count; i++)
        {
            if (i == btn.index)
            {
                GameManager.Instance.SetCanvas(objectToSwap[i], true);
            }
            else
            {
                GameManager.Instance.SetCanvas(objectToSwap[i], false);
            }
        }
    }
    #endregion

}
