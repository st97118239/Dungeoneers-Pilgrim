using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] private Use player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Use>();
            if (player.hp > 0)
            {
                player.hp--;
            }
        }
        else if (!collision.gameObject.CompareTag("Golem"))
        {
            Destroy(gameObject);
        }
    }
}
