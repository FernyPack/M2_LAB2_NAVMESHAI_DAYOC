using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RandomPatrol : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 3f;

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavMeshPosition(wanderRadius);
            agent.SetDestination(newPos);
            timer = 0f;
        }
    }

    Vector3 RandomNavMeshPosition(float radius)
    {
        Vector3 randomDir = Random.insideUnitSphere * radius;
        randomDir += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, radius, NavMesh.AllAreas))
            return hit.position;

        return transform.position;
    }
}