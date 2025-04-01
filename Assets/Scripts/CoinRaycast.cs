using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinRaycast : MonoBehaviour
{
    public float raycastDistance = 5f;  // Maximum distance the ray can detect
    public int totalCoins = 0;  // Total amount of coins the player has
    public LayerMask coinLayer;  // Layer mask to detect coins
    public TMP_Text promptText;  // Reference to the UI Text element for the prompt

    private RaycastHit hit; // To store the hit information

    void Update()
    {
        // Raycast from the camera to detect coins in front of the player
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, raycastDistance, coinLayer))
        {
            // Check if the hit object is a coin stack
            CoinStack coinStack = hit.collider.GetComponent<CoinStack>();
            if (coinStack != null)
            {
                // Show the prompt text on the screen
                promptText.gameObject.SetActive(true);

                // Check if the player presses the "E" key to collect the coin stack
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Add coins to the player's total based on the coin stack's value
                    totalCoins += coinStack.GetCoinAmount();

                    // Output to the console (optional for debugging)
                    Debug.Log("Coins collected: " + coinStack.GetCoinAmount());
                    Debug.Log("Total coins: " + totalCoins);

                    // Destroy the coin stack after collecting it
                    Destroy(hit.collider.gameObject);

                    // Hide the prompt text after collecting
                    promptText.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            // Hide the prompt text if no coin stack is detected
            promptText.gameObject.SetActive(false);
        }
    }
}