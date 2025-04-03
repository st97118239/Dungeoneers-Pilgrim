using UnityEngine;

public class DroppableItem : MonoBehaviour
{
    public bool isDropped;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 7)
        {
            if (isDropped)
            {
                 Destroy(GetComponent<Rigidbody>());
            }
        }
    }
}
