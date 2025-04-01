using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public Transform chestTop;      // Reference to the chest top (part that rotates)
    public float openAngle = 90f;   // Angle of rotation when opening the chest
    public float rotationSpeed = 5f; // Speed of the rotation
    public bool isOpened = false;  // Whether the chest is opened

    private Quaternion closedRotation; // The initial closed rotation of the chest top
    private Quaternion openRotation;   // The rotation when the chest is opened

    void Start()
    {
        // Store the initial closed rotation of the chest top
        closedRotation = chestTop.rotation;

        // Calculate the open rotation based on the open angle
        openRotation = Quaternion.Euler(closedRotation.eulerAngles.x - openAngle, closedRotation.eulerAngles.y, closedRotation.eulerAngles.z);
    }

    public void OpenChest()
    {
        if (!isOpened)
        {
            isOpened = true;  // Mark the chest as opened
        }
    }

    void Update()
    {
        // If the chest is opened, start rotating the chest top
        if (isOpened)
        {
            // Smoothly rotate the chest top to the open position
            chestTop.rotation = Quaternion.Slerp(chestTop.rotation, openRotation, Time.deltaTime * rotationSpeed);
        }
    }
}