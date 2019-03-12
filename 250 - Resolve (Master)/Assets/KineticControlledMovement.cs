using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticControlledMovement : MonoBehaviour
{
    public float movementSpeed = 4f;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        #region Movement
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //transform.position += Vector3.left * movementSpeed * Time.deltaTime;
            moveDirection += -mainCamera.transform.right;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            //transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            moveDirection += mainCamera.transform.right;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            //transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0;
            moveDirection += cameraForward;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            //transform.position += Vector3.back * movementSpeed * Time.deltaTime;
            Vector3 cameraBackward = -mainCamera.transform.forward;
            cameraBackward.y = 0;
            moveDirection += cameraBackward;
        }

        transform.position += moveDirection * Time.deltaTime * movementSpeed;
        #endregion
    }
}
