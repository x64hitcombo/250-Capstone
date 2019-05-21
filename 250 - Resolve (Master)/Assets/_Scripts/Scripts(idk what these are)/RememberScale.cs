using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RememberScale : MonoBehaviour
{
    public Vector3 scaleToRemeber;

    // Start is called before the first frame update
    void Start()
    {
        scaleToRemeber = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale = scaleToRemeber;
    }
}
