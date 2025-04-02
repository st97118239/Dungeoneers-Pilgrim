using TMPro;
using UnityEngine;

public class RaycastDetection : MonoBehaviour
{
    public float raycastDistance = 5f;
    public int totalCoins = 0;
    public LayerMask coinLayer;
    public LayerMask chestLayer;
    public TMP_Text coinText;
    public TMP_Text pickUpPromptText;
    public TMP_Text openPromptText;
    public Lantern lanternScript;

    public Inventory Inventory;

    private RaycastHit hit;

    void Update()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, raycastDistance))
        {
            CoinStack coinStack = hit.collider.GetComponent<CoinStack>();
            ChestScript chest = hit.collider.GetComponent<ChestScript>();

            if (coinStack != null)
            {
                pickUpPromptText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    totalCoins += coinStack.GetCoinAmount();
                    coinText.text = "" + totalCoins;
                    Debug.Log("Coins collected: " + coinStack.GetCoinAmount());
                    Debug.Log("Total coins: " + totalCoins);

                    Destroy(hit.collider.gameObject);

                    pickUpPromptText.gameObject.SetActive(false);
                }
            }
            else if (chest != null && !chest.isOpened)
            {
                openPromptText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    chest.OpenChest();
                    openPromptText.gameObject.SetActive(false);
                }
            }
            else if (hit.collider.gameObject.CompareTag("Lantern"))
            {
                pickUpPromptText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    lanternScript.PickUpLantern();
                    Destroy(hit.collider.gameObject);
                    Inventory.PickupLantern(lanternScript.lanternInHand);
                    pickUpPromptText.gameObject.SetActive(false);
                }
            }
            else if (hit.collider.gameObject.CompareTag("Weapon"))
            {
                pickUpPromptText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Inventory.PickupWeapon(hit.collider.gameObject);
                    pickUpPromptText.gameObject.SetActive(false);
                }
            }
            else
            {
                pickUpPromptText.gameObject.SetActive(false);
                openPromptText.gameObject.SetActive(false);
            }
        }
        else
        {
            pickUpPromptText.gameObject.SetActive(false);
            openPromptText.gameObject.SetActive(false);
        }
    }
}