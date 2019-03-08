using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    public GameObject pressurePlate;

    public bool objectOnPlate = false;

    public Animator animate;


	void Start ()
    {
        animate = GetComponent<Animator>();
	}
	

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Pushable")
    //    {
    //        animate.SetBool("objectOn", true);
    //        objectOnPlate = true;
    //    }

    //    if (other.gameObject.tag == "Player")
    //    {
    //        animate.SetBool("objectOn", true);
    //        objectOnPlate = true;
    //    }
    //}

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Pushable" || other.gameObject.tag == "Player")
        {
            objectOnPlate = true;
            animate.SetBool("objectOn", true);
            this.GetComponent<PuzzleObject>().confirm = true;
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pushable" || other.gameObject.tag == "Player")
        {
            objectOnPlate = false;
            animate.SetBool("objectOn", false);
        }
    }
}
