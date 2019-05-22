using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    private int health = 0;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
        React();
    }

    public void React()
    {
        if (health > 0) Hit();
        else if (health <= 0) Die();
    }

    public void Hit()
    {
 
    }

    public void Die()
    {
        if (gameObject.tag == "Animal")
        {
            anim.SetTrigger("Action");
            anim.SetBool("Dead", true);
        }
    }
}
