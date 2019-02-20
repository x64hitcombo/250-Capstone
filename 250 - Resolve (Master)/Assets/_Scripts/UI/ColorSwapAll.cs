using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSwapAll : MonoBehaviour
{

    public GameObject timeController;
    public float curTime;

    public float dayHour = 5;
    public Color dayColor = new Color(1, 1, 1, .8f);
    public float nightHour = 16;
    public Color nightColor = new Color(0, 0, 0, .8f);


    // Use this for initialization
    void Start()
    {

        timeController = GameObject.FindGameObjectWithTag("TimeController");
    }

    // Update is called once per frame
    void Update()
    {

        curTime = timeController.GetComponent<MainTimeScript>().Get_fCurrentHour;


            if (curTime >= dayHour && curTime <= nightHour)
            {
                ChangeColor(dayColor);
                ChangeTextColor(nightColor);
            }
            else if (curTime >= nightHour || curTime <= dayHour)
            {
                ChangeColor(nightColor);
                ChangeTextColor(dayColor);
            }
        }

    public void ChangeColor(Color col)
    {
        foreach (Image i in FindObjectsOfType<Image>())
        {
            if (i.tag != "UI_noSwap")
            {
                i.color = col;
            }
        }
    }

    public void ChangeTextColor(Color col)
    {
        foreach (Text t in FindObjectsOfType<Text>())
        {
            if (t.tag != "UI_noSwap")
            {
                t.color = col;
            }
        }
    }
}
