using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckConnections : MonoBehaviour
{
    public List<GameObject> connectorsTouching;
    private Collider2D objectCollider;
    public Collider2D objectHit;

    // Start is called before the first frame update
    void Start()
    {
        objectCollider = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        objectHit = collision.collider;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectHit = collision;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        objectHit = collision.collider;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.name == "NodeConnector")
        {
            //connectorsTouching.Remove(collision.gameObject);
        }
    }
}
