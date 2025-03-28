using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected GameObject item;

    public virtual void Pickup(GameObject itemToPickup)
    {
        item = itemToPickup;

        // TODO: Load an image for this item, and show it in the item slot
        //string imageName = string.Format("Sprites/Items/{0}.png", itemToPickup.name);
        //Sprite image = Resources.Load<Sprite>(imageName);

        //GetComponentInChildren<Image>().sprite = image;

        IsSelected(true); // Force the item in the hand of the player
    }

    public virtual void IsSelected(bool isSelected)
    {
        // TODO: Change the border to selected or not selected state
        string imageName = string.Format("Sprites/{0}", isSelected ? "red" : "black");
        Sprite image = Resources.Load<Sprite>(imageName);
        GetComponent<Image>().sprite = image;

        if (isSelected)
        {
            // TODO: Redraw the gameobject in the hand of the player
        } 
    }
}
