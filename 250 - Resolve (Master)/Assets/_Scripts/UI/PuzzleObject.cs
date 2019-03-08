using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleObject : MonoBehaviour
{
    public bool confirm = false;

    public Vector3 ogTrans;

    // Start is called before the first frame update
    void Start()
    {
        ogTrans = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
