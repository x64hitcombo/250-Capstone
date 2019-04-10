using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    public string[] dialogue;
    public int diaCount;

    // Text for UI (Shows what is being examined)
    public GameObject dialogueUI;

    // True/False if player is present near the object
    public bool opened = false;
    public string playerTag = "Player";

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == playerTag)
        {
            if (!opened)
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    ActivateUI();
                }
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    diaCount += 1;
                    UpdateDialogue();

                }
            }
        }
    }

    public void ActivateUI()
    {
        Debug.Log("Speaking");
        diaCount = 0;
        dialogueUI.SetActive(true);
        dialogueUI.GetComponentInChildren<Text>().text = dialogue[diaCount];
        opened = true;
    }

    public void OnTriggerExit(Collider other)
    {
        CloseWindow();
    }

    public void CloseWindow()
    {
        dialogueUI.SetActive(false);
        diaCount = 0;
        opened = false;
    }

    // Update is called once per frame
    void UpdateDialogue()
    {
        if (diaCount >= dialogue.Length)
        {
            CloseWindow();
        }

        dialogueUI.GetComponentInChildren<Text>().text = dialogue[diaCount];
    }
}
