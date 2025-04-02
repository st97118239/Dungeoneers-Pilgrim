using UnityEngine;
using UnityEngine.AI;

public class GolemAI : MonoBehaviour
{
    public Transform player;
    public float attackRange = 5f;
    public float stopRange = 2.5f;  // The range at which the golem should stop and start to flee
    public float fleeDistance = 5f;  // The range the golem will try to keep from the player
    public float stayCloseTo = 4f;
    public bool isActive = false;
    public bool isThrowing = false;
    public NavMeshAgent agent;
    
    private bool isFleeing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer >= 15 && isActive || player.GetComponent<Player>().isDead)
        {
            agent.isStopped = true;
            isActive = false;
        }
        else if (distanceToPlayer < 15 && !player.GetComponent<Player>().isDead)
        {
            isActive = true;
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

            // Rotate the golem to always face the player
            RotateTowardsPlayer();
        }
    }

    void Flee()
    {
        isFleeing = true;

        Vector3 directionAwayFromPlayer = transform.position - player.position;
        directionAwayFromPlayer.Normalize();
        Vector3 fleeDestination = transform.position + directionAwayFromPlayer * fleeDistance;
        
        // Set the destination to a point that is a safe flee distance away from the player
        agent.SetDestination(fleeDestination);
    }

    void MoveTowardsPlayer(float distanceToPlayer)
    {
        // Only move towards the player if we are farther than stopRange
        if (distanceToPlayer > stayCloseTo)
        {
            isFleeing = false;  // Stop fleeing when moving towards the player
            agent.isStopped = false;

            // Calculate the point just at the stopRange distance from the player
            Vector3 directionToPlayer = player.position - transform.position;
            Vector3 desiredPosition = player.position - directionToPlayer.normalized * stayCloseTo;

            // Set the agent's destination to this desired position (stop at the stopRange)
            agent.SetDestination(desiredPosition);
        }
        else
        {
            // If we are within stopRange, stop the movement
            agent.isStopped = true;
        }
    }

    // Method to rotate the golem to always face the player
    void RotateTowardsPlayer()
    {
        // Calculate the direction to the player
        Vector3 directionToPlayer = player.position - transform.position;

        // Create the rotation that looks towards the player
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

        // Smoothly rotate towards the player
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // 5f is the speed of rotation
    }

    void ThrowStone()
    {
        isThrowing = true;
        agent.isStopped = true;

        // Get the ThrowStone script and call the throw method
        GolemThrow throwScript = GetComponent<GolemThrow>();
        throwScript.ThrowStone(player.position);
    }
}