
using UnityEngine;
using System;
using System.Collections;

public class DefendObject : MonoBehaviour
{
    public static DefendObject Instance;
    public ParticleSystem Explosion;
    public ParticleSystem Portal;
    public static Action<int> OnTakeDamge;
    public static Action<int> OnDamage;

    [SerializeField] private GameObject sphere;
    [SerializeField] private ParticleSystem[] effects;
    [SerializeField] private int maxHP;

    [Space]
    [SerializeField] private GameObject[] waves;
    private int currentHP;

    private float maxEmitRate = 100f;
    public int MaxHP => maxHP;

    private bool triggerPortal;
    private float portalDelay;
    #region UnityFunction
    private void Awake()
    {
        Instance = this;
        currentHP = maxHP;
    }
    private void OnEnable()
    {
        OnTakeDamge += TakeDamage;
    }
    private void OnDisable()
    {
        OnTakeDamge -= TakeDamage;
    }
    private void Start()
    {
        Portal.gameObject.GetComponent<BoxCollider>().enabled = false;

        for (int i = 0; i < effects.Length; i++)
        {
            var emission = effects[i].emission;
            emission.rateOverTime = maxEmitRate;
        }
        for(int i = 0; i < waves.Length; i++)
        {
            waves[i].SetActive(false);
        }
    }
    private void Update()
    {
        if (triggerPortal)
        {
            portalDelay -= Time.deltaTime;
            if(portalDelay <= 0f)
            {
                Portal.Play();
                Portal.gameObject.GetComponent<BoxCollider>().enabled = true;
            }
        }

        if (waves.Length <= 0) return;
        // final wave when objective is 10% hp
        if (currentHP <= maxHP * 0.3f)
        {
            waves[3].SetActive(true);
            return;
        }
        // third wave when objective is 30% hp
        if (currentHP <= maxHP * 0.5f)
        {
            waves[2].SetActive(true);
            return;
        }
        // second wave when objective is 50% hp
        if (currentHP <= maxHP * 0.7f)
        {
            waves[1].SetActive(true);
            return;
        }
        // first wave when objective is 70% hp
        if (currentHP <= maxHP * 0.9f)
        {
            waves[0].SetActive(true);
            return;
        }

    }
    #endregion


    #region PrivateFunction
    private void TakeDamage(int damage)
    {
        currentHP -= damage;
        OnDamage?.Invoke(currentHP);
        for (int i = 0; i < effects.Length; i++)
        {
            var emission = effects[i].emission;
            emission.rateOverTime = (int)((float)currentHP / (float)maxHP * 100);
        }
        if (currentHP <= 0)
        {
            ObjectiveExplode();
        }
    }

    private void ObjectiveExplode()
    {
        currentHP = 0;
        sphere.SetActive(false);
        this.gameObject.GetComponent<BoxCollider>().enabled = false;

        Explosion.Play();
        AudioSource explodeSound = Explosion.GetComponent<AudioSource>();
        AudioClip clip = explodeSound.clip;
        explodeSound.PlayOneShot(clip);

        triggerPortal = true;
        portalDelay = 3f;
    }
    #endregion
}
