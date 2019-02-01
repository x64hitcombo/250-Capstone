﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInspection : MonoBehaviour {

    // Text for UI (Shows what is being examined)
    public GameObject inspectUI;
    public Text inspectText;

    // True/False if player is present near the object
    public bool playerView = false;

    public string playerTag = "Player";
    public string inspectString;

    public void OnTriggerEnter(Collider other)
    {
        playerView = true;
        if (other.gameObject.tag == playerTag)
        {
            enabled = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        playerView = true;
        if (other.gameObject.tag == playerTag)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Inspected");
                inspectUI.SetActive(true);
                inspectText.text = inspectString;
                enabled = true;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        inspectUI.SetActive(false);
        playerView = false;
        enabled = false;
    }
}
