using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrowObject_HandR;
    public GameObject arrowPrefab;

    public Transform raycastOrigin;
    public Transform raycastDestination;

    public float stringForce;

    private Ray ray;
    private GameObject arrow;

    private bool aimPointCreated;
    private bool arrowCreated;
    private bool arrowDespawned;
    private bool arrowShot;

    

    #region UnityFunction
    private void Start()
    {
        arrowObject_HandR.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (aimPointCreated)
        {
            CreateAimPoint();

            aimPointCreated = false;
        }
        if (arrowCreated)
        {
            CreateArrow();

            arrowCreated = false;
        }
        if (arrowDespawned)
        {
            DespawnArrow();

            arrowDespawned = false;
        }
        if (arrowShot)
        {
            ShotArrow();

            arrowShot = false;
        }
    }

    
    #endregion

    #region PublicFunction

    //Anim Events
    public void AimPointCreateTrigger()
    {
        aimPointCreated = true;
    }
    public void ArrowCreateTrigger()
    {
        arrowCreated = true;
    }
    public void ArrowDespawnTrigger()
    {
        arrowDespawned = true;
    }
    public void ArrowShotTrigger()
    {
        arrowShot = true;    
        
    }
    #endregion

    #region PrivateFunction
    private void CreateAimPoint()
    {
        ray.origin = raycastOrigin.position;
        ray.direction = raycastDestination.position - raycastOrigin.position;
        
    }
    private void CreateArrow()
    {
        arrowObject_HandR.SetActive(true);
        
    }
    private void DespawnArrow()
    {
        arrowObject_HandR.SetActive(false);
    }
    private void ShotArrow()
    {
        arrowObject_HandR.SetActive(false);
        arrow = Instantiate(arrowPrefab, ray.origin, this.gameObject.transform.rotation);
        CheckComponent<Rigidbody>(arrow);
        CheckComponent<Arrow>(arrow);

        arrow.GetComponent<Rigidbody>().AddForce(ray.direction * 1000 * stringForce);
        //AudioManager.Instance.PlayAudio(AudioType.SFX_Player_Arrow_Fly, false, PlayerPrefs.GetFloat(CONSTANT.PP_EFFECT_VOLUME) / 4);
    }
    private void CheckComponent<T>(GameObject Ob) where T : Component
    {
        if (Ob.GetComponent<T>() != null) return;
        
        Ob.AddComponent<T>();
    }

    
    #endregion


}
