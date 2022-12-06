using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("Arrow References")]
    public GameObject arrowObject_HandR;
    public GameObject arrowPrefab;
    public GameObject fakeArrowPrefab;
    [Header("Bow References")]
    public GameObject rightHand;
    public GameObject bowResetPullPoint;
    public GameObject bowPullPoint;
    [Header("Aim point")]
    public Transform raycastOrigin;
    public Transform raycastDestination;

    public float stringForce;

    private Ray ray;
    private GameObject arrow;
    private GameObject fakeArrow;

    private bool aimPointCreated;
    private bool isStringPulled;
    private bool isStringReleased;
    private bool arrowCreated;
    private bool arrowDespawned;
    private bool arrowShot;

    private bool fakeArrowCreated;
    

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
        if (fakeArrowCreated)
        {
            ShotFakeArrow();

            fakeArrowCreated = false;
        }
        
        if(isStringPulled)
        {
            bowPullPoint.transform.SetParent(rightHand.transform, false);
            bowPullPoint.transform.SetPositionAndRotation(rightHand.transform.position, Quaternion.identity);
        }
        if (isStringReleased)
        {
            bowPullPoint.transform.SetParent(bowResetPullPoint.transform, false);
            bowPullPoint.transform.SetPositionAndRotation(bowResetPullPoint.transform.position, Quaternion.identity);
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
    public void StringPullTrigger()
    {
        isStringReleased = false;
        isStringPulled = true;
    }
    public void StringReleaseTrigger()
    {
        isStringReleased = true;
    }
    public void PullStringSound()
    {
        AudioManager.Instance.PlayAudio(AudioType.SFX_Player_Bow_Pull, false, PlayerPrefs.GetFloat(CONSTANT.PP_EFFECT_VOLUME) / 10f);
    }
    public void ReleaseStringSound()
    {
        isStringReleased = true;
        AudioManager.Instance.PlayAudio(AudioType.SFX_Player_Bow_Release, false, PlayerPrefs.GetFloat(CONSTANT.PP_EFFECT_VOLUME) / 10f);
    }

    public void FakeArrowShotTrigger()
    {
        fakeArrowCreated = true;    
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
        arrow = Instantiate(arrowPrefab, ray.origin, this.gameObject.transform.rotation);
        CheckComponent<Rigidbody>(arrow);
        CheckComponent<Arrow>(arrow);

        arrow.GetComponent<Rigidbody>().AddForce(1000 * stringForce * ray.direction);
        //AudioManager.Instance.PlayAudio(AudioType.SFX_Player_Arrow_Fly, false, PlayerPrefs.GetFloat(CONSTANT.PP_EFFECT_VOLUME) / 4);
    }
    private void ShotFakeArrow()
    {
        fakeArrow = Instantiate(fakeArrowPrefab, 
            new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1.5f, this.gameObject.transform.position.z), 
            this.gameObject.transform.rotation);    
        fakeArrow.GetComponent<Rigidbody>().AddForce(1800 * stringForce * this.gameObject.transform.forward);


    }
    private void CheckComponent<T>(GameObject Ob) where T : Component
    {
        if (Ob.GetComponent<T>() != null) return;
        
        Ob.AddComponent<T>();
    }

    
    #endregion


}
