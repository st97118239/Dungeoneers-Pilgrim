using UnityEngine;

public class WeaponItem : Item
{
    protected override string BorderSelected => "InventoryRed";

    protected override string BorderUnselected => "InventoryBlack";

    public override void Pickup(GameObject itemToPickup)
    {
        if (item != null)
        {
            // TODO: Drop item to the floor
        }

        base.Pickup(itemToPickup);
    }
}
