
using UnityEngine;
using System;
public class DefendObject : MonoBehaviour
{
    public static DefendObject Instance;

    public static Action<int> OnTakeDamge;
    public static Action<int> OnDamage;

    [SerializeField] private GameObject sphere;
    [SerializeField] private ParticleSystem[] effects;
    [SerializeField] private int maxHP;
    
    private int currentHP;

    private float maxEmitRate = 100f;
    private float minEmitRate = 0f;
    public int MaxHP => maxHP;
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
        for (int i = 0; i < effects.Length; i++)
        {
            var emission = effects[i].emission;
            emission.rateOverTime = maxEmitRate;
        }
    }
    #endregion


    #region PrivateFunction
    private void TakeDamage(int damage)
    {
        currentHP -= damage;
        OnDamage?.Invoke(currentHP);
        if (currentHP <= 0)
        {
            currentHP = 0;
            sphere.SetActive(false);
        }
        for (int i = 0; i < effects.Length; i++)
        {
            var emission = effects[i].emission;
            emission.rateOverTime = (int)((float)currentHP / (float)maxHP * 100);
        }
        
    }
    #endregion
}
