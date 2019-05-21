using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HackingGameController : MonoBehaviour, IPointerClickHandler
{
    public List<GameObject> connectors;
    public bool canRotate = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (canRotate)
        {
            gameObject.transform.Rotate(0, 0, 90, Space.Self);
        }
    }
}
