using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToNodes : MonoBehaviour
{
    public GameObject nodeReached;
    public GameObject previousNodeReached;
    public GameObject connectorReached;
    public GameObject nodeToGo;
    public GameObject hackingGamePath;

    public int pathPoint = 0;

    public float movementSpeed = 10f;

    public bool moveUp = false;
    public bool moveDown = false;
    public bool moveLeft = false;
    public bool moveRight = false;

    private Vector3 previousLocation = Vector3.zero;
    public bool testMove = true;

    // Start is called before the first frame update
    void Start()
    {
        hackingGamePath = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (testMove)
        {

            foreach (GameObject connector in nodeReached.GetComponent<HackingGameController>().connectors)
            {
                if (connector.GetComponent<DetectConnection>().sendConnector)
                {
                    Vector3 currentVelocity = (connector.transform.position - transform.position);
                    print(currentVelocity);
                    if (currentVelocity.y > 1)
                    {
                        //moving up
                        moveUp = true;
                        moveDown = false;
                        moveLeft = false;
                        moveRight = false;
                    }
                    else if (currentVelocity.y < -1)
                    {
                        //moving down
                        moveDown = true;
                        moveUp = false;
                        moveLeft = false;
                        moveRight = false;
                    }
                    else if (currentVelocity.x > 1)
                    {
                        //move right
                        moveUp = false;
                        moveDown = false;
                        moveLeft = false;
                        moveRight = true;
                    }
                    else if (currentVelocity.x < -1)
                    {
                        //move left
                        moveUp = false;
                        moveDown = false;
                        moveLeft = true;
                        moveRight = false;
                    }
                }
            }
            testMove = false;
        }
        previousLocation = transform.position;

        if (moveUp)
        {
            Vector3 position = transform.position;
            position.y += movementSpeed * Time.deltaTime;
            transform.position = position;
        }
        else if (moveDown)
        {
            Vector3 position = transform.position;
            position.y -= movementSpeed * Time.deltaTime;
            transform.position = position;
        }
        else if (moveLeft)
        {
            Vector3 position = transform.position;
            position.x -= movementSpeed * Time.deltaTime;
            transform.position = position;
        }
        else if (moveRight)
        {
            Vector3 position = transform.position;
            position.x += movementSpeed * Time.deltaTime;
            transform.position = position;
        }

        if (nodeReached == null && connectorReached == null)
        {
            Destroy(gameObject);
        }

        if (nodeReached != null && nodeReached.GetComponent<HackingGameController>() != null)
        {
            nodeReached.GetComponent<HackingGameController>().canRotate = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "UI_noSwap" && (other.gameObject.name == "RotateNode" || other.gameObject.name == "SpawnerNode"))
        {
            previousNodeReached = nodeReached;
            nodeReached = other.gameObject;
        }
        if (other.tag == "UI_noSwap" && other.gameObject.name == "Connector")
        {
            connectorReached = other.gameObject;
            previousNodeReached = nodeReached;
            nodeReached = other.gameObject.transform.parent.gameObject;
        }
        if (other.tag == "UI_noSwap" && other.gameObject.name == "NodeCenter")
        {
            testMove = true;
            transform.position = other.gameObject.transform.position;
        }
        if (other.tag == "UI_noSwap" && other.gameObject.name == "GoalNode")
        {
            other.gameObject.GetComponent<GoalNodeLock>().isLocked = true;
            other.gameObject.GetComponent<Image>().color = Color.red;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "UI_noSwap" && (other.gameObject.name == "RotateNode" || other.gameObject.name == "SpawnerNode"))
        {
            previousNodeReached = nodeReached;
            nodeReached = other.gameObject;
        }
        if (other.tag == "UI_noSwap" && other.gameObject.name == "Connector")
        {
            connectorReached = other.gameObject;
            previousNodeReached = nodeReached;
            nodeReached = other.gameObject.transform.parent.gameObject;
        }
        if (other.tag == "UI_noSwap" && other.gameObject.name == "NodeCenter")
        {
            //testMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "UI_noSwap" && other.gameObject.name == "Connector")
        {
            previousNodeReached.GetComponent<HackingGameController>().canRotate = true;
            connectorReached = null;
            nodeReached = null;
        }
        if (other.tag == "UI_noSwap" && (other.gameObject.name == "RotateNode" || other.gameObject.name == "SpawnerNode" || other.gameObject.name == "GoalNode"))
        {
            previousNodeReached.GetComponent<HackingGameController>().canRotate = true;
            nodeReached = null;
        }
    }
}
