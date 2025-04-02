using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public List<GameObject> hearts = new List<GameObject>();
    public List<GameObject> heartsActive = new List<GameObject>();
    public GameObject selectedItem;
    public GameObject deathScreen;
    public Menu menu;
    public string itemTag;
    public bool isDead = false;

    void Start()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            // Add heart to active hearts list based on how many hearts the player should start with
            if (i < hearts.Count)
            {
                heartsActive.Add(hearts[i]);
            }
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (itemTag == "Weapon")
            {
                print("attack");
            }
        }
    }

    public void NewItem(GameObject item)
    {
        selectedItem = item;
        itemTag = item.tag;
    }

    public void RemoveItem()
    {
        selectedItem = null;
        itemTag = null;
    }

    public void RemoveHeart()
    {
        if (heartsActive.Count > 0)
        {
            GameObject lastHeart = heartsActive[^1];

            // Set the sprite to "black.png" for the heart being removed
            string imageName = "Sprites/HeartBlack";  // Always load the "black.png" sprite
            Sprite image = Resources.Load<Sprite>(imageName);

            // Get the Image component of the heart and set the sprite to black
            lastHeart.GetComponent<Image>().sprite = image;

            heartsActive.RemoveAt(heartsActive.Count - 1);

            print("Lost a heart! Remaining hearts: " + heartsActive.Count);

            if (heartsActive.Count == 0)
            {
                print("Player is dead!");
                isDead = true;
                deathScreen.SetActive(true);
                menu.lockCursor = false;
            }
        }
    }

    public void AddHeart()
    {
        if (heartsActive.Count < hearts.Count)  // Only add a heart if there are inactive hearts left
        {
            // Find the first inactive heart in `hearts` (one that is NOT in `heartsActive`)
            GameObject newHeart = hearts[heartsActive.Count];

            // Add it back to the `heartsActive` list
            heartsActive.Add(newHeart);

            // Set the sprite to "red.png" to make it visible again
            string imageName = "Sprites/HeartRed";
            Sprite image = Resources.Load<Sprite>(imageName);
            newHeart.GetComponent<Image>().sprite = image;

            print("Gained a heart! Active hearts: " + heartsActive.Count);
        }
    }
}
