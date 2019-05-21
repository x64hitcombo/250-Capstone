using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour {

    //Must contain a trigger collider

    public int damage;
    PlayerController playerController;

    public void Awake()
    {
        playerController = GameObject.Find("/!PlayerController/PlayerController").GetComponent<PlayerController>();
    }

    private void Start()
    {
        //this.gameObject.tag = "Hurtbox";
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hitbox")
        {
            other.gameObject.GetComponent<HitBox>().Ouch(damage);
        }
    }
}
