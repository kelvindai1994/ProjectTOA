using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;

public class Skill4 : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCrosshair;
    [Header("UI")]
    public Image coolDownMask;
    public TMP_Text timerText;
    [Space]
    [Header("Skills Info")]
    public int skillID;
    public string skillName;
    public GameObject skillShotEffect;
    public GameObject skillCastPrefab;
    public GameObject skillPrefab;
    public float castTime;
    public float maxCoolDown;
    [Space]
    [Header("Skill Marker")]
    public GameObject arrowRainMarker;
    public LayerMask collidingLayer = ~0;


    private Animator animator;

    private float coolDownDuration;
    private bool isCoolDown;

    private AudioSource soundComponent;
    private AudioClip clip;
    private AudioSource soundComponentCast;
    public bool IsCoolDown => isCoolDown;

    #region UnityFunction
    private void Start()
    {       
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        animator = player.GetComponent<Animator>();
        arrowRainMarker.SetActive(false);

        coolDownDuration = maxCoolDown;
        timerText.enabled = false;
        coolDownMask.enabled = false;
        timerText.SetText(coolDownDuration.ToString());
        coolDownMask.fillAmount = 1f;
    }
    private void Update()
    {
        if(player == null )
        {
            player = GameObject.FindGameObjectWithTag("Player");
            animator = player.GetComponent<Animator>();
        }
        GetInput();
        ApplyCoolDown();
    }
    #endregion

    #region PrivateFunction
    private void GetInput()
    { 
        if (!animator.GetBool("isEquip")) return;
        if (animator.GetBool("isRMPressed")) return;
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (isCoolDown)
            {
                Debug.Log("Skill is not ready yet");
                return;
            }
            StartCoroutine(Precast());
        }
    }
    private void ApplyCoolDown()
    {
        if (!isCoolDown)
        {
            return;
        }
        coolDownDuration -= Time.deltaTime;
        if (coolDownDuration > 0)
        {
            timerText.enabled = true;
            coolDownMask.enabled = true;
            timerText.SetText(Mathf.RoundToInt(coolDownDuration).ToString());
            coolDownMask.fillAmount = coolDownDuration / maxCoolDown;
        }
        else
        {
            isCoolDown = false;
            timerText.enabled = false;
            coolDownMask.enabled = false;
            coolDownDuration = maxCoolDown;
            coolDownMask.fillAmount = 1f;
        }
    }

    private IEnumerator Precast()
    {
        if (skillCastPrefab == null)
        {
            Debug.LogError("Skill not found !!!");
            yield return null;
        }
        if (skillPrefab && arrowRainMarker)
        {
            playerCrosshair.SetActive(false);
            arrowRainMarker.SetActive(true);
            while (true)
            {
                var forwardCamera = Camera.main.transform.forward;
                forwardCamera.y = 0.0f;
                Ray ray = new(Camera.main.transform.position + new Vector3(0, 2, 0), Camera.main.transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, collidingLayer))
                {
                    arrowRainMarker.transform.SetPositionAndRotation(hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal) * Quaternion.LookRotation(forwardCamera));
                }
                else
                {
                    playerCrosshair.SetActive(false);
                    arrowRainMarker.SetActive(false);
                }
                if (Input.GetMouseButtonDown(0))
                {
                    isCoolDown = true;

                    //Cast Skill
                    animator.SetBool("CanMove", false);
                    playerCrosshair.SetActive(true);
                    arrowRainMarker.SetActive(false);
                    //Play animation
                    animator.SetTrigger("RainArrow");

                    
                    GameObject shotEffect = Instantiate(skillShotEffect, player.transform);
                    //Play Shot Effect Sound
                    soundComponentCast = shotEffect.GetComponent<AudioSource>();
                    if (soundComponentCast != null)
                    {
                        clip = soundComponentCast.clip;
                        soundComponentCast.PlayOneShot(clip);
                    }
                    //Play Shot Effect
                    shotEffect.GetComponent<ParticleSystem>().Play();
                    

                    //Play Cast Effect
                    GameObject castEffect = Instantiate(skillCastPrefab, player.transform);
                    //skillCastPrefab.transform.SetPositionAndRotation(player.transform.position, player.transform.rotation);
                    castEffect.GetComponent<ParticleSystem>().Play();
                    yield return new WaitForSeconds(castTime);

                    //Play Skill Effect
                    //GameObject skillEffect = Instantiate(skillPrefab, hit.point, Quaternion.LookRotation(forwardCamera), null);
                    skillPrefab.transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(forwardCamera));
                    skillPrefab.transform.SetParent(null);
                    skillPrefab.GetComponent<ParticleSystem>().Play();
                    if (skillPrefab.GetComponent<AudioSource>())
                    {
                        soundComponent = skillPrefab.GetComponent<AudioSource>();
                        clip = soundComponent.clip;
                        soundComponent.PlayOneShot(clip);
                    }

                    //Reset state
                    animator.SetBool("CanMove", true);              
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
