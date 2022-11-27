using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMoveControler : AnimBase
{
    [Range(0, 1)] public float dampTime;
    [Range(10, 50)] public float smoothTimeEquip;
    [Range(10, 50)] public float turnSpeed;
    [Header("INPUT KEYS")]
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private KeyCode dodgeKey;
    [SerializeField] private KeyCode sprintKey;
    [SerializeField] private KeyCode toggleWalkKey;

    [Header("MOVEMENT PARAMETERS")]
    //Jump
    public float jumpHeight;
    public float jumpDamp;
    public float gravity;
    //Move
    public float groundSpeed;
    public float stepDownOffset;

    public bool isRMPressed;
    //In air
    public float airControl;

    private CharacterController characterController;
    private Vector3 rootmotion;
    private Vector3 currentVelocity;
    private Vector3 playerInput;
    private Vector3 velocity;

    //Jump
    private bool isJump;
    //Walk
    private bool isWalk;                                    
    //Camera
    Camera mainCamera;
  
    #region ParrentOverride
    public override void Start()
    {
        base.Start();
        mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    #endregion

    #region UnityFunction
    void Update()
    {     
        MoveControl();
        SprintControl();

        SetAnimatorMove();
        SetAnimatorSlide();

        if (animator.GetBool("CanJump"))
        {
            if (Input.GetKeyDown(jumpKey))
            {
                JumpControl();
            }
        }
        if (animator.GetBool(ParamAnim_CanRotation))
        {
            RotationChar();
        }


        TestFunction();
    }
    private void OnAnimatorMove()
    {
        rootmotion = animator.deltaPosition;
    }
    private void FixedUpdate()
    {
        if (isJump)
        {
            UpdateInAir();
        }
        else
        {
            UpdateOnGround();
        }

    }
    #endregion

    #region PrivateFunction

    //Character Move
    private void MoveControl()
    {
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.z = Input.GetAxis("Vertical");

        currentVelocity = playerInput;

        if (Input.GetKeyDown(toggleWalkKey)) isWalk = !isWalk;

        if (isWalk) currentVelocity = playerInput / 2;

    }
    //Character Sprint
    private void SprintControl()
    {
        if(PlayerStats.Instance.CurrentSTA > 0)
        {
            if (!animator.GetBool(ParamAnim_CanSprint)) return;
            if (currentVelocity != Vector3.zero && Input.GetKey(sprintKey))
            {
                PlayerStats.OnSprint();
                currentVelocity = playerInput * 2;
            }
        }   
    }
    //Update Character While on Ground
    private void SetAnimatorMove()
    {
        //Move State
        if (!animator.GetBool(ParamAnim_isEquip))
        {
            animator.SetFloat(ParamAnim_MoveState, 0f);
            animator.SetFloat(ParamAnim_Velocity, currentVelocity.magnitude, dampTime, Time.deltaTime);
        }
        else
        {
            isRMPressed = false;
            animator.SetFloat(ParamAnim_MoveState, 1f);
            animator.SetFloat(ParamAnim_Velocity_X, currentVelocity.x, dampTime, Time.deltaTime);
            animator.SetFloat(ParamAnim_Velocity_Z, currentVelocity.z, dampTime, Time.deltaTime);

            if (Input.GetMouseButton(1))
            {
                isRMPressed = true;
                animator.SetFloat(ParamAnim_Velocity_X, currentVelocity.x, dampTime, Time.deltaTime);
                animator.SetFloat(ParamAnim_Velocity_Z, currentVelocity.z, dampTime, Time.deltaTime);
            }
        }
        animator.SetBool("isRMPressed", isRMPressed);
    }
    private void UpdateOnGround()
    {
        Vector3 stepForwardAmount = rootmotion * groundSpeed;
        Vector3 stepDownAmount = Vector3.down * stepDownOffset;
        characterController.Move(stepForwardAmount + stepDownAmount);
        rootmotion = Vector3.zero;

        if (!characterController.isGrounded)
        {
            SetInAir(0);
        }
    }
    //Character Slide
    private void SetAnimatorSlide()
    {
        if (PlayerStats.Instance.CurrentSTA >= PlayerStats.Instance.DodgeSta)
        {
            if (animator.GetBool(ParamAnim_CanSlide) && animator.GetBool(ParamAnim_isEquip))
            {
              
                if (Input.GetKey(KeyCode.W))
                {
                    SlideInput(ParamAnim_SlideTriggerF);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    SlideInput(ParamAnim_SlideTriggerB);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    SlideInput(ParamAnim_SlideTriggerL);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    SlideInput(ParamAnim_SlideTriggerR);
                }
                else if (Input.GetKeyDown(dodgeKey))
                {
                    PlayerStats.OnDodge();
                    animator.SetTrigger(ParamAnim_SlideTriggerB);
                }
               
                
            }
        }     
    }
    private void SlideInput(int paramAnim)
    {
        if (!characterController.isGrounded) return;
        if (Input.GetKeyDown(dodgeKey))
        {
            PlayerStats.OnDodge();
            animator.SetTrigger(paramAnim);
        }
    }
    //Character Jump
    private void JumpControl()
    {
        if (!isJump)
        {
            float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
            SetInAir(jumpVelocity);
        }
    }
    private void UpdateInAir()
    {
        velocity.y -= gravity * Time.fixedDeltaTime;
        if (!animator.GetBool(ParamAnim_isEquip))
        {
            characterController.Move(Time.fixedDeltaTime * velocity);
        }
        else
        {
            Vector3 displacement = velocity * Time.fixedDeltaTime;
            displacement += CalculateAirControl();
            characterController.Move(displacement);
        }
        isJump = !characterController.isGrounded;
        rootmotion = Vector3.zero;
    }
    private void SetInAir(float jumpVelocity)
    {
        isJump = true;
        velocity = groundSpeed * jumpDamp * animator.velocity;
        velocity.y = jumpVelocity;
    }
    private Vector3 CalculateAirControl()
    {
        return ((transform.forward * playerInput.y) + (transform.right * playerInput.x)) * (airControl / 100);
    }

    //Character rotate
    private void RotationChar()
    {
        if (!animator.GetBool(ParamAnim_isEquip))
        {
            playerInput = Quaternion.AngleAxis(mainCamera.transform.rotation.eulerAngles.y, Vector3.up) * playerInput;
            if (playerInput != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(playerInput, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 480 * Time.deltaTime);
            }
        }
        else
        {
            float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
        }

    }









    private void TestFunction()
    {
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Debug.Log("Take Damage !!!");
            PlayerStats.OnTakeDamage(100);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Debug.Log("Exp Gained !!!");
            PlayerStats.OnKill(100);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            EnemyHealth.OnTakeDamage(50,true);
        }
    }
    #endregion




}
