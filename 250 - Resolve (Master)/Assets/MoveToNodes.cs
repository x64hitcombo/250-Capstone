using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNodes : MonoBehaviour
{
    public GameObject nodeReached;
    public GameObject nodeToGo;

    public float movementSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.K))
        {
            //transform.position += Vector3.left * movementSpeed * Time.deltaTime;
            moveDirection += -transform.up;
        }
        if (Input.GetKey(KeyCode.L))
        {
            //transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            moveDirection += transform.right;
        }
        if (Input.GetKey(KeyCode.I))
        {
            //transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
            moveDirection += transform.up;
        }
        if (Input.GetKey(KeyCode.J))
        {
            //transform.position += Vector3.back * movementSpeed * Time.deltaTime;
            moveDirection += -transform.right;
        }

        transform.position += moveDirection * Time.deltaTime * movementSpeed;

        if (nodeReached == null)
        {
            //Destroy(this.gameObject);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "RotateNode" || other.gameObject.name == "SpawnerNode" || other.gameObject.name == "GoalNode")
        {
            nodeReached = other.gameObject;
        }
        else
        {
            nodeReached = null;
        }
    }
}
