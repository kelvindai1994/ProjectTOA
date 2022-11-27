using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class Character : MonoBehaviour
{
    public static Character Instance;

    public NavMeshAgent character;
    public Animator animator;

    public GameObject resetPosition;
    public GameObject targetDestination;

    public float startSlide;
    [SerializeField]
    private int ID;
    private bool isSelected;

    #region UnityFunction
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        character = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }
    private void Update()
    {
        if(character.velocity != Vector3.zero)
        {
            animator.SetFloat("Speed", character.velocity.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
        }
    }
    private void FixedUpdate()
    {
        if (isSelected)
        {
            if (CalculateDistance(targetDestination.transform) <= 0.5f)
            {
                SetRotation(targetDestination.transform);
            }
        }
        else
        {
            if (CalculateDistance(resetPosition.transform) <= 0.5f)
            {
                SetRotation(resetPosition.transform);
            }
            
        }
    }
    private void OnMouseEnter()
    {
        //Make character glow or something
        //Debug.Log("Character : " + this.ID);
    }
    #endregion




    #region PublicFunction
    public void SetDestination()
    {
        character.SetDestination(targetDestination.transform.position);
        isSelected = true;
    }
    public void ResetPosition()
    {
        character.SetDestination(resetPosition.transform.position);
        isSelected = false;
    }
    public int GetID()
    {
        return this.ID;
    }
    #endregion

    #region PrivateFunction
    private void SetRotation(Transform target)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation,  (character.angularSpeed / 100) * Time.deltaTime);
        
    }
    private float CalculateDistance(Transform target)
    {
        float distance = Vector3.Distance(character.transform.position, target.position);
        return distance;
    }
    #endregion
}
