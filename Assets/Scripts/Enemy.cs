using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType; // 0 = skeleton, 1 = golem
    public float health;
    public float atkspd;
    public int coinAmount;

    private Player player;
    private bool addedCoins = false;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
    }

    void Update()
    {
        if (health <= 0)
        {
            if (enemyType == EnemyType.Golem)
            {
                GolemAI golemScript = GetComponent<GolemAI>();
                golemScript.isDead = true;
            }

            if (!addedCoins)
            {
                addedCoins = true;

                if (player.heartsActive.Count < player.hearts.Count)
                {
                    player.AddHeart();
                }
                else
                    player.AddCoins(coinAmount);

                
            }
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
