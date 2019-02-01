using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour {

    private ManagePlayerStats player;

    public Image tipIcon;
    public Text tipValue;
    public Text tipTitle;
    public Text tipDesc;

    public float activeValue = 0;

    public enum Stats
    {
        Null,
        Thirst,
        Hunger,
        Exposure,
        Fatigue
    };
    public Stats targetStat;

    GameObject target;

    private void Start()
    {
        HideToolTip();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ManagePlayerStats>();

    }

    private void Update()
    {
        HandleTransform();

        if (targetStat == Stats.Thirst)
        {
            activeValue = player.currentThirst;
        }
        else if (targetStat == Stats.Hunger)
        {
            activeValue = player.currentHunger;
        }
        else if (targetStat == Stats.Exposure)
        {
            activeValue = player.exposure;
        }
        else if (targetStat == Stats.Fatigue)
        {
            activeValue = player.currentFatigue;
        }
        else
        {
            activeValue = 0;
        }

        tipValue.text = activeValue.ToString();
    }

    public void HandleTransform()
    {
        this.transform.position = Input.mousePosition;
    }

    public void ShowToolTip()
    {
        this.gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        this.gameObject.SetActive(false);
    }
}
