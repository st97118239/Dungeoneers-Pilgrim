using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected GameObject item;
    public GameObject player;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public virtual void Pickup(GameObject itemToPickup)
    {
        RemoveItemFromHand();

        item = itemToPickup;

        string imageName = string.Format("Sprites/Items/{0}", itemToPickup.name);
        Debug.Log(imageName);
        Sprite image = Resources.Load<Sprite>(imageName);

        Image x = GetComponentsInChildren<Image>().Where(i => i.gameObject != gameObject).First();
        x.sprite = image;
        x.color = Color.white;

        Weapon itemSlotWeaponStats = GetComponent<Weapon>();

        if (itemSlotWeaponStats != null)
        {
            itemSlotWeaponStats.wpnType = item.GetComponent<Weapon>().wpnType;
            itemSlotWeaponStats.dmg = item.GetComponent<Weapon>().dmg;
            itemSlotWeaponStats.atkspd = item.GetComponent<Weapon>().atkspd;
            itemSlotWeaponStats.atkRange = item.GetComponent<Weapon>().atkRange;

            item.transform.SetParent(mainCamera.transform);

            item.transform.localPosition = DefaultWeaponLocations.GetPosition(itemSlotWeaponStats.wpnType);
            item.transform.localRotation = DefaultWeaponLocations.GetRotation(itemSlotWeaponStats.wpnType);
            item.transform.localScale = Vector3.one;

            item.GetComponent<BoxCollider>().enabled = false;
        }        
    }

    public virtual void IsSelected(bool isSelected)
    {
        string imageName = string.Format("Sprites/{0}", isSelected ? "InventoryRed" : "InventoryBlack");
        Sprite image = Resources.Load<Sprite>(imageName);
        GetComponent<Image>().sprite = image;

        //Debug.Log(item.transform.rotation.w);

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
            player.GetComponent<Player>().NewItem(item);
        }
    }

    public virtual void RemoveItemFromHand()
    {
        if (item != null)
        {
            item.SetActive(false);
            player.GetComponent<Player>().RemoveItem();
        }
    }
}
