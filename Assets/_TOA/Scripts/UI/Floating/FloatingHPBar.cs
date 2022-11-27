using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHPBar : MonoBehaviour
{
    public static FloatingHPBar Create(Vector3 position, Transform parent, string name)
    {
        if(AssetManager.Instance.floatingHPPfab == null)
        {
            Debug.LogError("Prefab not found !!!");
        }
        if(Camera.main == null)
        {
            Debug.LogError("Cam not found !!!");
        }
        if (parent == null)
        {
            Debug.LogError("Parent not found !!!");
        }
        
        Transform floatingHPBarTransform = Instantiate(AssetManager.Instance.floatingHPPfab, position, Camera.main.transform.rotation, parent);

        FloatingHPBar floatingHPBar = floatingHPBarTransform.GetComponent<FloatingHPBar>();
        floatingHPBar.enemyName.SetText(name);

        return floatingHPBar;
    }

    public TMP_Text enemyName;
    public Image hpBar;

    #region UnityFunction
    private void Update()
    {
        this.gameObject.transform.LookAt(Camera.main.transform.position);
    }
    #endregion

    #region PublicFunction
    public void UpdateHP(float amount)
    {
        hpBar.fillAmount -= amount;
        if(hpBar.fillAmount <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    #endregion

}
