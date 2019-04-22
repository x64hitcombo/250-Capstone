using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingGameSpawn : MonoBehaviour
{
    public float spawnTimer = 10f;
    public GameObject objectToSpawn;
    public GameObject parentObject;

    public float actualTimer;

    // Start is called before the first frame update
    void Start()
    {
        actualTimer = spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        TimerToSpawn();
    }

    private void TimerToSpawn()
    {   
        if (actualTimer <= 0)
        {
            actualTimer = 0;
            GameObject dummyObjectToSpawn = Instantiate(objectToSpawn);
            dummyObjectToSpawn.transform.parent = parentObject.transform;
            dummyObjectToSpawn.transform.position = parentObject.transform.position;
            dummyObjectToSpawn.transform.localScale = new Vector3(1, 1, 1);
            actualTimer = spawnTimer;
        }
        else if (actualTimer > 0)
        {
            actualTimer -= Time.deltaTime;
        }
    }
}
