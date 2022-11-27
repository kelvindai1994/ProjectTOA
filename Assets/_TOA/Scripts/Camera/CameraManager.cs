using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Camera m_Camera;
    private void Start()
    {
        m_Camera = Camera.main;
        m_Camera.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.Menu)
        {
            m_Camera.gameObject.SetActive(false);
        }
        else
        {
            m_Camera.gameObject.SetActive(true);
        }

    }
}
