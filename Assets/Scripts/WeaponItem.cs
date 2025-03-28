using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    public override void Pickup(GameObject itemToPickup)
    {
        if (item != null)
        {
            // TODO: Drop item to the floor
        }

        base.Pickup(itemToPickup);
    }
}
