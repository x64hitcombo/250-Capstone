using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingGameScreen : MonoBehaviour
{
    public GoalNodeLock[] goalNodes;
    public bool isHacked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goalNodes == null)
        {
            isHacked = false;
        }
        if (GoalCheck())
        {
            isHacked = true;
        }
    }

    public bool GoalCheck()
    {
        for (int i = 0; i < goalNodes.Length; i++)
        {
            if (goalNodes[i].GetComponent<GoalNodeLock>().isLocked == false)
            {
                return false;
            }
        }

        return true;
    }
}
