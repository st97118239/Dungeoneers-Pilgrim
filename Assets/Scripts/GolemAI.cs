using UnityEngine;
using UnityEngine.AI;

public class GolemAI : MonoBehaviour
{
    public float attackRange = 5f;
    public float stopRange = 2.5f;
    public float fleeDistance = 5f;
    public float stayCloseTo = 4f;
    public float playerDetectionRange = 15f;
    public bool isActive = false;
    public bool isThrowing = false;
    public bool isDead = false;
    public NavMeshAgent agent;

    private Transform player;
    private bool isFleeing = false;
    private bool explosionPlayed = false;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Transform>();

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer >= playerDetectionRange && isActive || Player.isPaused || isDead)
        {
            agent.isStopped = true;
            isActive = false;
        }
        else if (distanceToPlayer < playerDetectionRange && !Player.isPaused && !isDead)
        {
            isActive = true;
        }

        if (isDead && !explosionPlayed)
        {
            explosionPlayed = true;
            ParticleSystem particle = GetComponentInChildren<ParticleSystem>();
            particle.Play();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponentInChildren<GolemDestroy>().StartCheck();

        }

        if (isActive)
        {
            if (distanceToPlayer <= stopRange && !isThrowing)
            {
                if (distanceToPlayer < fleeDistance)
                {
                    Flee();
                }
            }
            else if (distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer(distanceToPlayer);
            }
            else if (distanceToPlayer >= stopRange && distanceToPlayer <= attackRange && !isFleeing && !isThrowing)
            {
                ThrowStone();
            }

            RotateTowardsPlayer();
        }
    }

    void Flee()
    {
        isFleeing = true;

        Vector3 directionAwayFromPlayer = transform.position - player.position;
        directionAwayFromPlayer.Normalize();
        Vector3 fleeDestination = transform.position + directionAwayFromPlayer * fleeDistance;
        
        agent.SetDestination(fleeDestination);
    }

    void MoveTowardsPlayer(float distanceToPlayer)
    {
        if (distanceToPlayer > stayCloseTo)
        {
            isFleeing = false;
            agent.isStopped = false;

            Vector3 directionToPlayer = player.position - transform.position;
            Vector3 desiredPosition = player.position - directionToPlayer.normalized * stayCloseTo;

            agent.SetDestination(desiredPosition);
        }
        else
        {
            agent.isStopped = true;
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void ThrowStone()
    {
        isThrowing = true;
        agent.isStopped = true;

        GolemThrow throwScript = GetComponent<GolemThrow>();
        throwScript.ThrowStone(player.position);
    }
}