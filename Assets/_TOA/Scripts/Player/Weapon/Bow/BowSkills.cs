using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowSkills : MonoBehaviour
{
    public Animator animator;
    [Header("Ground Marker")]
    public GameObject arrowRainMarker;
    public LayerMask collidingLayer = ~0;
    [Space]
    [Header("Skill Shot Effect")]
    public GameObject[] skillShotEffect;

    [Space]
    [Header("Skills")]
    public GameObject[] skillPrefabs;
    private bool isCasting = false;

    [Space]
    [Header("Skills Cast")]
    public GameObject[] skillCastPrefabs;

    [Space]
    [Header("Skills Cast Time")]
    public float[] castTime;

    [Space]
    public GameObject playerCrosshair;

    //Sounds
    private AudioSource soundComponent;
    private AudioClip clip;
    private AudioSource soundComponentCast;
    #region UnityFunction
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        if (playerCrosshair == null)
        {
            playerCrosshair = GameObject.FindGameObjectWithTag("Player_Crosshair");
        }
        arrowRainMarker.SetActive(false);
    }
    void Update()
    {
        //if (!animator.GetBool("isEquip")) return; //Check if equip weapon yet
        //if (animator.GetBool("isRMPressed")) return;
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    StartCoroutine(Precast(0));
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    StartCoroutine(Precast(1));
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    StartCoroutine(Precast(2));
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    Skill4 skill4 = this.gameObject.GetComponent<Skill4>();
        //    StartCoroutine(Precast(3));
        //}
    }
    #endregion

    #region PrivateFunction
    private IEnumerator Precast(int skill)
    {
        if (skillCastPrefabs[skill] == null)
        {
            Debug.LogError("Skill not found !!!");
            yield return null;
        }
        if (skillPrefabs[skill] && arrowRainMarker)
        {
            playerCrosshair.SetActive(false);
            arrowRainMarker.SetActive(true);
            while (true)
            {
                var forwardCamera = Camera.main.transform.forward;
                forwardCamera.y = 0.0f;
                RaycastHit hit;
                Ray ray = new Ray(Camera.main.transform.position + new Vector3(0, 2, 0), Camera.main.transform.forward);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, collidingLayer))
                {
                    arrowRainMarker.transform.position = hit.point;
                    arrowRainMarker.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * Quaternion.LookRotation(forwardCamera);
                }
                else
                {
                    playerCrosshair.SetActive(false);
                    arrowRainMarker.SetActive(false);
                }
                if (Input.GetMouseButtonDown(0) && !isCasting)
                {
                    //Cast Skill
                    animator.SetBool("CanMove", false);
                    isCasting = true;
                    playerCrosshair.SetActive(true);
                    arrowRainMarker.SetActive(false);
                    //Play animation
                    if (skill == 3)
                    {
                        animator.SetTrigger("RainArrow");
                    }
                    //Play sound
                    soundComponentCast = skillShotEffect[skill].GetComponent<AudioSource>();
                    if (soundComponentCast != null)
                    {
                        clip = soundComponentCast.clip;
                        soundComponentCast.PlayOneShot(clip);
                    }
                    //Play particles
                    skillShotEffect[skill].GetComponent<ParticleSystem>().Play();
                    skillCastPrefabs[skill].GetComponent<ParticleSystem>().Play();
                    yield return new WaitForSeconds(castTime[skill]);
                    //Set skill position
                    skillPrefabs[skill].transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(forwardCamera));
                    skillPrefabs[skill].transform.parent = null;
                    skillPrefabs[skill].GetComponent<ParticleSystem>().Play();
                    if (skillPrefabs[skill].GetComponent<AudioSource>())
                    {
                        soundComponent = skillPrefabs[skill].GetComponent<AudioSource>();
                        clip = soundComponent.clip;
                        soundComponent.PlayOneShot(clip);
                    }

                    //Reset state
                    animator.SetBool("CanMove", true);
                    isCasting = false;
                    yield break;
                }
                else if (Input.GetMouseButtonDown(1) || !animator.GetBool("isEquip"))
                {
                    //Cancel Skill
                    arrowRainMarker.SetActive(false);
                    playerCrosshair.SetActive(true);
                    yield break;
                }
                yield return null;
            }
        }
    }

    #endregion
    
}
