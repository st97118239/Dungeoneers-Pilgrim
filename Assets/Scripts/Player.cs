using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public List<GameObject> hearts = new(); // list van totale aantal harten
    public List<GameObject> heartsActive = new(); // list van harten die de speler over heeft
    public GameObject selectedItem; // item die de speler geselecteerd heeft
    public GameObject deathScreen; // de panel die te voorschijn komt wanneer je dood gaat
    public GameObject endScreen; // de panel die te voorschijn komt wanneer je klaar bent
    public TMP_Text coinText; // het text object van munten
    public Menu menu; // het menu panel die te voorschijn komt wanneer je op esc drukt
    public Image attackProgressBar; // de image die te voorschijn komt wanneer je slaat
    public static bool isPaused; // bool voor het pauzeren
    public static bool isDead; // bool voor speler dood of niet
    public int totalCoins = 0; // totale aantal munten
    public float atkCooldown; // timer totdat de speler weer kan aanvallen
    public Checkpoints checkpoint; // checkpoint van de speler om makkelijk te testen

    private bool grabItem; // bool om cooldown the halveren als de speler naar het wapen switcht

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void AfterSceneLoaded()
    {
        isPaused = false;
        isDead = false;
    }

    private void Awake()
    {
        AfterSceneLoaded();
    }

    void Start()
    {
        for (int i = 0; i < hearts.Count; i++) // elk hart van hearts in heartsActive zetten
        {
            if (i < hearts.Count)
            {
                heartsActive.Add(hearts[i]);
            }
        }

        transform.SetLocalPositionAndRotation(CheckpointLocations.GetPosition(checkpoint), CheckpointLocations.GetRotation(checkpoint)); // zet speler naar checkpoint

        // zet de coolbar image op 0 en uit
        attackProgressBar.fillAmount = 0f;
        attackProgressBar.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isDead && !isPaused)
        {
            // timer voor de cooldown van aanvallen
            atkCooldown -= Time.deltaTime;

            if (atkCooldown < 0)
            {
                atkCooldown = 0f;

                if (Input.GetMouseButtonDown(0))
                {
                    if (selectedItem != null && selectedItem.CompareTag("Weapon")) // check of de speler een wapen vast heeft
                    {
                        Attack();
                    }
                }
                attackProgressBar.gameObject.SetActive(false); // zet de coolbar image uit wanneer vol

                grabItem = false; // zet bool uit om te voorkomen dat de cooldown gehalveerd wordt
            }
            else
            {
                attackProgressBar.gameObject.SetActive(true); // zet de cooldown image weer aan
                if (selectedItem != null && selectedItem.CompareTag("Weapon")) // check of de speler een wapen vast heeft
                {
                    int factor = grabItem ? 2 : 1; // zet de factor naar 2 als grabItem aan staat, anders 1
                    attackProgressBar.fillAmount = 1 - atkCooldown / (selectedItem.GetComponent<Weapon>().atkspd / factor); // zet de cooldown image op de correcte hoeveelheid
                }
            }
        }
    }

    // functie voor aanvallen
    private void Attack()
    {
        Weapon weaponTags = selectedItem.GetComponent<Weapon>(); // zet weaponTags om makkelijk be de component te komen
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, weaponTags.atkRange, 1 << 10)) // stuur raycast voor aanvallen
        {
            Enemy enemyHit = hit.collider.gameObject.GetComponent<Enemy>(); // zet de enemy component van de enemy die de speler slaat in enemyHit om er makkelijk bij te komen
            enemyHit.health -= weaponTags.dmg; // haal dmg van de enemy's health af
        }

        atkCooldown = weaponTags.atkspd; // zet de attack cooldown naar atkspd
    }

    // functie voor wanneer je van item switcht
    public void NewItem(GameObject item)
    {
        selectedItem = item; // zet het geselecteerde wapen in selectedItem
        if (selectedItem != null && item.CompareTag("Weapon")) // check of het item een wapen is
        {
            grabItem = true; // zet grabItem aan om de cooldown te halveren
            atkCooldown = selectedItem.GetComponent<Weapon>().atkspd; // zet de attack cooldown goed
        }
    }

    // functie voor wanneer de speler van item switcht 
    public void RemoveItem()
    {
        selectedItem = null; // zet selectedItem uit
    }

    // functie voor het weghalen van een hart
    public void RemoveHeart()
    {
        if (heartsActive.Count > 0) // check of de speler niet dood is
        {
            GameObject lastHeart = heartsActive[^1]; // selecteer het hart die weg moet

            string imageName = "Sprites/HeartBlack"; // vind de sprite voor het hart
            Sprite image = Resources.Load<Sprite>(imageName); // laad de sprite

            lastHeart.GetComponent<Image>().sprite = image; // zet het zwarte hart in de image van het hart

            heartsActive.RemoveAt(heartsActive.Count - 1); // haal 1 hart weg

            if (heartsActive.Count <= 0) // check of de speler nu dood is
            {
                print("Player is dead!");
                menu.ContinueButton(); // zet pauze menu uit zodat je niet twee menus tegelijk hebt
                isPaused = true; // zet spel op pauze
                isDead = true; // zet de speler op dood
                deathScreen.SetActive(true); // zet de dead screen aan
                menu.lockCursor = false; // zet de cursor lock uit
                menu.ResetCursor(); // zorg dat de cursor lock gerefreshed wordt
            }
        }
    }

    // functie voor een hart toevoegen
    public void AddHeart()
    {
        if (heartsActive.Count < hearts.Count) // check of de harten die de speler over heeft minder is dan het totale aantal harten zodat je niet meer krijgt
        {
            GameObject newHeart = hearts[heartsActive.Count]; // zet het hart dat aan moet in newHeart

            heartsActive.Add(newHeart); // zet het hart weer aan

            // zet de sprite van het hart naar HeartRed
            string imageName = "Sprites/HeartRed";
            Sprite image = Resources.Load<Sprite>(imageName);
            newHeart.GetComponent<Image>().sprite = image;
        }
    }

    // functie voor munten toevoegen
    public void AddCoins(int coinAmountToAdd)
    {
        totalCoins += coinAmountToAdd; // coins toevoegen aan totaal
        coinText.text = "" + totalCoins; // text die munten weergeeft updaten
    }

    // functie voor einde van het spel
    public void TheEnd()
    {
        menu.ContinueButton(); // zet pauze menu uit zodat je niet twee menus tegelijk hebt
        isPaused = true; // zet spel op pauze
        isDead = true; // zet de speler op dood
        endScreen.SetActive(true); // zet de dead screen aan
        menu.lockCursor = false; // zet de cursor lock uit
        menu.ResetCursor(); // zorg dat de cursor lock gerefreshed wordt
    }
}
