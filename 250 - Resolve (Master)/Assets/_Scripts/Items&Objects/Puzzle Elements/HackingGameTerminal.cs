using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingGameTerminal : MonoBehaviour
{
    public GameObject screenPrefab;
    public GameObject hackingUI;
    public HackingGameScreen screen;
    public bool playerPresent = false;

    // Start is called before the first frame update
    void Update()
    {
        if (!screen)
        {
            screen = GameObject.FindGameObjectWithTag("HackingGame").GetComponent<HackingGameScreen>();
        }

        if (playerPresent && screen.isHacked)
        {
            this.GetComponent<PuzzleObject>().confirm = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hackingUI.SetActive(true);
                enabled = true;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        LoadScreen();
        playerPresent = true;
    }

    public void OnTriggerExit(Collider other)
    {
        hackingUI.SetActive(false);
        enabled = false;
        playerPresent = false;
        DeleteScreen();
    }

    public void DeleteScreen()
    {
        List<Transform> allChildren = new List<Transform>();

        if (hackingUI.transform.childCount > 0)
        {
            GameObject c = hackingUI.transform.GetChild(0).gameObject;
            Destroy(c);
        }
    }

    public void LoadScreen()
    {
        if (hackingUI.transform.childCount == 0)
        {
            Instantiate(screenPrefab, hackingUI.transform);
            Debug.Log("yeah we did it");
        }
    }
}
