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

    public float maxDistanceFromHome = 50f;

    public float wanderRange = 10f;

    public float eatingTimer = 5f;
    public float waitTimer = 5f;
    public float attackTimer = 5f;

    public bool attackPlayer = false;
    public bool hunting = false;

    [SerializeField]
    private float currentTimer, currentHunger, currentThirst;

    private NavMeshHit navHit;
    private Vector3 wanderTarget;

    private float waitToEat;
    private float waitToWander;
    private float waitToAttack;

    private Animation anim;
    private Vector3 lastPosition;

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
        if (GetComponent<Animation>() != null)
        {
            anim = GetComponent<Animation>();
        }
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HungerThirstTimer();
        FindTarget();
        if (target == null)
        {
            Wandering();
        }
        else if (target != null && Vector3.Distance(homeSpawner.position, target.position) <= maxDistanceFromHome)
        {
            agent.SetDestination(target.position);
            if (anim != null)
            {
                anim.Play();
            }
        }
        else if (!hunting && Vector3.Distance(gameObject.transform.position, player.transform.position) > distanceFromPlayerHunt)
        {
            Wandering();
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (target != null)
            {
                if (target.tag == "AnimalFood")
                {
                    waitToEat -= Time.deltaTime;
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
                    if (waitToEat <= 0)
                    {
                        currentThirst += increaseThirstBy;
                        target = null;
                        hunting = false;
                    }
                }
                else if (target.tag == "Player")
                {
                    //attack player
                }
            }
        }

        Vector3 position = gameObject.transform.position;
        if (position == lastPosition)
        {
            if (anim != null)
            {
                anim.Stop();
            }
        }
        lastPosition = position;

    }

    private void HungerThirstTimer()
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
                currentHunger -= decreaseHungerBy;
            }
            else if (currentHunger <= 0)
            {
                currentHunger = 0;
            }
            currentTimer = secondsTillDecrease;
        }
        else if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }

        if (currentThirst > 100)
        {
            currentThirst = thirst;
        }
        else if (currentHunger > 100)
        {
            currentHunger = hunger;
        }
    }

    private void FindTarget()
    {
        if (attackPlayer && Vector3.Distance(gameObject.transform.position, player.transform.position) <= distanceFromPlayerHunt)
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
                if (anim != null)
                {
                    anim.Play();
                }
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
    }
}
