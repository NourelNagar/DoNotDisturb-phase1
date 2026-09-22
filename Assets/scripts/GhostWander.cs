using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Makes a NavMeshAgent wander to random points within a radius of its
/// starting position, waiting a random amount of time between moves.
/// Attach this to the ghost GameObject (the one with a NavMeshAgent).
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class GhostWander : MonoBehaviour
{
    [Header("Wander Settings")]
    [Tooltip("How far from its current position the ghost can pick a new random destination")]
    public float wanderRadius = 10f;

    [Tooltip("Minimum seconds to wait before picking a new destination")]
    public float minWaitTime = 2f;

    [Tooltip("Maximum seconds to wait before picking a new destination")]
    public float maxWaitTime = 5f;

    [Tooltip("How many attempts to find a valid point on the NavMesh before giving up for this cycle")]
    public int maxSampleAttempts = 10;

    private NavMeshAgent _agent;
    private float _waitTimer;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        // Pick an initial destination right away
        SetNewRandomDestination();
        ResetWaitTimer();
    }

    private void Update()
    {
        // If the agent has arrived (or has no path), count down to the next move
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            _waitTimer -= Time.deltaTime;

            if (_waitTimer <= 0f)
            {
                SetNewRandomDestination();
                ResetWaitTimer();
            }
        }
    }

    private void ResetWaitTimer()
    {
        _waitTimer = Random.Range(minWaitTime, maxWaitTime);
    }

    private void SetNewRandomDestination()
    {
        for (int i = 0; i < maxSampleAttempts; i++)
        {
            // Pick a random point in a flat circle around the current position
            Vector2 randomCircle = Random.insideUnitCircle * wanderRadius;
            Vector3 randomPoint = transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);

            // Try to find a valid point on the NavMesh near that random point
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
            {
                _agent.SetDestination(hit.position);
                return;
            }
        }

        // If we failed every attempt, just try again next Update cycle
        Debug.LogWarning("GhostWander: couldn't find a valid NavMesh point after " + maxSampleAttempts + " attempts.");
    }
}
