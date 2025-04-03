using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected GameObject item;
    public GameObject player;
    public GameObject droppedWeapons;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public virtual void Pickup(GameObject itemToPickup)
    {
        RemoveItemFromHand();

        if (item != null)
        {
            DropItem(item);
        }

        item = itemToPickup;

        string imageName = string.Format("Sprites/Items/{0}", itemToPickup.name);
        Debug.Log(imageName);
        Sprite image = Resources.Load<Sprite>(imageName);

        Image x = GetComponentsInChildren<Image>().Where(i => i.gameObject != gameObject).First();
        x.sprite = image;
        x.color = Color.white;

        if (TryGetComponent<Weapon>(out var itemSlotWeaponStats))
        {
            itemSlotWeaponStats.wpnType = item.GetComponent<Weapon>().wpnType;
            itemSlotWeaponStats.dmg = item.GetComponent<Weapon>().dmg;
            itemSlotWeaponStats.atkspd = item.GetComponent<Weapon>().atkspd;
            itemSlotWeaponStats.atkRange = item.GetComponent<Weapon>().atkRange;

            item.transform.SetParent(mainCamera.transform);

            item.transform.SetLocalPositionAndRotation(DefaultWeaponLocations.GetPosition(itemSlotWeaponStats.wpnType), DefaultWeaponLocations.GetRotation(itemSlotWeaponStats.wpnType));
            item.transform.localScale = Vector3.one;

            item.GetComponent<BoxCollider>().enabled = false;
        }        
    }

    public virtual void IsSelected(bool isSelected)
    {
        string imageName = string.Format("Sprites/{0}", isSelected ? "InventoryRed" : "InventoryBlack");
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
