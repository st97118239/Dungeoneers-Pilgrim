using UnityEngine;
using TMPro; // For using TextMeshPro

public class ChestRaycast : MonoBehaviour
{
    public float raycastDistance = 5f;   // Max distance for raycast detection
    public LayerMask chestLayer;         // Layer mask to detect chests
    public TextMeshProUGUI promptText;   // UI prompt text to show when looking at the chest

    private RaycastHit hit;              // Raycast hit info

    void Update()
    {
        // Raycast from the camera to detect if we're looking at a chest
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, raycastDistance, chestLayer))
        {
            // Check if the object hit by the raycast has a ChestScript attached
            ChestScript chest = hit.collider.GetComponent<ChestScript>();
            if (chest != null && !chest.GetComponent<ChestScript>().isOpened)
            {
                print("a");
                // Show the prompt text to indicate the player can open the chest
                promptText.gameObject.SetActive(true);

                // If the player presses E and the chest is not opened
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Open the chest by calling its OpenChest() method
                    chest.OpenChest();
                    promptText.gameObject.SetActive(false); // Hide the prompt text after opening the chest
                }
            }
        }
        else
        {
            // Hide the prompt text if we're not looking at a chest
            promptText.gameObject.SetActive(false);
        }
    }
}