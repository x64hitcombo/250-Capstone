using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMaster : MonoBehaviour
{
    public GameObject[] puzzleObjects;
    //public bool allConfirm = true;

    public GameObject reward;
    public Transform rewardDropSpot;
    public GameObject outAnimate;
    private bool isCreated;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //VictoryCheck();

        if (VictoryCheck())
        {
            Debug.Log("Puzzle Complete");
            AllComplete();
        }
    }

    public bool VictoryCheck()
    {
        for (int i = 0; i < puzzleObjects.Length; i++)
        {
            if (puzzleObjects[i].GetComponent<PuzzleObject>().confirm == false)
            {
                return false;
            }
        }

        return true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (!VictoryCheck() && other.gameObject.tag == "Player")
        {
            foreach (GameObject pObject in puzzleObjects)
            {
                Debug.Log("All reset, captain!");

                pObject.gameObject.transform.position = pObject.GetComponent<PuzzleObject>().ogTrans;
            }
        }
    }

public void AllComplete()
    {
        if (outAnimate)
        {
            outAnimate.GetComponent<Animator>().SetBool("objEvent", true);
            Debug.Log("Yeah hahaa"); 
        }

        if (reward)
        {
            if (rewardDropSpot)
            {
                if (!isCreated)
                {
                    Instantiate(reward, rewardDropSpot);
                    isCreated = true;
                }
            }
        }
    }
}

