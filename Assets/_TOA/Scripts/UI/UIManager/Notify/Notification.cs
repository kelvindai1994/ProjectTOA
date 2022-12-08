using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Notification : BaseNotify
{
    public bool isShow;
    //Panel
    [Space]
    [Header("Panel")]
    public GameObject panel;
    public CanvasGroup panelAlpha;
    public Image panelImage;
    //Icon
    [Space]
    [Header("Icon")]
    public Image icon;
    private float iconStartTime = 0f;
    private float iconEndTime = 1f;
    //Text
    [Space]
    [Header("Text")]
    public GameObject textResPos;
    public GameObject textHolder;
    public TMP_Text text;
    public float textSpeed;
    private bool triggerText;

    private const float MAX_DURATION = 10f;
    private float duration;

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
        DefendObject.OnDamage += UpdateText;
    }
    private void OnDisable()
    {
        DefendObject.OnDamage -= UpdateText;
    }
    private void Start()
    {

        panelAlpha = panel.GetComponent<CanvasGroup>();
        panelAlpha.alpha = 0f;

        panelImage = panel.GetComponent<Image>();
        panelImage.fillAmount = 0f;

        duration = MAX_DURATION;
    }
    private void Update()
    {
        if (!isShow) return;
        AppearTime();
        PanelAnimation();
        IconAnimation();
        TextAnimation();
    }
    #endregion

    #region PrivateFunction
    private void UpdateText(int amount)
    {
        this.Show(null);
        float Percentage =  (float) amount / (float) DefendObject.Instance.MaxHP  * 100;
        if(Percentage <= 0)
        {
            panelAlpha.alpha = 1f;
            isShow = true;
            text.SetText($"OBJECTIVE HAS BEEN DESTROYED");
            return;
        }
        if(Percentage <= 5)
        {
            panelAlpha.alpha = 1f;
            isShow = true;
            text.SetText($"OBJECTIVE HAS %{(int)Percentage} HP LEFT");
            return;
        }
        if(Percentage <= 10)
        {
            panelAlpha.alpha = 1f;
            isShow = true;
            text.SetText($"OBJECTIVE HAS %{(int)Percentage} HP LEFT");
            return;
        }
        if(Percentage <= 25)
        {
            panelAlpha.alpha = 1f;
            isShow = true;
            text.SetText($"OBJECTIVE HAS %{(int)Percentage} HP LEFT");
            return;
        }
        if(Percentage <= 50)
        {
            panelAlpha.alpha = 1f;
            isShow = true;
            text.SetText($"OBJECTIVE HAS %{(int)Percentage} HP LEFT");
            return;
        }       
    }
    private void AppearTime()
    {
        if (duration <= 0f)
        {
            isShow = false;
            triggerText = false;
            panelAlpha.alpha = 0f;
            panelImage.fillAmount = 0f;
            textHolder.transform.position = textResPos.transform.position;
            duration = MAX_DURATION;
        }
        duration -= Time.deltaTime;
        
    }
    private void TextAnimation()
    {
        if (triggerText)
        {        
            textHolder.transform.position += textSpeed * Time.deltaTime * Vector3.right;
        }
    }
    private void PanelAnimation()
    {
        if (duration > MAX_DURATION / 2f)
        {
            panelImage.fillAmount += 0.5f * Time.deltaTime;      
        }
        else
        {
            panelImage.fillAmount -= 0.3f * Time.deltaTime;
        }
        if (panelImage.fillAmount >= 1f)
        {
            triggerText = true;
        }
    }
    private void IconAnimation()
    {
        iconStartTime += Time.deltaTime;
        if (iconStartTime < iconEndTime / 2)
        {
            icon.transform.localScale += 1f * Time.deltaTime * Vector3.one;
        }
        else
        {
            icon.transform.localScale -= 1f * Time.deltaTime * Vector3.one;
        }
        if(iconStartTime >= iconEndTime)
        {
            iconStartTime = 0f;
        }
    }
    #endregion
}
