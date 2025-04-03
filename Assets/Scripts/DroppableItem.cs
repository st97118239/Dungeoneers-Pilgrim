using UnityEngine;

public class DroppableItem : MonoBehaviour
{
    public bool isDropped;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3 && isDropped)
        {
            Destroy(GetComponent<Rigidbody>());
        }
    }
}
