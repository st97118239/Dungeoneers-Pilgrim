using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item[] Items = new Item[3]; // alle slots
    public Player player; // speler
    public Item CurrentSlot = null; // geselecteerde slot
    [SerializeField] private int currentSlotIndex; // geselecteerde slot nummer

    private const int LanternSlot = 2; // lantern slot index nummer

    void Start()
    {
        ChangeSlot(0); // zet slot naar eerste slot standaard
    }

    void Update()
    {
        if (!player.isPaused) // menu check
        {
            // item switchen via toetsen
            if (Input.GetKeyDown(KeyCode.Alpha1))
                ChangeSlot(0);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                ChangeSlot(1);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                ChangeSlot(2);

            var dir = Input.GetAxis("Mouse ScrollWheel"); // scrollwheel support toevoegen voor scrollen
            if (dir < 0f)
                ChangeSlot(currentSlotIndex + 1);
            else if (dir > 0f)
                ChangeSlot(currentSlotIndex - 1);
        }
    }

    // functie voor slot veranderen
    private void ChangeSlot(int index)
    {
        // Slot wrapping (van item 3 terug naar 1 gaan en andersom)
        if (index < 0) index = Items.Length - 1;
        else if (index >= Items.Length) index = 0;

        Items[currentSlotIndex].IsSelected(false); // zet slot uit (naar zwarte border)
        Items[index].IsSelected(true); // zet slot aan (naar rode border)
        currentSlotIndex = index;
        CurrentSlot = Items[currentSlotIndex];
    }

    // functie voor oppakken van wapen
    public void PickupWeapon(GameObject gameObject)
    {
        if (currentSlotIndex == LanternSlot) // checken of de player niet op de lantaarn slot staat
            return;

        CurrentSlot.Pickup(gameObject); // functie aan roepen die item in inventory zet
        CurrentSlot.IsSelected(true); // functie aan roepen die item selecteerd (in de hand van de speler zet)
    }

    // functie voor oppakken lantaarn
    public void PickupLantern(GameObject gameObject)
    {
        Items[LanternSlot].Pickup(gameObject); // zet lantaarn in het lantaarn gereserveerde slot

        if (currentSlotIndex == LanternSlot) // checken of de lantaarn slot geselecteerd is
            CurrentSlot.IsSelected(true);
    }
}
