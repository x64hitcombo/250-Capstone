using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    PlayerController playerController;

    public Transform playerPos;

    #region  Facing Variables
    [SerializeField] public float rotateSpeed;
    [SerializeField] public bool isMoving;
    [SerializeField] private bool action = false;
    public Camera mainCamera;

    int yRot = 0;
    #endregion

    #region Animator Variables
    bool isRunning = false;
    Animator anim;
    #endregion

    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        anim = gameObject.GetComponent<Animator>();
    }


    void Update()
    {
        FindDirection();
        if (isMoving) FaceDirection();
        else if (action == false) FaceMouse();
        ResetPos();

    }


    public void MoveSpeed(float speed)
    {
        anim.SetFloat("moveSpeed", speed);
    }

    private void FaceDirection()
    {    
        transform.eulerAngles = new Vector3(0, yRot, 0);
    }

    private void FaceMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, -angle + 45, 0));
    }

    private void FindDirection()
    {

        float vert = (Input.GetAxis("Vertical"));
        float hori = (Input.GetAxis("Horizontal"));



        if (vert > 0 && hori == 0) yRot = -45;
        else if (vert < -0 && hori == 0) yRot = 125;
        else if (vert == 0 && hori > 0) yRot = 45;
        else if (vert == 0 && hori < -0) yRot = -125;
        else if (vert > 0 && hori < 0) yRot = -70;
        else if (vert > 0 && hori > 0) yRot = 0;
        else if (vert < 0 && hori < 0) yRot = 180;
        else if (vert < 0 && hori > 0) yRot = 67;
    }

    public void LookAtMouse()
    {
        isMoving = false;
    }

    public void Pickup()
    {
        anim.SetBool("action", true);
        anim.SetTrigger("pickup");
        playerController.movement = false;
        anim.SetInteger("moveSpeed", 0);
        action = true;
    }

    public void DisableAction()
    {
        anim.SetBool("action", false);
        playerController.movement = true;
        action = false;

    }

    public void GoToBed()
    {
        anim.SetBool("action", true);
        anim.SetBool("sleep", true);
    }

    public void Consume()
    {
        anim.SetBool("action", true);
        anim.SetTrigger("consuming");
        playerController.movement = false;
    }

    public void ResetPos()
    {
        gameObject.transform.position = playerPos.transform.position;
    }



}
