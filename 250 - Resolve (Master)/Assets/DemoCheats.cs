using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCheats : MonoBehaviour
{
    public List<Transform> teleLoc = new List<Transform>();
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Teleport(int i)
    {
        player.transform.position = teleLoc[i].position;
    }
}
