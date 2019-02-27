using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isBed : MonoBehaviour
{
    public CapsuleCollider player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>();    
    }

    // Update is called once per frame
    void OntriggerStay(Collider other)
    {
        if (other == player)
        {
            player.GetComponent<ManagePlayerStats>().byBed = true;
        }
        Debug.Log("Yo");
    }

}
