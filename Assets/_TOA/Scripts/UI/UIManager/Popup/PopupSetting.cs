using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSetting : BasePopup
{
    #region ParentOverride

    public override void Hide()
    {
        base.Hide();
    }
    private void Start()
    {
        Hide();
    }
    #endregion


}
