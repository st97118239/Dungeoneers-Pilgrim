using TMPro;
using UnityEngine;

public class RaycastDetection : MonoBehaviour
{
    public float raycastDistance = 5f;
    public LayerMask coinLayer;
    public LayerMask chestLayer;
    public TMP_Text pickUpPromptText;
    public TMP_Text openPromptText;
    public Lantern lanternScript;
    public Inventory inventory;

    private Player player;
    private RaycastHit hit;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }

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
                    player.AddCoins(coinStack.GetCoinAmount());

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

                if (Input.GetKeyDown(KeyCode.E) && inventory.PickupLantern(lanternScript.lanternInHand))
                {
                    lanternScript.PickUpLantern();
                    Destroy(hit.collider.gameObject);
                    pickUpPromptText.gameObject.SetActive(false);
                }
            }
            else if (hit.collider.gameObject.CompareTag("Weapon"))
            {
                pickUpPromptText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E) && player.atkCooldown <= 0)
                {
                    inventory.PickupWeapon(hit.collider.gameObject);
                    pickUpPromptText.gameObject.SetActive(false);
                }
            }
            else if (hit.collider.gameObject.CompareTag("Egg"))
            {
                pickUpPromptText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.collider.gameObject);
                    pickUpPromptText.gameObject.SetActive(false);
                    GetComponentInParent<Player>().TheEnd();
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