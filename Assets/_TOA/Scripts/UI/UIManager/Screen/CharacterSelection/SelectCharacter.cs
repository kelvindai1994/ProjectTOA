using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectCharacter : MonoBehaviour
{
    public static SelectCharacter Instance;
    public GameObject[] characters;
    public Camera mCamera;
    [SerializeField]
    private int SelectedCharacter = -1;

    #region UnityFunction
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        mCamera = Camera.main;
        if (PlayerPrefs.HasKey("SelectedCharacter"))
        {
            PlayerPrefs.DeleteKey("SelectedCharacter");
        }
    }
    private void Update()
    {
        CharacterSelecting();
        UpdateCharPosition();
    }
    #endregion
    #region PublicFunction
    public int GetSelectedCharacter()
    {
        return this.SelectedCharacter;
    }
    #endregion
    #region PrivateFunction
    private void CharacterSelecting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (IsPointerOverUIObject())
                {
                    return;
                }
                if (hitInfo.collider.gameObject.CompareTag("Player"))
                {
                    if(hitInfo.collider.gameObject.GetComponent<Character>() == null)
                    {
                        Debug.LogError("Missing Character Script !!!");
                        return;
                    }
                    SelectedCharacter = hitInfo.collider.gameObject.GetComponent<Character>().GetID(); 
                }
                else if (hitInfo.collider.gameObject.CompareTag("Enviroment"))
                {
                    Debug.Log(hitInfo.collider.gameObject.name);
                    SelectedCharacter = -1;
                }
            }
            
        }
    }
    private void UpdateCharPosition()
    {
        if (PlayerPrefs.HasKey("SelectedCharacter"))
        {
            if (SelectedCharacter == PlayerPrefs.GetInt("SelectedCharacter")) return;
            characters[PlayerPrefs.GetInt("SelectedCharacter")].GetComponent<Character>().ResetPosition();
            PlayerPrefs.DeleteKey("SelectedCharacter");
        }

        if(SelectedCharacter != -1)
        {
            if (PlayerPrefs.HasKey("SelectedCharacter"))
            {
                characters[PlayerPrefs.GetInt("SelectedCharacter")].GetComponent<Character>().ResetPosition();
            }
            characters[SelectedCharacter].GetComponent<Character>().SetDestination();
            PlayerPrefs.SetInt("SelectedCharacter", SelectedCharacter);
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
    #endregion


}
