﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsTargetingBait : MonoBehaviour
{
    public bool isAnimalEating = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Animal")
        {
            isAnimalEating = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Animal")
        {
            isAnimalEating = false;
        }
    }
}
