using UnityEngine;
using System.Collections;

public class EnemyAggro : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Detection Settings")]
    public float detectionRange = 10f;     
    public float viewAngle = 60f;         
    public LayerMask obstructionMask;      
    [HideInInspector] 
    public bool isAggro = false;

    private bool initialAggroDone = false;

    void Start()
    {
        StartCoroutine(InitialAggro());
    }

    IEnumerator InitialAggro()
    {
        isAggro = true;
        yield return null; 
        isAggro = false;
        initialAggroDone = true;
    }

    void Update()
    {
        CheckPlayerInSight();
    }

    void CheckPlayerInSight()
    {
        if (!initialAggroDone) return; 
        if (player == null) return;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            if (angle <= viewAngle / 2f)
            {
                if (!Physics.Raycast(transform.position + Vector3.up, directionToPlayer, distanceToPlayer, obstructionMask))
                {
                    isAggro = true;
                    return;
                }
            }
        }

        isAggro = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2f, 0) * transform.forward * detectionRange;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2f, 0) * transform.forward * detectionRange;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}
