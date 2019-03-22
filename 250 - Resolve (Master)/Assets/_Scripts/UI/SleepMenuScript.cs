using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepMenuScript : MonoBehaviour
{

    public MainTimeScript TimeMain;

    public GameObject SleepPanel;

    public Slider Slider;

    public Text CurrentHour;
    public Text CurrentMinute;
    public Text SelectedHour;
    
    private bool SleepTime;
    public PlayerController player;
    public bool toggle = true;

    void Start()
    {
        TimeMain = FindObjectOfType<MainTimeScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        SleepTime = false;
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        SelectHour();
        CurrentTime();

        if (SleepTime == true)
        {
            FastForward();
        }

        Debug.Log("Updating:");
    }

    //Show what time it currently is in the game world
    void CurrentTime()
    {
        CurrentHour.text = TimeMain.Get_fCurrentHour.ToString("F0");
        CurrentMinute.text = TimeMain.Get_fCurrentMinute.ToString("F0");
    }

    //Updates text box to display what hour is currently selected on the slider
    void SelectHour()
    {
        SelectedHour.text = Slider.value.ToString("F0");
    }

    //Increases game speed to fast forward to the designated hour
    public void FastForward()
    {

        Debug.Log("Clicked");
        SleepTime = true;

        if (TimeMain._fCurrentHour != Slider.value)
        {
            TimeMain.GetSet_fTimeMultiplier = 100f;
            player.movement = false;
        }
        if (TimeMain._fCurrentHour >= Slider.value && TimeMain._fCurrentHour <= Slider.value + 0.99)
        {
            SleepTime = false;
            player.movement = true;
            toggle = false;
        }
    }

    public void ToggleUI()
    {
        toggle = !toggle;
        this.gameObject.SetActive(toggle);
    }

}
