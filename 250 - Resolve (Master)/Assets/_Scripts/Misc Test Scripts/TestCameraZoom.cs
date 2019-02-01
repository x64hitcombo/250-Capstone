using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraZoom : MonoBehaviour {

    private Camera mainCamera;  //Camera for face cursor

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();

    }

    // Update is called once per frame
    void Update () {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundTerrain = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundTerrain.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}
