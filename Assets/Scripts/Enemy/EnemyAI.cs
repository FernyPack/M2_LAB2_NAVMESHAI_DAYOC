using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(RandomPatrol))]
[RequireComponent(typeof(EnemyAggro))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    private RandomPatrol patrol;
    private EnemyAggro aggro;
    private Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        patrol = GetComponent<RandomPatrol>();
        aggro = GetComponent<EnemyAggro>();
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (aggro == null || patrol == null || agent == null) return;

        if (aggro.isAggro && player != null)
        {
            patrol.enabled = false;

            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            patrol.enabled = true;
        }
    }
}