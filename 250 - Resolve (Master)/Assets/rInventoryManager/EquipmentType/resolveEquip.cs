using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.General;
using Devdog.InventoryPro;

public class resolveEquip : EquippableInventoryItem {

    public float expRating;

    public override int Use()
    {
        var stats = GameObject.FindGameObjectWithTag("Player").GetComponent<ManagePlayerStats>();
        stats.exposureRating += expRating;

        return base.Use();
    }
}
