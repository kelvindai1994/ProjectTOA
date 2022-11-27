using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class HPBar : MonoBehaviour
{
    [Header("HP Bar")]
    [SerializeField] private Image hpBar;
    [SerializeField] private TMP_Text hpText;
    
    //HP Bar
    private Material mHpBar;
    private float currentWidth = 250f;
    private float maxWidth = 500f;

    
    #region UnityFunction
    private void OnEnable()
    {
        PlayerStats.OnDamage += UpdateHP;
        PlayerStats.OnHeal += UpdateHP;
        PlayerStats.OnHPChange += UpdateHP;

        PlayerStats.OnLVLChange += UpdateHPBar;
    }
    private void OnDisable()
    {
        PlayerStats.OnDamage -= UpdateHP;
        PlayerStats.OnHeal -= UpdateHP;
        PlayerStats.OnHPChange -= UpdateHP;

        PlayerStats.OnLVLChange -= UpdateHPBar;
    }
    private void Start()
    {
        mHpBar = hpBar.GetComponent<Image>().material;
        hpBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);      
    }
    #endregion

    #region PrivateFunction
    private void UpdateHP(float currentHP)
    {
        hpText.SetText("{0:0}", currentHP);
        float value = currentHP / PlayerStats.Instance.MaxHP;
        mHpBar.SetFloat("_FillLevel", value);
        
    }

    private void UpdateHPBar(int lvl)
    {
        if (hpBar.rectTransform.rect.width < maxWidth)
        {
            hpBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (currentWidth + (5 * (lvl - 1))));
        }
    }

   
    #endregion

}
