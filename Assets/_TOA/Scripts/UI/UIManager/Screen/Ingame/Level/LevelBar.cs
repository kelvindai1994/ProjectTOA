using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LevelBar : MonoBehaviour
{
    [SerializeField] private Image lvlBar;
    [SerializeField] private TMP_Text lvlText;
    private Material mLvlBar;
    private void OnEnable()
    {
        PlayerStats.OnEXPGain += UpdateExp;
        PlayerStats.OnLVLChange += UpdateLevel;
    }
    private void OnDisable()
    {
        PlayerStats.OnEXPGain -= UpdateExp;
        PlayerStats.OnLVLChange -= UpdateLevel;
    }
    private void Start()
    {
        mLvlBar = lvlBar.GetComponent<Image>().material;
    }

    private void UpdateExp(float currentExp)
    {
        float value = currentExp / PlayerStats.Instance.MaxEXP;
        mLvlBar.SetFloat("_FillLevel", value);
    }
    private void UpdateLevel(int currentLvl)
    {
        lvlText.SetText(currentLvl.ToString());
    }
}
