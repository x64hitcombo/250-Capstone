using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    AIController healthScript;
    void Start()
    {
        if (transform.parent.tag == "Animal")
        {
            healthScript = transform.parent.GetComponent<AIController>();
        }
    }

    
    void Update()
    {
        
    }

    public void Ouch(int damage)
    {
        healthScript.TakeDamage(damage);
    }
}
