using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    public float spawnTimer = 10;
    public float maxSpawn = 10;

    public bool canSpawnsAttack = false;

    private float currentTimer;
    private float currentSpawn;

    public GameObject objectToSpawn;

    public List<GameObject> spawnedAI = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        currentTimer = spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        TimerToSpawn();
    }

    private void TimerToSpawn()
    {
        if (currentTimer <= 0 && spawnedAI.Count < 10)
        {
            currentTimer = 0;
            GameObject dummyObjectToSpawn = Instantiate(objectToSpawn);
            if (canSpawnsAttack)
            {
                dummyObjectToSpawn.GetComponent<AIController>().attackPlayer = true;
            }
            dummyObjectToSpawn.GetComponent<AIController>().homeSpawner = gameObject.transform;
            dummyObjectToSpawn.name = objectToSpawn.name;
            spawnedAI.Add(dummyObjectToSpawn);
            currentTimer = spawnTimer;
        }
        else if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }

        for (int i = spawnedAI.Count - 1; i > -1; i--)
        {
            if (spawnedAI[i] == null)
            {
                spawnedAI.RemoveAt(i);
            }
        }
    }
}
