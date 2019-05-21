using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingGamePath : MonoBehaviour
{
    public GameObject spawnerNode;
    public List<GameObject> path;
    public List<GameObject> testPath;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnerNode != null)
        {
            path.Add(spawnerNode);
            testPath.Add(spawnerNode);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject node in path)
        {
            foreach (GameObject connector in node.GetComponent<HackingGameController>().connectors)
            {
                if (connector.GetComponent<DetectConnection>().isConnected)
                {
                    if (!path.Contains(connector.GetComponent<DetectConnection>().connectorTouching.transform.parent.gameObject))
                    {
                        testPath.Add(connector.GetComponent<DetectConnection>().connectorTouching.transform.parent.gameObject);
                    }
                }
            }
        }

        foreach (GameObject node in testPath)
        {
            if (!path.Contains(node))
            {
                path.Add(node);
            }
        }

        foreach (GameObject node in path)
        {
            bool keep = false;
            foreach (GameObject connector in node.GetComponent<HackingGameController>().connectors)
            {
                if (keep == false)
                {
                    if (connector.GetComponent<DetectConnection>().isConnected)
                    {
                        keep = true;
                    }
                }
            }

            if (keep == false)
            {
                path.Remove(node);
            }
        }
    }
}
