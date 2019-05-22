using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour {

    //Must contain a trigger collider

    public int damage;
    PlayerController playerController;
    private int touchedC = 0;

    public void Awake()
    {
        playerController = GameObject.Find("PlayerController 1").GetComponent<PlayerController>();
    }

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.tag == "Hitbox" && playerController.meleeAtk == true)
         {
            if (touchedC == 0)
            {
                Debug.Log("What up duck");
                other.gameObject.GetComponent<HitBox>().Ouch(damage);
                touchedC = 1;
            }
                
         }
       
    }

    private void OnTriggerExit(Collider other)
    {

        if(touchedC == 1)
        {
            touchedC = 0;
        }
    }
}
