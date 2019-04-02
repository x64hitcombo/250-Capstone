using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggle : MonoBehaviour
{
    public bool toggle;
    public GameObject windows;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleUI();
        }
    }

    public void ToggleUI()
    {
        toggle = !toggle;
        windows.SetActive(toggle);
    }
}
