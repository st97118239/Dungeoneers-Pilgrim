using System;
using System.Collections.Generic;
using System.Resources;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int amountOfWeaponSlots = 2;
    public int amountOfSpecialSlots = 1;

    private readonly List<WeaponItem> weaponSlots = new();
    private readonly List<SpecialItem> specialSlots = new();

    public List<Item> Items = new(); // alle slots
    public Player player; // speler
    public GameObject droppedWeapons; // een laag waar alle gedropte wapens onder komen te hangen
    public Item CurrentSlot = null; // geselecteerde slot
    [SerializeField] private int currentSlotIndex; // geselecteerde slot nummer

    public GameObject weaponSlotPrefab;
    public GameObject specialSlotPrefab;

    public GameObject defaultWeapon;

    private float weaponSlotWidth; // hier komt de breedte van het weaponslot in
    private float specialSlotWidth; // hier komt de breedte van het special item slot in
    private const int margin = 25; // de ruimte tussen de inventory sloten

    void Start()
    {
        CreateInventory();
        ChangeSlot(0); // zet slot naar eerste slot standaard
        GiveDefaultWeapon();
    }

    private void GiveDefaultWeapon()
    {
        Debug.Log(defaultWeapon);
        PickupWeapon(defaultWeapon);
    }

    // generieke methode, bedankt pap ;)
    // TItem wordt bij de aanroep gevuld met of WeaponItem of SpecialItem
    // omdat beide classes erven van Item, kunnen we met deze methode dezelfde code toepassen op verschillende types
    // en dat betekent minder regels code
    private TItem CreateSlot<TItem>(GameObject prefab) where TItem : Item
    {
        GameObject newGameObject = Instantiate(prefab);
        TItem newItem = newGameObject.GetComponent<TItem>();
        newItem.player = player.gameObject;
        newItem.droppedWeapons = droppedWeapons;

        return newItem;
    }

    private void CreateInventory()
    {
        // hier worden de items dynamisch toegevoegd aan inventory dmv prefabs
        // van elk item wordt de positie berekend tov de inventory (dit gameObject)

        for (int weaponIdx = weaponSlots.Count; weaponIdx < amountOfWeaponSlots; weaponIdx++)
        {
            WeaponItem newWeaponItem = CreateSlot<WeaponItem>(weaponSlotPrefab);
            weaponSlots.Add(newWeaponItem);

            newWeaponItem.gameObject.transform.SetParent(gameObject.transform);

            weaponSlotWidth = ((RectTransform)newWeaponItem.gameObject.transform).rect.width;
        }

        for (int specialIdx = specialSlots.Count; specialIdx < amountOfSpecialSlots; specialIdx++)
        {
            SpecialItem newSpecialItem = CreateSlot<SpecialItem>(specialSlotPrefab);
            specialSlots.Add(newSpecialItem);

            newSpecialItem.gameObject.transform.SetParent(gameObject.transform);

            specialSlotWidth = ((RectTransform)newSpecialItem.gameObject.transform).rect.width;
        }

        // maak de inventory weer smal, zodat de posities van de slots kloppen
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, margin);

        int itemPosition = 0;

        // zet alle sloten op de juiste plek, beginnen bij de weapon slots
        for (int weaponIdx = 0; weaponIdx < weaponSlots.Count; weaponIdx++)
        {
            float x = (itemPosition + 1) * margin + itemPosition * weaponSlotWidth - 12.5f;
            weaponSlots[weaponIdx].gameObject.transform.SetLocalPositionAndRotation(new Vector3(x, 100), Quaternion.identity);
            itemPosition++;
        }

        // zet de special item slots op de juiste plek
        for (int specialIdx = 0; specialIdx < specialSlots.Count; specialIdx++)
        {
            float x = (itemPosition + 1) * margin + itemPosition * specialSlotWidth - 12.5f;
            specialSlots[specialIdx].gameObject.transform.SetLocalPositionAndRotation(new Vector3(x, 100), Quaternion.identity);
            itemPosition++;
        }

        // bereken hoe breedt de inventory moet worden, dit is een optelsom van alle sloten en hun breedte en de ruimte tussen de sloten
        float width = amountOfWeaponSlots * margin + amountOfWeaponSlots * weaponSlotWidth + margin + amountOfSpecialSlots * margin + amountOfSpecialSlots * specialSlotWidth;
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);

        // voeg alles toe aan de items lijst om sloten makkelijker te kunnen selecteren
        Items.Clear();
        Items.AddRange(weaponSlots);
        Items.AddRange(specialSlots);
    }

    public void AddWeaponSlot()
    {
        amountOfWeaponSlots++;
        CreateInventory();
    }

    public void AddSpecialSlot()
    {
        amountOfSpecialSlots++;
        CreateInventory();
    }

    void Update()
    {
        // alleen voor assessment om te laten zien dat de inventory dynamisch is
        if (Input.GetKeyDown(KeyCode.K))
            AddWeaponSlot();
        if (Input.GetKeyDown(KeyCode.L))
            AddSpecialSlot();

        if (!player.isPaused) // menu check
        {
            // controleer alle numerieke toetsen of ze zijn ingedrukt en wijzig het inventory slot
            for (KeyCode key = KeyCode.Alpha0; key <= KeyCode.Alpha9; key++)
            {
                if (Input.GetKeyDown(key))
                {
                    // de waarde van Alpha0 is lager dan Alpha1, dus we moeten die apart afhandelen
                    if (key == KeyCode.Alpha0)
                        ChangeSlot(10);
                    else
                        ChangeSlot(key - KeyCode.Alpha1); // als we Alpha1 van de waarde afhalen, krijg je 0 - 9
                }
            }

            var dir = Input.GetAxis("Mouse ScrollWheel"); // scrollwheel support toevoegen voor scrollen
            if (dir < 0f)
                NextSlot();
            else if (dir > 0f)
                PreviousSlot();
        }
    }

    private void PreviousSlot()
    {
        // Slot wrapping (van het eerste item naar het laatste item)
        if (currentSlotIndex == 0)
            ChangeSlot(Items.Count - 1);
        else
            ChangeSlot(currentSlotIndex - 1);
    }

    private void NextSlot()
    {
        // Slot wrapping (van het laatste item naar het eerste item)
        if (currentSlotIndex + 1 >= Items.Count)
            ChangeSlot(0);
        else
            ChangeSlot(currentSlotIndex + 1);
    }

    // functie voor slot veranderen
    private void ChangeSlot(int index)
    {
        // negeer een ongeldig slot (slot wat niet bestaat, of nog niet bestaat)
        if (index < 0 || index >= Items.Count)
            return;

        Items[currentSlotIndex].IsSelected(false); // zet slot uit (naar zwarte border)
        Items[index].IsSelected(true); // zet slot aan (naar rode border)
        currentSlotIndex = index;
        CurrentSlot = Items[currentSlotIndex];
    }

    // functie voor oppakken van wapen
    public void PickupWeapon(GameObject gameObject)
    {
        if (currentSlotIndex >= weaponSlots.Count) // checken of de player niet op een special item slot staat
            return;

        CurrentSlot.Pickup(gameObject); // functie aan roepen die item in inventory zet
        CurrentSlot.IsSelected(true); // functie aan roepen die item selecteerd (in de hand van de speler zet)
    }

    // functie voor oppakken lantaarn
    public bool PickupLantern(GameObject gameObject)
    {
        if (currentSlotIndex < weaponSlots.Count) // checken of de player niet op een weapon slot staat
            return false;

        CurrentSlot.Pickup(gameObject); // functie aan roepen die item in inventory zet
        CurrentSlot.IsSelected(true); // functie aan roepen die item selecteerd (in de hand van de speler zet)
        return true;
    }
}
