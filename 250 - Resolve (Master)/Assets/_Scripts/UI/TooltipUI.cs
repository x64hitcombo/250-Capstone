using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour {

    private ManagePlayerStats player;

    public GameObject sprite;
    //public Sprite tipIcon;
    public Text tipValue;
    public Text tipTitle;
    public Text tipDesc;

    public string activeValue;

    public enum Stats
    {
        Null,
        Thirst,
        Hunger,
        Exposure,
        Fatigue
    };
    public Stats targetStat;

    public static TooltipUI _instance;
    public static TooltipUI Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TooltipUI>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("Tooltip");
                    _instance = container.AddComponent<TooltipUI>();
                }
            }

            return _instance;
        }
    }

    private void Start()
    {
        HideToolTip();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ManagePlayerStats>();
        //tipIcon = sprite.GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        HandleTransform();

        if (targetStat == Stats.Thirst)
        {
            activeValue = player.currentThirst.ToString();
        }
        else if (targetStat == Stats.Hunger)
        {
            activeValue = player.currentHunger.ToString();
        }
        else if (targetStat == Stats.Exposure)
        {
            activeValue = player.currentExposure.ToString();
        }
        else if (targetStat == Stats.Fatigue)
        {
            activeValue = player.currentFatigue.ToString();
        }
        else
        {
            activeValue = " ";
        }

        tipValue.text = activeValue;
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
