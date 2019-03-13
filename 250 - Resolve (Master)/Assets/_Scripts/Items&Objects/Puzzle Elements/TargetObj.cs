using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObj : MonoBehaviour {
    public bool objectHit = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            objectHit = true;
            this.GetComponent<PuzzleObject>().confirm = true;
        }
    }
}
