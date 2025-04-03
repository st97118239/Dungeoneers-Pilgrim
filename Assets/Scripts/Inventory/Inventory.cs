using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item[] Items = new Item[3];
    public Player player;
    public Item CurrentSlot = null;
    [SerializeField] private int currentSlotIndex;

    private const int LanternSlot = 2;

    void Start()
    {
        ChangeSlot(0);
    }

    void Update()
    {
        if (!player.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                ChangeSlot(0);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                ChangeSlot(1);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                ChangeSlot(2);

            var dir = Input.GetAxis("Mouse ScrollWheel");
            if (dir < 0f)
                ChangeSlot(currentSlotIndex + 1);
            else if (dir > 0f)
                ChangeSlot(currentSlotIndex - 1);
        }
    }

    private void ChangeSlot(int index)
    {
        // Make sure that the current slot is circular, e.g. going down from slot 0 goes to slot 2, and going up from slot 2 goes to slot 0
        if (index < 0) index = Items.Length - 1;
        else if (index >= Items.Length) index = 0;

        Items[currentSlotIndex].IsSelected(false);
        Items[index].IsSelected(true);
        currentSlotIndex = index;
        CurrentSlot = Items[currentSlotIndex];
    }

    public void PickupWeapon(GameObject gameObject)
    {
        if (currentSlotIndex == LanternSlot)
            return;

        CurrentSlot.Pickup(gameObject);
        CurrentSlot.IsSelected(true);
    }

    public void PickupLantern(GameObject gameObject)
    {
        Items[LanternSlot].Pickup(gameObject);

        if (currentSlotIndex == LanternSlot)
            CurrentSlot.IsSelected(true);
    }
}
