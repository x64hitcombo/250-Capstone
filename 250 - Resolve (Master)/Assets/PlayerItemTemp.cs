using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemTemp : MonoBehaviour
{
    public float tempRadius = 5f;

    public enum tempSet
    {
        Hot,
        Cold
    }
    public tempSet temp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InTempZone();
        ChangeItemTemp();
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
                    if (temp == tempSet.Hot)
                    {
                        collider.gameObject.GetComponent<HandleObjectTemperature>().hot = true;
                        collider.gameObject.GetComponent<HandleObjectTemperature>().cold = false;
                        collider.gameObject.GetComponent<HandleObjectTemperature>().neither = false;
                        collider.gameObject.GetComponent<HandleObjectTemperature>().changeColor = true;
                    }
                    else if (temp == tempSet.Cold)
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (temp == tempSet.Hot)
            {
                temp = tempSet.Cold;
            }
            else if (temp == tempSet.Cold)
            {
                temp = tempSet.Hot;
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, tempRadius);
    }
}
