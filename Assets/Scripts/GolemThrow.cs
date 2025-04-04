using System.Collections.Generic;
using UnityEngine;

public class GolemThrow : MonoBehaviour
{
    public GameObject stonePrefab;
    public Transform throwPoint;
    public float throwForce = 10f;
    public Vector3 offset = new(0f, 1.5f, 0f);
    public float yArch = 0.7f;

    private Enemy enemyScript;
    private GolemAI golemScript;
    private readonly List<Stone> stonesThrown = new();
    private float cooldown;
    private bool isPaused = false;

    void Start()
    {
        enemyScript = GetComponent<Enemy>();
        cooldown = enemyScript.atkspd;
        golemScript = GetComponent<GolemAI>();
    }

    void Update()
    {
        if (!Player.isPaused && !Player.isDead)
        {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0)
            {
                cooldown = 0f;

                golemScript.isThrowing = false;
            }
        }

        if (Player.isPaused && !isPaused)
        {
            Pause();
        }
        else if (!Player.isPaused && isPaused)
        {
            Unpause();
        }
    }

    private void Unpause()
    {
        foreach (Stone stone in stonesThrown)
        {
            stone.ResumeStone();
        }

        isPaused = false;
    }

    private void Pause()
    {
        foreach (Stone stone in stonesThrown)
        {
            stone.PauseStone();
        }

        isPaused = true;
    }

    private void RemoveStone(Stone stone)
    {
        stonesThrown.Remove(stone);
    }

    public void ThrowStone(Vector3 targetPosition)
    {
        GameObject stone = Instantiate(stonePrefab, throwPoint.position + offset, Quaternion.identity);

        Stone stoneComponent = stone.GetComponent<Stone>();
        stoneComponent.DestroyFunc = RemoveStone;
        stonesThrown.Add(stoneComponent);

        Vector3 direction = (targetPosition - throwPoint.position).normalized;
        direction.y += yArch;

        Rigidbody rb = stone.GetComponent<Rigidbody>();
        rb.velocity = direction * throwForce;

        cooldown = enemyScript.atkspd;

        golemScript.agent.isStopped = false;
    }
}