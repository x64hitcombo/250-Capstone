using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCompass : MonoBehaviour
{
    public Transform[] targets;
    public Transform cam;
    private int activeTarget;

    public RectTransform compassArrow;
    public Quaternion arrowRotation;
    public Vector3 northDir;


    // Start is called before the first frame update
    void Update()
    {
        North();
        DetermineQuestDirection();
    }

    public void North()
    {
        northDir.z = cam.eulerAngles.y;
    }

    public void DetermineQuestDirection()
    {
        if (targets != null)
        {
            Vector3 direction = transform.position - targets[activeTarget].position;

            arrowRotation = Quaternion.LookRotation(direction);

            arrowRotation.z = -arrowRotation.y;
            arrowRotation.x = 0;
            arrowRotation.y = 0;

            compassArrow.localRotation = arrowRotation * Quaternion.Euler(northDir);
        }
    }

    public void SetTarget(int quest)
    {
        activeTarget = quest;
    }
}
