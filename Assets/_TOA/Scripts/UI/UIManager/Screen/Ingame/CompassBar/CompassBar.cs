using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
public class CompassBar : MonoBehaviour
{
    public static CompassBar Instance;
    public static Action<bool> ResetCompass;

    public GameObject enemyIconPrefab;
    public List<EnemyMarker> enemyMarkers = new();

    public RawImage compassImage;
    public Camera mCam;

    [SerializeField] private float maxDistance;

    private float compassUnit;
    private int enemyID;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        ResetCompass += ClearCompass;
    }
    private void OnDisable()
    {
        ResetCompass -= ClearCompass;
    }
    private void Start()
    {
        mCam = Camera.main;
        compassUnit = compassImage.rectTransform.rect.width / 360f;
    }

    private void Update()
    {       
        if(mCam == null) mCam = Camera.main;
        compassImage.uvRect = new Rect(mCam.transform.localEulerAngles.y / 360f, 0, 1f, 1f);

        foreach(EnemyMarker enemyMarker in enemyMarkers)
        {
            if (enemyMarker != null) 
            {
                if(enemyMarker.image == null) continue;
                enemyMarker.image.rectTransform.anchoredPosition = GetPosOnCompass(enemyMarker);


                //Change icon size base on distance
                float dst = Vector2.Distance(new Vector2(mCam.transform.position.x, mCam.transform.position.z), enemyMarker.Position);
                float scale = 0f;
                if (dst <= maxDistance)
                {
                    scale = 1f - (dst / maxDistance);
                }
                enemyMarker.image.rectTransform.localScale = Vector3.one * scale;
            }     
        }
    }

    public void AddEnemyMarker(EnemyMarker marker)
    {
        enemyID++;
        marker.gameObject.name += enemyID.ToString();

        GameObject newEnemyMarker = Instantiate(enemyIconPrefab, compassImage.transform);
        newEnemyMarker.name = marker.gameObject.name;
        marker.image = newEnemyMarker.GetComponent<Image>();
        marker.image.sprite = marker.icon;

        enemyMarkers.Add(marker);
    }
    public void RemoveEnemyMarker(EnemyMarker marker)
    {
        enemyMarkers.Remove(marker);
        for (int i = 0; i < compassImage.transform.childCount; i++)
        {
            GameObject child = compassImage.gameObject.transform.GetChild(i).gameObject;

            if(child.name == marker.gameObject.name)
            {
                Destroy(child);
                return;
            }
        }    
    }

    private Vector2 GetPosOnCompass(EnemyMarker marker)
    {
        Vector2 camPos = new(mCam.transform.position.x, mCam.transform.position.z);
        Vector2 camFwd = new(mCam.transform.forward.x, mCam.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.Position - camPos, camFwd);

        return new Vector2(compassUnit * angle, 0);
    }

    private void ClearCompass(bool isReset = false)
    {
        if (isReset)
        {
            enemyMarkers.Clear();
            if (compassImage.transform.childCount > 0)
            {
                for (int i = 0; i < compassImage.transform.childCount; i++)
                {
                    GameObject child = compassImage.gameObject.transform.GetChild(i).gameObject;
                    Destroy(child);
                }
            }
        }
    }

}
