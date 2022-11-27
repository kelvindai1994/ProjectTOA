using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenIngame : BaseScreen
{
    [Header("CrossHair")]
    public GameObject playerCrosshair;

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

}
