using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrossHair : MonoBehaviour
{
    public static PlayerCrossHair Instance;
    public GameObject playerCrossHair;

    private bool isTrigger;
    private bool isCrit;

    private const float SCALE_TIMER_MAX = 0.25f;
    private float scaleTimer;
    #region UnityFunction
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        playerCrossHair.SetActive(false);
        scaleTimer = SCALE_TIMER_MAX;
    }
    private void Update()
    {
        if (isTrigger)
        {
            playerCrossHair.SetActive(true);
            AnimateCrosHair();
        }
    }
    #endregion

    #region PublicFunction
    public void SetTrigger(bool trigger, bool crit)
    {
        isTrigger = trigger;
        isCrit = crit;
    }
    #endregion

    #region PrivateFunction
    private void AnimateCrosHair()
    {
        if (!isCrit)
        {
            if (scaleTimer > SCALE_TIMER_MAX * 0.5f)
            {
      
                playerCrossHair.transform.localScale += 3f * Time.deltaTime * Vector3.one;
            }
            else
            {
                playerCrossHair.transform.localScale -= 3f * Time.deltaTime * Vector3.one;
            }
        }
        else
        {
            if (scaleTimer > SCALE_TIMER_MAX * 0.5f)
            {

                playerCrossHair.transform.localScale += 10f * Time.deltaTime * Vector3.one;
            }
            else
            {
                playerCrossHair.transform.localScale -= 10f * Time.deltaTime * Vector3.one;
            }
        }
        
        scaleTimer -= Time.deltaTime;
        if (scaleTimer <= 0f)
        {
            playerCrossHair.SetActive(false);
            playerCrossHair.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            isTrigger = false;
            scaleTimer = SCALE_TIMER_MAX;
        }
    }
    #endregion
}
