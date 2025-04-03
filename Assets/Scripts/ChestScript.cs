using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public Transform chestTop;
    public float openAngle = 90f;
    public float rotationSpeed = 5f;
    public bool isOpened = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = chestTop.rotation;

        openRotation = Quaternion.Euler(closedRotation.eulerAngles.x - openAngle, closedRotation.eulerAngles.y, closedRotation.eulerAngles.z);
    }

    public void OpenChest()
    {
        if (!isOpened)
        {
            isOpened = true;
        }
    }

    void Update()
    {
        if (isOpened)
        {
            chestTop.rotation = Quaternion.Slerp(chestTop.rotation, openRotation, Time.deltaTime * rotationSpeed);
        }
    }
}