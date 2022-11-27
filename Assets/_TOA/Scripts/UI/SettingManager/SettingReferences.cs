using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class SettingReferences : MonoBehaviour
{
    public static SettingReferences Instance { get; set; }

    [Header("Graphic Setting Dropdowns")]
    public TMP_Dropdown qualityDD;
    public TMP_Dropdown modeDD;
    public TMP_Dropdown resolutionDD;

    [Header("Sound Setting Sliders")]
    public Slider masterS;
    public Slider musicS;
    public Slider uiS;
    public Slider effectS;

    [Header("Sound Setting Percentages")]
    public TMP_Text masterPercentT;
    public TMP_Text musicPercentT;
    public TMP_Text uiPercentT;
    public TMP_Text effectPercentT;

    [Header("Setting Navigate Canvas")]
    public CanvasGroup SettingNavigate;
    public CanvasGroup SettingConfirm;
    private void Awake()
    {
        Instance = this;
    }

}
