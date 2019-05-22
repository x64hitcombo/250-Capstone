using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class AIController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Transform target;
    public Transform homeSpawner;

    public float hunger = 100;
    public float thirst = 100;

    public float decreaseHungerBy = 1;
    public float decreaseThirstBy = 0.5f;

    public float increaseHungerBy = 50;
    public float increaseThirstBy = 50;

    public float secondsTillDecrease = 1;

    public float minHungerSearch = 50;
    public float minThirstSearch = 50;

    public float distanceFromPlayerHunt = 10f;
    public float distanceFromBait = 10f;
    public float distanceToStop = 2f;

    public float maxDistanceFromHome = 50f;

    public float wanderRange = 10f;

    public float eatingTimer = 5f;
    public float waitTimer = 5f;
    public float attackTimer = 5f;

    public bool attackPlayer = false;
    public bool hunting = false;

    public float damageDealt = 5f;

    [SerializeField]
    private float currentTimer, currentHunger, currentThirst;

    private NavMeshHit navHit;
    private Vector3 wanderTarget;

    private float waitToEat;
    private float waitToWander;
    private float waitToAttack;

    private Animator anim;
    private Vector3 lastPosition;

    #region Health Variables
    [SerializeField]
    private int health = 0;
    private bool alive = true;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentTimer = secondsTillDecrease;
        currentHunger = hunger;
        currentThirst = thirst;
        waitToEat = eatingTimer;
        waitToWander = waitTimer;
        waitToAttack = attackTimer;
        anim = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    public void FixedUpdate()
    {
        HandleTimers();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive == true)
        {
            FindTarget();
            if (target == null)
            {
                Wandering();
            }
            else if (target != null && Vector3.Distance(homeSpawner.position, target.position) <= maxDistanceFromHome)
            {
                agent.SetDestination(target.position);
                //Move Animation
                anim.SetBool("Movement", true);
                anim.SetBool("Running", true);
            }
            else if (!hunting && Vector3.Distance(gameObject.transform.position, player.transform.position) > distanceFromPlayerHunt)
            {
                Wandering();
            }

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (target != null)
                {
                    if (target.tag == "Bait")
                    {
                        waitToEat -= Time.deltaTime;
                        //Play eat/drink animation
                        //Might have a bug here -Nick
                        anim.SetTrigger("Action");
                        anim.SetTrigger("Eating");
                        if (waitToEat <= 0)
                        {
                            waitToEat = eatingTimer;
                            Destroy(target.gameObject);
                            target = null;
                            hunting = false;
                        }
                    }
                    else if (target.tag == "AnimalFood")
                    {
                        waitToEat -= Time.deltaTime;
                        //Play eat/drink animation
                        //Might have a bug here -Nick
                        anim.SetTrigger("Action");
                        anim.SetTrigger("Eating");
                        if (waitToEat <= 0)
                        {
                            waitToEat = eatingTimer;
                            currentHunger += increaseHungerBy;
                            target = null;
                            hunting = false;
                        }
                    }
                    else if (target.tag == "AnimalDrink")
                    {
                        waitToEat -= Time.deltaTime;
                        //Play eat/drink animation
                        //Might have a bug here -Nick
                        anim.SetTrigger("Action");
                        anim.SetTrigger("Eating");
                        if (waitToEat <= 0)
                        {
                            currentThirst += increaseThirstBy;
                            target = null;
                            hunting = false;
                        }
                    }
                    else if (target.tag == "Player")
                    {
                        if (waitToAttack <= 0)
                        {
                            //Play attack animation
                            anim.SetTrigger("Action");
                            anim.SetTrigger("Attack");
                            agent.SetDestination(transform.position); //Set to wait at position
                            if (target.GetComponent<Health>() != null)
                            {
                                target.GetComponent<Health>().curHealth -= damageDealt;
                            }
                            waitToAttack = attackTimer;
                            //after attack animation ends move again and play the movement animation
                            anim.SetBool("Movement", true);
                            anim.SetBool("Running", true);
                        }
                    }
                }
            }
        }
    }

    private void HandleTimers()
    {
        if (currentTimer <= 0)
        {
            currentTimer = 0;
            if (currentThirst > 0)
            {
                currentThirst -= decreaseThirstBy;
            }
            else if (currentThirst <= 0)
            {
                currentThirst = 0;
            }

            if (currentHunger > 0)
            {
                currentHunger -=  decreaseHungerBy;
            }
            else if (currentHunger <= 0)
            {
                currentHunger = 0;
            }
            currentTimer = secondsTillDecrease;
        }
        else if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime * secondsTillDecrease;
        }

        if (currentThirst > 100)
        {
            currentThirst = thirst;
        }
        else if (currentHunger > 100)
        {
            currentHunger = hunger;
        }

        if (waitToAttack > 0)
        {
            waitToAttack -= Time.deltaTime * secondsTillDecrease;
        }
        else if (waitToAttack <= 0)
        {
            waitToAttack = 0;
        }
    }

    private void FindTarget()
    {
        List<GameObject> bait = new List<GameObject>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Bait"))
        {
            if (!bait.Contains(obj))
            {
                bait.Add(obj);
            }
        }

        GameObject closestBait = null;
        if (bait.Count > 0)
        {
            closestBait = bait[0];
            foreach (GameObject obj in bait)
            {
                if (Vector3.Distance(transform.position, obj.transform.position) < Vector3.Distance(transform.position, closestBait.transform.position))
                {
                    closestBait = obj;
                }
            }
        }

        if (bait.Count > 0 && Vector3.Distance(closestBait.transform.position, transform.position) <= distanceFromBait && !closestBait.GetComponent<AnimalsTargetingBait>().isAnimalEating)
        {
            target = closestBait.transform;
            hunting = true;
        }
        else if (attackPlayer && Vector3.Distance(gameObject.transform.position, player.transform.position) <= distanceFromPlayerHunt)
        {
            target = player;
            hunting = false;
        }
        else if (currentHunger <= minHungerSearch)
        {
            if (!hunting)
            {
                List<GameObject> food = new List<GameObject>();
                food.AddRange(GameObject.FindGameObjectsWithTag("AnimalFood"));
                GameObject closestObject = food[0];
                foreach (GameObject obj in food)
                {
                    if (Vector3.Distance(closestObject.transform.position, gameObject.transform.position) > Vector3.Distance(obj.transform.position, gameObject.transform.position))
                    {
                        closestObject = obj;
                    }
                }
                target = closestObject.transform;
                hunting = true;
            }
        }
        else if (currentThirst <= minThirstSearch)
        {
            if (!hunting)
            {
                List<GameObject> drink = new List<GameObject>();
                drink.AddRange(GameObject.FindGameObjectsWithTag("AnimalDrink"));
                GameObject closestObject = drink[0];
                foreach (GameObject obj in drink)
                {
                    if (Vector3.Distance(closestObject.transform.position, gameObject.transform.position) > Vector3.Distance(obj.transform.position, gameObject.transform.position))
                    {
                        closestObject = obj;
                    }
                }
                target = closestObject.transform;
                hunting = true;
            }
        }
    }

    private void Wandering()
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * wanderRange;
        if (NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, NavMesh.AllAreas))
        {
            if (waitToWander <= 0)
            {
                agent.SetDestination(randomPoint);
                waitToWander = waitTimer;
                //Play walk animation
                anim.SetBool("Movement", true);
                anim.SetBool("Running", true);
            }
        }

        if (waitToWander > 0)
        {
            waitToWander -= Time.deltaTime;
            if (waitToWander < 0)
            {
                waitToWander = 0;
            }
        }

        if (agent.remainingDistance < distanceToStop)
        {
            agent.SetDestination(transform.position);
            //play idle animation
            anim.SetBool("Running", false);
            anim.SetBool("Movement", false);
            //print(transform.name + " has stopped!");
        }
    }
    #region Health Functions
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
        //Not in use -Nick
    }

    public void Die()
    {
        if (gameObject.tag == "Animal")
        {
            anim.SetTrigger("Action");
            anim.SetBool("Dead", true);
            alive = false;
            SelfDestruct();
        }
    }

    private void SelfDestruct()
    {
        Destroy(gameObject, 10f);
    }

    #endregion
}
