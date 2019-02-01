using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour {

    //Must contain a trigger collider

    public int damage;

    private void Start()
    {
        this.gameObject.tag = "Hurtbox";
    }
}
