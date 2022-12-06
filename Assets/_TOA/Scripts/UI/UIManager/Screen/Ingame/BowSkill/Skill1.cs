using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;

public class Skill1 : MonoBehaviour
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
    public Transform SkillPoint;
    public GameObject skillShotEffect;
    public GameObject skillCastPrefab;

    public float castTime;
    public float maxCoolDown;

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

        coolDownDuration = maxCoolDown;
        timerText.enabled = false;
        coolDownMask.enabled = false;
        timerText.SetText(coolDownDuration.ToString());
        coolDownMask.fillAmount = 1f;
    }
    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");          
        }
        if(player != null)
        {
            animator = player.GetComponent<Animator>();
            SkillPoint = GameObject.FindGameObjectWithTag("SkillShotPoint").transform;
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (isCoolDown)
            {
                AudioManager.Instance.PlayAudio(AudioType.SFX_Player_Skill_CoolDown, false, PlayerPrefs.GetFloat(CONSTANT.PP_EFFECT_VOLUME) / 10f, 0);
                return;
            }
            StartCoroutine(FrontAttack());


           
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
    public IEnumerator FrontAttack()
    {
        if (playerCrosshair && animator.GetBool("CanCast"))
        {
            isCoolDown = true;

            var forwardCamera = Camera.main.transform.forward;
            forwardCamera.y = 0.0f;
            //Play Animation
            animator.SetTrigger("FireArrow");

            //Play Cast Effect
            GameObject castEffect = Instantiate(skillCastPrefab, SkillPoint.transform);
            castEffect.GetComponent<ParticleSystem>().Play();
            soundComponentCast = castEffect.GetComponent<AudioSource>();
            if(soundComponentCast != null)
            {
                clip = soundComponentCast.clip;
                soundComponentCast.PlayOneShot(clip);
            }
            Destroy(castEffect, 1.5f);

            //Play Shot Effect
            skillShotEffect.transform.SetPositionAndRotation(SkillPoint.transform.position, Quaternion.LookRotation(forwardCamera));
            skillShotEffect.GetComponent<ParticleSystem>().Play();

            //Instantitate arrow

            yield return null;

        }
    }
    #endregion
}
