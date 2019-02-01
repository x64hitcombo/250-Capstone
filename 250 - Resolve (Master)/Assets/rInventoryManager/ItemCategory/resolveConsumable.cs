﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.General;
using Devdog.InventoryPro;

public class resolveConsumable : ConsumableInventoryItem
{

    public enum SurvStats
    {
        Thirst,
        Hunger
    };
    public SurvStats activeStat = SurvStats.Thirst;

    public float restore;

    public override int Use()
    {
        var stats = GameObject.FindGameObjectWithTag("Player").GetComponent<ManagePlayerStats>();

        if (activeStat == SurvStats.Thirst)
        {
            stats.currentThirst += restore;
            if (stats.currentThirst > stats.maxValue)
            {
                stats.currentThirst = stats.maxValue;
            }
        }
        else if (activeStat == SurvStats.Hunger)
        {
            stats.currentHunger += restore;
            if (stats.currentHunger > stats.maxValue)
            {
                stats.currentHunger = stats.maxValue;
            }
        }
        return base.Use();
    }
}