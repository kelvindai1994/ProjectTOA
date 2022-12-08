using UnityEngine.SceneManagement;
using UnityEngine;

public class BGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int ScnIndex = SceneManager.GetActiveScene().buildIndex;
        if (UIManager.HasInstance)
        {
            if(ScnIndex == (int)SceneIndex.Menu)
            {
                AudioManager.Instance.PlayAudio(AudioType.ST_Menu, true, PlayerPrefs.GetFloat(CONSTANT.PP_MUSIC_VOLUME), 1.5f);
            }
            if(ScnIndex == (int)SceneIndex.TownMap)
            {
                AudioManager.Instance.StopAudio(AudioType.ST_Menu, false, PlayerPrefs.GetFloat(CONSTANT.PP_MUSIC_VOLUME), 0f);
                AudioManager.Instance.PlayAudio(AudioType.ST_Town, true, PlayerPrefs.GetFloat(CONSTANT.PP_MUSIC_VOLUME), 1.5f);
            }
            if (ScnIndex == (int)SceneIndex.Map1)
            {
                AudioManager.Instance.StopAudio(AudioType.ST_Town, false, PlayerPrefs.GetFloat(CONSTANT.PP_MUSIC_VOLUME), 0f);
                AudioManager.Instance.PlayAudio(AudioType.ST_Map1, true, PlayerPrefs.GetFloat(CONSTANT.PP_MUSIC_VOLUME), 1.5f);
            }
        }
    }


}
