using UnityEngine;

public class Lantern : MonoBehaviour
{
    // Boolean to track if the player has picked up the lantern
    public bool hasLantern = false;

    // The new lantern object to enable after pickup
    public GameObject newLantern;

    // This function will be called when the player picks up the lantern
    public void PickUpLantern()
    {
        // Enable the new lantern object (assuming it's a different lantern prefab or model)
        newLantern.SetActive(true);
        Debug.Log("New lantern enabled!");

        // Set the boolean to true to indicate that the player has the lantern
        hasLantern = true;
        Debug.Log("Lantern picked up, hasLantern is now true.");
    }
}
