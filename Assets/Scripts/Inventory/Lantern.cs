using UnityEngine;

public class Lantern : MonoBehaviour
{
    public bool hasLantern = false;
    public GameObject lanternInHand;

    public void PickUpLantern()
    {
        lanternInHand.SetActive(true);
        Debug.Log("New lantern enabled!");

        hasLantern = true;
        Debug.Log("Lantern picked up, hasLantern is now true.");
    }
}
