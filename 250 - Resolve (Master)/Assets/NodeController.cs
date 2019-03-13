using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    public bool spawnNode = false;
    public bool movingNode = false;
    public bool openAccessNode = false;
    public bool reroutingNode = false;
    public bool firewallNode = false;
    public bool lockedAccessNode = false;

    public float movementSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movingNode)
        {
            HandleMovingNode();
        }
    }

    public void HandleMovingNode()
    {
        Vector2 moveDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.I))
        {
            moveDirection.y += movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.K))
        {
            moveDirection.y -= movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.J))
        {
            moveDirection.x -= movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.L))
        {
            moveDirection.x += movementSpeed * Time.deltaTime;
        }
        transform.position += new Vector3(moveDirection.x, moveDirection.y, transform.position.z).normalized * Time.deltaTime * movementSpeed;
    }
}
