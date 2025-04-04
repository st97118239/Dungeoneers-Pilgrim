using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected GameObject item; // item in de slot
    public GameObject player;
    public GameObject droppedWeapons; // Empty GameObject waar wapens die gedropped worden heen gaan

    protected abstract string BorderSelected { get; }
    protected abstract string BorderUnselected { get; }

    // functie voor object oppakken
    public virtual void Pickup(GameObject itemToPickup)
    {
        RemoveItemFromHand(); // deselecteer item

        if (item != null)
        {
            DropItem(item); // item die nu vast wordt gehouden droppen
        }

        item = itemToPickup;

        string imageName = string.Format("Sprites/Items/{0}", itemToPickup.name); // sprite voor item slot zoeken
        Sprite image = Resources.Load<Sprite>(imageName); // item slot sprite laden

        Image x = GetComponentsInChildren<Image>().Where(i => i.gameObject != gameObject).First(); // item slot met de sprite zoeken
        x.sprite = image; // sprite veranderen
        x.color = Color.white; // kleur naar wit veranderen zodat de sprite zichtbaar wordt

        // kijken of het item een wapen is
        if (item.TryGetComponent(out Weapon itemSlotWeapon))
        {
            itemSlotWeapon = item.GetComponent<Weapon>();

            item.transform.SetParent(Camera.main.transform); // zet wapen in camera zodat het wapen meebeweegt met de speler

            item.transform.SetLocalPositionAndRotation(DefaultWeaponLocations.GetPosition(itemSlotWeapon.wpnType), DefaultWeaponLocations.GetRotation(itemSlotWeapon.wpnType)); // zet wapen positie goed
            item.transform.localScale = Vector3.one; // reset scale van wapen anders wordt de scale 1.6

            item.GetComponent<BoxCollider>().enabled = false; // box collider uit zodat het niet tegen muren botst
        }
    }

    public virtual void IsSelected(bool isSelected)
    {
        string imageName = string.Format("Sprites/{0}", isSelected ? BorderSelected : BorderUnselected); // inventory slot sprite zoeken
        Sprite image = Resources.Load<Sprite>(imageName); // inventory slot sprite laden
        GetComponent<Image>().sprite = image; // inventory slot sprite in de slot zetten

        if (isSelected) // check of item in hand moet of niet
            ShowItemInHand();
        else
            RemoveItemFromHand();
    }

    // functie voor item selecteren
    public virtual void ShowItemInHand()
    {
        if (item != null)
        {
            item.SetActive(true);
            player.GetComponent<Player>().NewItem(item);
        }
    }

    // functie voor item deselecteren
    public virtual void RemoveItemFromHand()
    {
        if (item != null)
        {
            item.SetActive(false);
            player.GetComponent<Player>().RemoveItem();
        }
    }

    // functie voor item droppen
    public virtual void DropItem(GameObject itemToDrop)
    {
        itemToDrop.transform.localPosition = new Vector3(0f, 0f, 1f);
        itemToDrop.transform.SetParent(droppedWeapons.transform);
        itemToDrop.GetComponent<DroppableItem>().isDropped = true;
        itemToDrop.SetActive(true);
        itemToDrop.GetComponent<BoxCollider>().enabled = true;
        itemToDrop.AddComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
    }
}
