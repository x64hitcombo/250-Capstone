using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    /// <summary>
    /// Detects Combat collisions and applies the damage to
    /// an accomponied Health component
    ///     GameObject Requires:
    ///     - Health Script Component
    ///     - Collider
    ///     - Death Particle System
    /// </summary>

    public float maxHealth;
    public float curHealth;
    FireDamage fire;

    public bool canTakeDamage = true;
    public bool mortalityCheck = true; //False is dead

    public Text healthTxt;

    public string hurtboxTag = "hurtbox";
    public string bodyTag = "Player";

    ParticleSystem deathParticle;
    public GameObject drop; //drops an object on death

    public int flashTime;
    public Material flashMat;
    MeshRenderer flashMesh;

    public void Start()
    {
        curHealth = maxHealth;
        deathParticle = GetComponent<ParticleSystem>();
        flashMesh = this.GetComponent<MeshRenderer>();
    }

    public void Update()
    {
        if (healthTxt != null)
        {
            healthTxt.text = curHealth.ToString();
        }
    }

    //Detects contact with hurtbox
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(hurtboxTag))
        {
            TakeDamage(other.GetComponent<Hurtbox>().damage);
        }
    }

    //Applies damage to the health variable
    public void TakeDamage(float amount)
    {
        if (canTakeDamage)
        {
            curHealth -= amount; //Damage dealing for(var n = 0; n < 5; n++)
            if (curHealth <= 0) //Checks for deads
            {
                {
                    curHealth = 0;
                    mortalityCheck = false;
                }
            }

            if (!mortalityCheck)
            {
                Die();
            }
            else
            {
                StartCoroutine(FlashMesh());
            }
        }
    }

    public void Die()
    {
        if (deathParticle != null)
        {
            flashMesh.enabled = false;
            //Destroys all adjacent objects
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            //destroys after playing death particle and drops death object
            Destroy(this.gameObject, deathParticle.main.duration);
            if (!deathParticle.isPlaying)
            {
                deathParticle.Play();
                if (drop != null)
                {
                    Instantiate(drop, transform.position, transform.rotation);
                }
            }
        }
        else
        {
            Debug.Log("Please Attach a deathparticle");
        }
    }

    IEnumerator FlashMesh()
    {
        for (var n = 0; n < flashTime; n++)
        {
            canTakeDamage = false;
            flashMesh.enabled = true;
            yield return new WaitForSeconds(0.1f);
            flashMesh.enabled = false;
            yield return new WaitForSeconds(0.1f);
        }
        flashMesh.enabled = true;
        canTakeDamage = true;
    }
}