using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public Animator animator;
    [Header("LEVEL PARAMETERS")]
    public static Action<float> OnKill;
    public static Action<float> OnEXPGain;
    public static Action<int> OnLVLChange;

    [SerializeField] private int level;
    [SerializeField] private float maxExp;
    [SerializeField] private float MaxExpIncreaseMulti;
    private float currentExp;

    [Header("HP PARAMETERS")]
    public static Action<float> OnTakeDamage;
    public static Action<float> OnDamage;
    public static Action<float> OnHeal;
    public static Action<float> OnHPChange;
    public static Action<bool> OnDeath;

    [SerializeField] private float maxHP;
    [SerializeField] private float maxHPIncreaseMulti;
    [SerializeField] private float HPRegenDelay;
    [SerializeField] private float HPRegenTimeTick;
    private float currentHP;
    private float baseHPRegen;
    private Coroutine regenaratingHealth;

    [Header("STAMINA PARAMETERS")]
    public static Action OnSprint;
    public static Action OnDodge;
    public static Action<float> OnSTAChange;
    public static Action OnSprintCheck;

    [SerializeField] private float maxSTA;
    [SerializeField] private float maxSTAIncreaseMulti;
    [SerializeField] private float sprintSTAConsump;
    [SerializeField] private float dodgeSTAConsump;
    [SerializeField] private float STARegenDelay;
    [SerializeField] private float STARegenTimeTick;
    public float CurrentSTA { get;private set; }
    
    private float baseSTARegen;
    private Coroutine regenaratingStamina;

    private const float BASE_MAX_EXP = 1000f;
    private const float BASE_HP = 1000f;
    private const float BASE_STA = 100f;
    public int Level => level;
    public float MaxHP => maxHP;
    public float MaxSTA => maxSTA;
    public float MaxEXP => maxExp;
    public float DodgeSta => dodgeSTAConsump;

    private bool isDead;
    private int hitCount = 0;
    #region UnityFunction
    private void OnEnable()
    {
        OnKill += ExpGain;
        OnTakeDamage += TakeDamage;
        OnSprint += UseStaminaSprint;
        OnDodge += UseStaminaDodge;
    }
    private void OnDisable()
    {
        OnKill -= ExpGain;
        OnTakeDamage -= TakeDamage;
        OnSprint -= UseStaminaSprint;
        OnDodge -= UseStaminaDodge;
    }
    private void Awake()
    {
        Instance = this;
        isDead = false;
        //Animator
        if (animator == null)
        {
            animator = this.gameObject.GetComponent<Animator>();
        }
        //Health
        currentHP = maxHP;
        baseHPRegen = maxHP * (0.5f / 100); // regen 0.5%hp of maxHp
        //Stamina
        CurrentSTA = maxSTA;
        baseSTARegen = maxSTA * (5f / 100); // regen 5% of maxSta;
    }
    private void Update()
    {
        if (this.gameObject != null)
        {  
            OnHPChange?.Invoke(currentHP);
            OnSTAChange?.Invoke(CurrentSTA);
            OnEXPGain?.Invoke(currentExp);
        }
    }
    #endregion


    #region PrivateFunction
    //Handle TakeDamage
    private void TakeDamage(float dmgAmount)
    {
        
        currentHP -= dmgAmount;

        hitCount++;
        if (hitCount >= 3)
        {
            animator.SetTrigger("Damage");
            hitCount = 0;
        }
        
        OnDamage?.Invoke(currentHP);

        if (currentHP <= 0)
            KillPlayer();
        else if (regenaratingHealth != null)
            StopCoroutine(regenaratingHealth);

        regenaratingHealth = StartCoroutine(RegenaratingHealth());
    }
    private void KillPlayer()
    {

        CharacterController characterController = gameObject.GetComponent<CharacterController>();
        AnimMoveControler animMove = gameObject.GetComponent<AnimMoveControler>();
        AnimAttackControler animAttack = gameObject.GetComponent<AnimAttackControler>();

        characterController.enabled = false;
        animMove.enabled = false;
        animAttack.enabled = false;

        currentHP = 0;
        animator.SetBool("isDead", true);

        if (regenaratingHealth != null)
            StopCoroutine(regenaratingHealth);
        isDead = true;
        OnDeath?.Invoke(isDead);

    }

    //Handle Stamina

    private void UseStaminaSprint()
    {
        if (regenaratingStamina != null)
        {
            StopCoroutine(regenaratingStamina);
            regenaratingStamina = null;
        }

        CurrentSTA -= sprintSTAConsump * Time.deltaTime;

        if (CurrentSTA <= 0)
        {
            CurrentSTA = 0;
        }

        OnSTAChange?.Invoke(CurrentSTA);

        if (CurrentSTA < maxSTA && regenaratingStamina == null)
        {
            regenaratingStamina = StartCoroutine(RegenaratingStamina());
        }
    }
    private void UseStaminaDodge()
    {
        if (regenaratingStamina != null)
        {
            StopCoroutine(regenaratingStamina);
            regenaratingStamina = null;
        }

        CurrentSTA -= dodgeSTAConsump;

        if (CurrentSTA <= 0)
        {
            CurrentSTA = 0;
        }

        OnSTAChange?.Invoke(CurrentSTA);

        if (CurrentSTA < maxSTA && regenaratingStamina == null)
        {
            regenaratingStamina = StartCoroutine(RegenaratingStamina());
        }
    }
    //Handle ExpGain and LevelUP stats
    private void ExpGain(float expAmount)
    {
        currentExp += expAmount;

        OnEXPGain?.Invoke(currentExp);
        if (currentExp >= maxExp)
        {
            LevelUp();
            HPUp();
            STAUp();


            currentExp -= maxExp;
            maxExp = BASE_MAX_EXP + (MaxExpIncreaseMulti * (level - 1));
        }
    }
    private void LevelUp()
    {
        level++;
        OnLVLChange?.Invoke(level);
    }
    private void HPUp()
    {
        maxHP = BASE_HP + (maxHPIncreaseMulti * (level - 1));

        currentHP = maxHP;

        baseHPRegen = maxHP * (0.5f / 100);

    }
    private void STAUp()
    {
        maxSTA = BASE_STA + (maxSTAIncreaseMulti * (level - 1));

        CurrentSTA = maxSTA;

        baseSTARegen = maxSTA * (5f / 100);
    }


    #region EnumeratorFunction
    private IEnumerator RegenaratingHealth()
    {
        yield return new WaitForSeconds(HPRegenDelay);
        WaitForSeconds timeToWait = new(HPRegenTimeTick);

        while (currentHP < maxHP)
        {
            currentHP += baseHPRegen;

            if (currentHP > maxHP)
                currentHP = maxHP;

            OnHeal?.Invoke(currentHP);

            yield return timeToWait;
        }

        regenaratingHealth = null;
    }

    private IEnumerator RegenaratingStamina()
    {
        yield return new WaitForSeconds(STARegenDelay);
        WaitForSeconds timeToWait = new(STARegenTimeTick);

        while(CurrentSTA < maxSTA)
        {
            CurrentSTA += baseSTARegen;

            if(CurrentSTA >= maxSTA)
                CurrentSTA = maxSTA;

            OnSTAChange?.Invoke(CurrentSTA);

            yield return timeToWait;
        }

        regenaratingStamina = null;
    }
    #endregion

    #endregion
   

   
}
