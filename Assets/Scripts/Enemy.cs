using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyType; // 0 = skeleton, 1 = golem
    public float health;
    public float atkspd;
    public int coinAmount;

    private Player player;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
    }

    void Update()
    {
        if (health <= 0)
        {
            player.AddCoins(coinAmount);
            Destroy(gameObject);
        }
    }
}
