using UnityEngine;

public class SpecialItem : Item
{
    protected override string BorderSelected => "InventoryRed";

    protected override string BorderUnselected => "InventoryBlue";

    public override void Pickup(GameObject itemToPickup)
    {
        if (!itemToPickup.CompareTag("Lantern"))
            return;

        base.Pickup(itemToPickup);
    }

    public override void ShowItemInHand()
    {
        if (item != null)
        {
            item.transform.localPosition = item.GetComponent<LanternInHand>().coordsInHand;
            item.GetComponent<LanternInHand>().Light.GetComponent<Light>().intensity = item.GetComponent<LanternInHand>().intensityHand;
            item.GetComponent<LanternInHand>().Light.GetComponent<Light>().range = item.GetComponent<LanternInHand>().rangeHand;
            player.GetComponent<Player>().NewItem(item);
        }
    }

    public override void RemoveItemFromHand()
    {
        if (item != null)
        {
            item.transform.localPosition = item.GetComponent<LanternInHand>().coordsOnSide;
            item.GetComponent<LanternInHand>().Light.GetComponent<Light>().intensity = item.GetComponent<LanternInHand>().intensitySide;
            item.GetComponent<LanternInHand>().Light.GetComponent<Light>().range = item.GetComponent<LanternInHand>().rangeSide;
            player.GetComponent<Player>().RemoveItem();
        }
    }
}
