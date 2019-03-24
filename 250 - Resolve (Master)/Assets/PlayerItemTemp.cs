using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemTemp : MonoBehaviour
{
    public float tempRadius = 5f;
    public bool hot = false;
    public bool cold = false;
    public bool neither = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeItemTemp();
        InTempZone();
    }

    public void InTempZone()
    {
        List<Collider> overlaps = new List<Collider>();
        overlaps.AddRange(Physics.OverlapSphere(transform.position, tempRadius));

        if (overlaps != null)
        {
            foreach(Collider collider in overlaps)
            {
                if (collider.gameObject.tag == "ChangeTempObject")
                {
                    if (hot)
                    {
                        collider.gameObject.GetComponent<HandleObjectTemperature>().hot = true;
                        collider.gameObject.GetComponent<HandleObjectTemperature>().cold = false;
                        collider.gameObject.GetComponent<HandleObjectTemperature>().neither = false;
                        collider.gameObject.GetComponent<HandleObjectTemperature>().changeColor = true;
                    }
                    else if (cold)
                    {
                        collider.gameObject.GetComponent<HandleObjectTemperature>().hot = false;
                        collider.gameObject.GetComponent<HandleObjectTemperature>().cold = true;
                        collider.gameObject.GetComponent<HandleObjectTemperature>().neither = false;
                        collider.gameObject.GetComponent<HandleObjectTemperature>().changeColor = true;
                    }
                }
            }
        }
    }

    public void ChangeItemTemp()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (hot)
            {
                hot = false;
                cold = true;
            }
            else if (cold)
            {
                cold = false;
                neither = true;
            }
            else if (neither)
            {
                neither = false;
                hot = true;
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, tempRadius);
    }
}
