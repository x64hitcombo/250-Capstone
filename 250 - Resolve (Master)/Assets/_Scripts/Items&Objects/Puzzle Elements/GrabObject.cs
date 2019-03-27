using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour {
    private GameObject player;
    public bool isGrabbed = false;
    public bool playerPresent = false;

    private void Start()
    {
        this.gameObject.GetComponent<PuzzleObject>().confirm = true;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerPresent = true;
            player = other.gameObject;
            if (Input.GetKeyDown(KeyCode.E))
            {
                toggleGrab();
            }

            if (isGrabbed)
            {
                this.gameObject.transform.parent = player.transform;

            }
            else
            {
                this.gameObject.transform.parent = null;

            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        playerPresent = false;
    }

    //public void objectGrab()
    //{
    //    if (!isGrabbed)
    //    {
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            isGrabbed = true;
    //            this.gameObject.transform.parent = player.transform;
    //            Debug.Log("Object grabbed");
    //        }
    //    }
    //}

    //public void ObjectLetGo()
    //{
    //    if (isGrabbed)
    //    {
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            this.gameObject.transform.parent = null;
    //            Debug.Log("Object let go");
    //        }

    //    }
    //}

    public void toggleGrab()
    {
        isGrabbed = !isGrabbed;
    }
}
