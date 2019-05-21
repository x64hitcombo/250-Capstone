using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectConnection : MonoBehaviour
{
    public bool isConnected = false;
    public bool sendConnector = false;
    public bool recieveConnector = false;

    public bool canChangeConnectorStatus = true;

    public GameObject connectorTouching;

    private bool checkObject;
    private GameObject objectToCheck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isConnected && recieveConnector)
        {
            foreach(GameObject connector in transform.parent.gameObject.GetComponent<HackingGameController>().connectors)
            {
                if (connector != this.gameObject)
                {
                    connector.GetComponent<DetectConnection>().sendConnector = true;
                    connector.GetComponent<DetectConnection>().recieveConnector = false;
                }
            }
        }
        else if (!isConnected)
        {
            sendConnector = false;
            recieveConnector = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "UI_noSwap" && other.gameObject.name == "Connector")
        {
            isConnected = true;
            connectorTouching = other.gameObject;
            if (connectorTouching.GetComponent<DetectConnection>().sendConnector == true && canChangeConnectorStatus)
            {
                recieveConnector = true;
                sendConnector = false;
                objectToCheck = connectorTouching;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "UI_noSwap" && other.gameObject.name == "Connector")
        {
            isConnected = true;
            connectorTouching = other.gameObject;
            if (connectorTouching.GetComponent<DetectConnection>().sendConnector == true && canChangeConnectorStatus)
            {
                recieveConnector = true;
                sendConnector = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "UI_noSwap" && other.gameObject.name == "Connector")
        {
            isConnected = false;
            connectorTouching = null;
            if (canChangeConnectorStatus)
            {
                recieveConnector = false;
                sendConnector = false;
            }
        }
    }
}
