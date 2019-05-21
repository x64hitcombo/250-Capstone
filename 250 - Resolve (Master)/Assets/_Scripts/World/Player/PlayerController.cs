using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent (typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    #region Movement and Slope Variables
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;

    [SerializeField] private float walkSpeed, runSpeed;
    [SerializeField] private float runBuildupSpeed;
    [SerializeField] private KeyCode runKey;

    private float moveSpeed;

    [SerializeField] private float slopeforce;
    [SerializeField] private float slopeForceRayLength;

    private CharacterController charCont;
    #endregion

    public Camera mainCamera;  //Camera for face cursor
    public bool climbing = false;
    private bool canClimb;
    private Transform climbLookPosition;
    public bool canSprint = true;

    //public GameObject hackingGame;
    //public GameObject saveLoadUI;
    public float staminaLossRate;

    public float baseMovementSpeed;
    public bool movement = true;

    public bool melee = false;

    AnimatorController anim;
    PlayerInventory playerInventory;

    public float attackRate = 3f;
    private float nextAttack = 0.0f;

    private int shotStage = 0;

    private void Awake()
    {
        charCont = GetComponent<CharacterController>();
        anim = GetComponentInChildren<AnimatorController>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if(movement == true)
        {
            AnimateMovement();
            Movement();
        }
        

        if (Input.GetKey(KeyCode.LeftShift) && canSprint)
        {
            ConsumeStamina();
        }

        if (Input.GetButtonDown("Fire1") && Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;       
            Attack(1);
        }
        if (Input.GetButtonDown("Fire2")) Attack(2);
        if (Input.GetButtonUp("Fire2") && shotStage == 1) Attack(3);

    }

    #region Movement, Slope, and Animation
    private void Movement()
    {

        float horizInput = Input.GetAxis(horizontalInputName);
        float vertInput = Input.GetAxis(verticalInputName);

        Vector3 forwardMovement = mainCamera.transform.forward * vertInput;
        Vector3 rightMovement = mainCamera.transform.right * horizInput;

        charCont.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * moveSpeed);

        if ((vertInput != 0 || horizInput != 0) && OnSlope())
        {
            charCont.Move(Vector3.down * charCont.height / 2 * slopeforce * Time.deltaTime);
        }

        SetMovementSpeed();
    }

    private void SetMovementSpeed()
    {
        if (Input.GetKey(runKey) && canSprint) moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, Time.deltaTime * runBuildupSpeed);
        else moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, Time.deltaTime * runBuildupSpeed);
    }

    private bool OnSlope()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, charCont.height / 2 * slopeForceRayLength))
        {
            if (hit.normal != Vector3.up) return true;
        }
        return false;
    }

    void AnimateMovement()
    {
        float vertAxis = Input.GetAxis("Vertical");
        float horiAxis = Input.GetAxis("Horizontal");

        if (vertAxis > 0 || vertAxis < 0 || horiAxis > 0 || horiAxis < 0)
        {
            anim.isMoving = true;
            if (Input.GetButton("Sprint") && canSprint) anim.MoveSpeed(2);
            else anim.MoveSpeed(1);

        }
        else
        {
            anim.MoveSpeed(0);
            //anim.isMoving = false;
        }
    }
    #endregion

    private void DetermineClimb()
    {
        if (climbing && Input.GetKeyDown(KeyCode.E))
        {
            climbing = false;
        }
        else if (canClimb && Input.GetKeyDown(KeyCode.E))
        {
            climbing = true;
        }
    }

    private void ConsumeStamina()
    {
        ManagePlayerStats mps = GetComponent<ManagePlayerStats>();
        mps.currentStamina -= Time.deltaTime * staminaLossRate;
        if (mps.currentStamina <= 0)
        {
            canClimb = false;
            climbing = false;
        }
    }


    void Attack(int stage)
    {
        if (playerInventory.eqWeapon && stage == 1) anim.Attack();
        else if (playerInventory.eqBow && stage == 2)
        {
            shotStage = 1;
            anim.Fire(1);
        }
        else if (playerInventory.eqBow && stage == 1)
        {
            shotStage = 0;
            anim.Fire(2);
        }
        else if (stage == 3)
        {
            shotStage = 0;
            anim.Fire(3);
        }
    }






    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WallClimbZone")
        {
            canClimb = true;
            climbLookPosition = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "WallClimbZone")
        {
            canClimb = false;
            climbing = false;
            climbLookPosition = null;
        }
    }
}
