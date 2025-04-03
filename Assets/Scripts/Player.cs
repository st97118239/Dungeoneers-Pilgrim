using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public List<GameObject> hearts = new List<GameObject>();
    public List<GameObject> heartsActive = new List<GameObject>();
    public GameObject selectedItem;
    public GameObject deathScreen;
    public TMP_Text coinText;
    public Menu menu;
    public Image attackProgressBar;
    public string itemTag;
    public bool isDead = false;
    public int totalCoins = 0;

    private float atkCooldown;

    void Start()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < hearts.Count)
            {
                heartsActive.Add(hearts[i]);
            }
        }

        attackProgressBar.fillAmount = 0f;
        attackProgressBar.gameObject.SetActive(false);
    }
    void Update()
    {
        atkCooldown -= Time.deltaTime;

        if (atkCooldown < 0)
        {
            atkCooldown = 0f;

            if (Input.GetMouseButtonDown(0))
            {
                if (selectedItem != null && selectedItem.CompareTag("Weapon"))
                {
                    Attack();
                }
            }
            attackProgressBar.gameObject.SetActive(false);
        }
        else
        {
            attackProgressBar.gameObject.SetActive(true);
            if (selectedItem != null && selectedItem.CompareTag("Weapon"))
                attackProgressBar.fillAmount = 1 - atkCooldown / selectedItem.GetComponent<Weapon>().atkspd;
        }
    }

    private void Attack()
    {
        Weapon weaponTags = selectedItem.GetComponent<Weapon>();
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, weaponTags.atkRange, 1 << 10))
        {
            Enemy enemyHit = hit.collider.gameObject.GetComponent<Enemy>();
            enemyHit.health -= weaponTags.dmg;
        }

        atkCooldown = weaponTags.atkspd;
    }

    public void NewItem(GameObject item)
    {
        selectedItem = item;
        itemTag = item.tag;
        if (selectedItem != null && item.CompareTag("Weapon"))
        {
            atkCooldown = selectedItem.GetComponent<Weapon>().atkspd;
        }
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

            string imageName = "Sprites/HeartBlack";
            Sprite image = Resources.Load<Sprite>(imageName);

            lastHeart.GetComponent<Image>().sprite = image;

            heartsActive.RemoveAt(heartsActive.Count - 1);

            print("Lost a heart! Remaining hearts: " + heartsActive.Count);

            if (heartsActive.Count == 0)
            {
                print("Player is dead!");
                isDead = true;
                deathScreen.SetActive(true);
                menu.lockCursor = false;
                menu.ResetCursor();
            }
        }
    }

    public void AddHeart()
    {
        if (heartsActive.Count < hearts.Count)
        {
            GameObject newHeart = hearts[heartsActive.Count];

            heartsActive.Add(newHeart);

            string imageName = "Sprites/HeartRed";
            Sprite image = Resources.Load<Sprite>(imageName);
            newHeart.GetComponent<Image>().sprite = image;

            print("Gained a heart! Active hearts: " + heartsActive.Count);
        }
    }

    public void AddCoins(int coinAmountToAdd)
    {
        totalCoins += coinAmountToAdd;
        coinText.text = "" + totalCoins;
        Debug.Log("Coins collected: " + coinAmountToAdd);
        Debug.Log("Total coins: " + totalCoins);
    }
}
