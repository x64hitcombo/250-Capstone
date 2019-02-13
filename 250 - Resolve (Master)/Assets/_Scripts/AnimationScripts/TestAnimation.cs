using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour
{
    private Animator anim;
    public bool pickUpBool1 = false;
    public bool pickUpBool2 = false;
    public bool walkBool = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            pickUpBool1 = true;
            if (pickUpBool1 == true)
            {
                anim.SetBool("pickUp1", true);
                anim.SetBool("pickUp2", false);
                anim.SetBool("isWalking", false);
                pickUpBool1 = false;
                walkBool = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            pickUpBool2 = true;
            if (pickUpBool2 == true)
            {
                anim.SetBool("pickUp2", true);
                anim.SetBool("pickUp1", false);
                anim.SetBool("isWalking", false);
                pickUpBool1 = false;
                walkBool = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            walkBool = true;
            if (walkBool == true)
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("pickUp1", false);
                anim.SetBool("pickUp2", false);
                pickUpBool1 = false;
                pickUpBool2 = false;
            }
        }

    }
    }
