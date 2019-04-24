using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    PlayerController playerController;

    #region  Facing Variables
    [SerializeField] public float rotateSpeed;
    [SerializeField] public bool isMoving;
    public Camera mainCamera;
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
        if (isMoving) FaceDirection();
        else FaceMouse();
    }


    public void MoveSpeed(float speed)
    {
        anim.SetFloat("moveSpeed", speed);
    }

    private void FaceDirection()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 newPosition = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.LookAt(newPosition + transform.position);
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
    }

    public void DisableAction()
    {
        anim.SetBool("action", false);
        playerController.movement = true;
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

}
