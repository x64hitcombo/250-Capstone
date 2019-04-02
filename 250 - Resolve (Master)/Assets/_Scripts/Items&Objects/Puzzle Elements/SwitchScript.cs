using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchScript : MonoBehaviour {
    // True/False Switch being pressed
    public bool switchPressed = false;

    // True/False if player is present near the object
    public bool playerPresent = false;

    public enum confirmState
    {
        On,
        Off
    }

    public confirmState confirm;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerPresent = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                switchPressed = !switchPressed;
                Debug.Log("Switch Pressed");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        playerPresent = false;
    }

    public void Update()
    {
        if (confirm == confirmState.On)
        {
            if (switchPressed == true)
            {
                this.GetComponent<PuzzleObject>().confirm = true;
            }
            if (switchPressed == false)
            {
                this.GetComponent<PuzzleObject>().confirm = false;
            }
        }

        if (confirm == confirmState.Off)
        {
            if (switchPressed == false)
            {
                this.GetComponent<PuzzleObject>().confirm = true;
            }
            else
            {
                this.GetComponent<PuzzleObject>().confirm = false;

            }
        }
    }
}
