using UnityEngine;

public class GolemThrow : MonoBehaviour
{
    public GameObject stonePrefab;
    public Transform throwPoint;
    public float throwForce = 10f;
    public Vector3 offset = new Vector3(0f, 2f, 0f);

    private Enemy enemyScript;
    private GolemAI golemScript;
    private float cooldown;

    void Start()
    {
        enemyScript = GetComponent<Enemy>();
        cooldown = enemyScript.atkspd;
        golemScript = GetComponent<GolemAI>();
    }

    void Update()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            cooldown = 0f;

            golemScript.isThrowing = false;
        }
    }

    public void ThrowStone(Vector3 targetPosition)
    {
        GameObject stone = Instantiate(stonePrefab, throwPoint.position + offset, Quaternion.identity);

        Vector3 direction = (targetPosition - throwPoint.position).normalized;
        direction.y += 0.7f;

        Rigidbody rb = stone.GetComponent<Rigidbody>();
        rb.velocity = direction * throwForce;

        cooldown = enemyScript.atkspd;

        golemScript.agent.isStopped = false;
    }
}