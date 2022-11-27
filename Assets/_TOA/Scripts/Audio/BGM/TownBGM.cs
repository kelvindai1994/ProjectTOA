using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownBGM : MonoBehaviour
{
    void Start()
    {
        if (UIManager.HasInstance)
        {
            AudioManager.Instance.StopAudio(AudioType.ST_Menu, true, PlayerPrefs.GetFloat(CONSTANT.PP_MUSIC_VOLUME), 1.5f);
            AudioManager.Instance.PlayAudio(AudioType.ST_InGame, true, PlayerPrefs.GetFloat(CONSTANT.PP_MUSIC_VOLUME), 3.5f);
        }
    }

}
