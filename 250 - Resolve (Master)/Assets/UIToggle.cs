using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggle : MonoBehaviour
{
    public bool toggle = false;
    public List<GameObject> windows = new List<GameObject>();
    public GameObject[] singleWindows;

    private void Start()
    {
        foreach (GameObject window in windows)
        {
            window.SetActive(toggle);
        }

        foreach (GameObject window in singleWindows)
        {
            window.SetActive(toggle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ToggleUI();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            toggle = !toggle;
        }

        if (!VictoryCheck())
        {
            toggle = false;
            ToggleUI();
        }
    }

    public void ToggleUI()
    {
        //toggle = !toggle;
        foreach (GameObject window in windows)
        {
            window.SetActive(toggle);
        }
    }

    public bool VictoryCheck()
    {
        for (int i = 0; i < singleWindows.Length; i++)
        {
            if (singleWindows[i].activeSelf)
            {
                Debug.Log("yeet");
                return false;
            }
        }

        return true;
    }
}
