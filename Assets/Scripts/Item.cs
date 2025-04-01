using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected GameObject item;

    public virtual void Pickup(GameObject itemToPickup)
    {
        item = itemToPickup;

        // TODO: Load an image for this item, and show it in the item slot
        string imageName = string.Format("Sprites/Items/{0}", itemToPickup.name);
        Debug.Log(imageName);
        Sprite image = Resources.Load<Sprite>(imageName);

        var x = GetComponentsInChildren<Image>().Where(i => i.gameObject != gameObject).First(); // We don't want the image of the current gameObject, but the first child. This method name is CRAZY!
        x.sprite = image;
        x.color = Color.white;
    }

    public virtual void IsSelected(bool isSelected)
    {
        // TODO: Change the border to selected or not selected state
        string imageName = string.Format("Sprites/{0}", isSelected ? "red" : "black");
        Sprite image = Resources.Load<Sprite>(imageName);
        GetComponent<Image>().sprite = image;

        if (isSelected)
            ShowItemInHand();
        else
            RemoveItemFromHand();
    }

    public virtual void ShowItemInHand()
    {
        if (item != null)
        {
            item.SetActive(true);
        }
        
    }

    public virtual void RemoveItemFromHand()
    {
        if (item != null)
        {
            item.SetActive(false);
        }
    }
}
