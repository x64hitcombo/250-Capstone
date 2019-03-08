using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMaster : MonoBehaviour
{
    public GameObject[] puzzleObjects;
    public bool allConfirm = false;

    public GameObject outAnimate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        VictoryCheck();
    }

    public void VictoryCheck()
    {
        foreach (GameObject pObject in puzzleObjects)
        {
            if (pObject.GetComponent<PuzzleObject>().confirm)
            {
                allConfirm = true;
                AllComplete();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (!allConfirm && other.gameObject.tag == "Player")
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
    }
}

