
using UnityEngine;

public class MenuBGM : MonoBehaviour
{
    void Start()
    {
        if (UIManager.HasInstance)
        {
            AudioManager.Instance.PlayAudio(AudioType.ST_Menu, true, PlayerPrefs.GetFloat(CONSTANT.PP_MUSIC_VOLUME), 1.5f);
        }
    }


}
