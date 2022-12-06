using UnityEngine.UI;
using UnityEngine;

public class HPPotion : MonoBehaviour
{
    public Image potionFill;
    public Image potionCrack;
    public int healAmount;

    private const float MAX_DELAY_TIMER = 1.5f;
    private float fillDelayTimer;

    #region UnityFunction
    private void Start()
    {
        fillDelayTimer = MAX_DELAY_TIMER;

        potionFill.fillAmount = 0f;
        potionCrack.fillAmount = 1f;

    }
    #endregion

    #region PublicFunction
    private void Update()
    {
        PotionRegen();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(potionFill.fillAmount < 1f)
            {
                //Play error sound
                Debug.Log("Potion is unavailable !!!");
            }
            else if(potionFill.fillAmount >= 1f)
            {
                potionFill.fillAmount = 0f;
                PlayerStats.OnTakeDamage(-healAmount);
            }
        }
    }
    #endregion

    #region PrivateFunction
    private void PotionRegen()
    {
        if(potionFill.fillAmount >= 1f)
        {
            potionFill.fillAmount = 1f;
            return;
        }
        fillDelayTimer -= Time.deltaTime;
        if(fillDelayTimer <= 0f)
        {
            potionFill.fillAmount += 0.1f;
            potionCrack.fillAmount -= 0.1f;
            fillDelayTimer = MAX_DELAY_TIMER;
        }
    }
    #endregion
}
