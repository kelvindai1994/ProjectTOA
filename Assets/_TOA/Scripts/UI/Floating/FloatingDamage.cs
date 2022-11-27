using TMPro;
using UnityEngine;

public class FloatingDamage : MonoBehaviour
{
    public static FloatingDamage Create(Vector3 position, float damage, bool isCrit = false, bool isMiss = false)
    {
        Transform floatingDamageTransform = Instantiate(AssetManager.Instance.floatingDamagePfab, position, Camera.main.transform.rotation);

        FloatingDamage floatingDamage = floatingDamageTransform.GetComponent<FloatingDamage>();

        floatingDamage.SetUp(damage, isCrit, isMiss);

        return floatingDamage;
    }
    private static int sortingLayer;

    private TextMeshPro txt;
    private const float DISAPPEAR_TIMER_MAX = 1f;
    private float disappearTimer;
    private float disappearSpeed;
    private Vector3 moveVector;

    private Color txtColor;
    #region UnityFunction
    private void Awake()
    {
        txt = GetComponent<TextMeshPro>();
    }
    private void Update()
    {
        UpdateFloatingText();
    }
    #endregion

    #region PublicFunction

    #endregion

    #region PrivateFunction
    private void SetUp(float dmgAmount, bool isCrit, bool isMiss)
    {
        sortingLayer++;
        txt.sortingOrder = sortingLayer;
        if(isMiss)
        {
            txt.SetText("MISS");
        }
        else
        {
            txt.SetText(dmgAmount.ToString());
        }
        if (!isCrit)
        {
            txt.fontSize = 2f;
            txtColor = Color.white;
        }
        else
        {
            txt.fontSize = 3f;
            txtColor = Color.yellow;
        }
        txt.color = txtColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;
        disappearSpeed = 5f;
        moveVector = new Vector3(0, 1f, -0.5f);
        
    }

    private void UpdateFloatingText()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 2f * Time.deltaTime;

        if(disappearTimer > DISAPPEAR_TIMER_MAX * .5f)
        {
            transform.localScale += 1f * Time.deltaTime * Vector3.one;
        }
        else
        {
            transform.localScale -= 1f * Time.deltaTime * Vector3.one;
        }
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            txtColor.a -= disappearSpeed * Time.deltaTime;
            txt.color = txtColor;
            if (txtColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    #endregion
}
