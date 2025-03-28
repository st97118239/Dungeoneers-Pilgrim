using UnityEngine;

public class SpecialItem : Item
{
    public override void Pickup(GameObject itemToPickup)
    {
        if (item != null) return;

        if (!itemToPickup.CompareTag("lantern"))
            return;

        base.Pickup(itemToPickup);
    }
}
