using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.General;
using Devdog.InventoryPro;

public class TestType : ConsumableInventoryItem {

    public float hunRestore;

    public override int Use()
    {
        var stats = GameObject.FindGameObjectWithTag("Player").GetComponent<ManagePlayerStats>();
        stats.currentHunger += hunRestore;
        if (stats.currentHunger > stats.maxValue)
        {
            stats.currentHunger = stats.maxValue;
        }
        return base.Use();
    }
}
