using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseUIElement : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    protected UIType uiType = UIType.Unknown;
    protected bool isHide;
    protected bool isInited;

    public CanvasGroup CanvasGroup => canvasGroup;
    public UIType UIType => uiType;
    public bool IsHide => isHide;
    public bool IsInited => isInited;

    #region Events
    public void OnButtonClick()
    {
        AudioManager.Instance.PlayAudio(AudioType.SFX_UI_Click, false, PlayerPrefs.GetFloat(CONSTANT.PP_UI_VOLUME));
    }
    public void OnSliderMove()
    {
        AudioManager.Instance.PlayAudio(AudioType.SFX_UI_Slider, false, PlayerPrefs.GetFloat(CONSTANT.PP_UI_VOLUME));
    }
    #endregion

    #region VirtualFunctions
    public virtual void Show(object data)
    {
        this.gameObject.SetActive(true);
        this.isHide = false;
        SetActiveCanvasGroup(true);
    }

    public virtual void Init()
    {
        this.isInited = true;
        if (!this.gameObject.GetComponent<CanvasGroup>())
        {
            this.gameObject.AddComponent<CanvasGroup>();
        }
        this.canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        this.gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        this.isHide = true;
        SetActiveCanvasGroup(false);
    }
    #endregion


    #region PrivateFunctions
    private void SetActiveCanvasGroup(bool isActive)
    {
        if (CanvasGroup != null)
        {
            CanvasGroup.blocksRaycasts = isActive;
            CanvasGroup.interactable = isActive;
            CanvasGroup.alpha = isActive ? 1 : 0;
        }
    }
    #endregion


}
