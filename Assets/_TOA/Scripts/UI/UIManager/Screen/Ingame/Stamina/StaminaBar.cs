using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Image staBar;
    [SerializeField] private TMP_Text staText;

    private float currentWidth = 250f;
    private float maxWidth = 500f;

    private Material mStaBar;

    private void OnEnable()
    {
        PlayerStats.OnSTAChange += UpdateStamina;

        PlayerStats.OnLVLChange += UpdateStaminaBar;
    }
    private void OnDisable()
    {
        PlayerStats.OnSTAChange -= UpdateStamina;

        PlayerStats.OnLVLChange -= UpdateStaminaBar;
    }

    private void Start()
    {
        mStaBar = staBar.GetComponent<Image>().material;
        staBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
    }
    
    private void UpdateStamina(float currentStamina)
    {
        staText.SetText("{0:0}", currentStamina);
        float value = currentStamina / PlayerStats.Instance.MaxSTA;
        mStaBar.SetFloat("_FillLevel", value);
    }
    private void UpdateStaminaBar(int lvl)
    {
        if (staBar.rectTransform.rect.width < maxWidth)
        {
            staBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (currentWidth + (1 * (lvl - 1))));
        }
    }
}
