using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyType; // 0 = skeleton, 1 = golem
    public float health;
    public float atkspd;
    public int coinAmount;
    public RaycastDetection playerCam;

    void Update()
    {
        if (health <= 0)
        {
            playerCam.totalCoins += coinAmount;
            Destroy(gameObject);
        }
    }
}
