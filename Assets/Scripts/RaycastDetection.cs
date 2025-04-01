using System.Collections;
using System.Collections.Generic;
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

    private RaycastHit hit;

    void Update()
    {
        // Raycast from the camera to detect objects in front of the player
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, raycastDistance))
        {
            // Check if the hit object is a coin stack
            CoinStack coinStack = hit.collider.GetComponent<CoinStack>();
            ChestScript chest = hit.collider.GetComponent<ChestScript>();
            if (coinStack != null)
            {
                // Show the prompt text for collecting coins
                pickUpPromptText.gameObject.SetActive(true);

                // Check if the player presses "E" to collect coins
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Add coins to the player's total
                    totalCoins += coinStack.GetCoinAmount();
                    coinText.text = "" + totalCoins;
                    Debug.Log("Coins collected: " + coinStack.GetCoinAmount());
                    Debug.Log("Total coins: " + totalCoins);

                    // Destroy the coin stack after collecting it
                    Destroy(hit.collider.gameObject);

                    // Hide the prompt text after collecting
                    pickUpPromptText.gameObject.SetActive(false);
                }
            }
            // Check if the hit object is a chest
            else if (chest != null && !chest.isOpened)
            {
                // Show the prompt text for opening the chest
                openPromptText.gameObject.SetActive(true);

                    // If the player presses "E" and the chest is not opened
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        // Open the chest
                        chest.OpenChest();
                        openPromptText.gameObject.SetActive(false); // Hide the prompt text after opening the chest
                    }
            }
            else if (!hit.collider.CompareTag("Usable"))
            {
                // Hide the prompt text if no relevant object is detected
                pickUpPromptText.gameObject.SetActive(false);
                openPromptText.gameObject.SetActive(false);
            }
        }
    }
}