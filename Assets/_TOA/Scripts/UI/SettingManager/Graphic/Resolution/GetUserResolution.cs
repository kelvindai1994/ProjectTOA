using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GetUserResolution : MonoBehaviour
{
    public TMP_Dropdown dd;

    private Resolution[] resolutions;
    private List<string> res_DropOptions;


    private void Awake()
    {
        resolutions = Screen.resolutions;
        res_DropOptions = new List<string>();
    }
    private void Start()
    {
        dd.ClearOptions();
        for (int i = 14; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "X" + resolutions[i].height + " , " + resolutions[i].refreshRate + "hz";
            res_DropOptions.Add(option);
        }
        dd.AddOptions(res_DropOptions);
    }
}
