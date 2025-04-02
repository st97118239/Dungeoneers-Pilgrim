using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] private Player player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Player>();
            if (player.heartsActive.Count > 0)
            {
                player.RemoveHeart();
            }
        }
        else if (!collision.gameObject.CompareTag("Golem"))
        {
            Destroy(gameObject);
        }
    }
}
