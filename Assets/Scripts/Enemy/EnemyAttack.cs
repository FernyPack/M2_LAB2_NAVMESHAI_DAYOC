using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage = 10f;
    public float attackRange = 2f;
    public float attackRate = 1f;
    private float nextAttackTime = 0f;

    private Transform player;
    private Health playerHealth;
    private EnemyAggro enemyAggro;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
            playerHealth = player.GetComponent<Health>();

        enemyAggro = GetComponent<EnemyAggro>();
    }

    void Update()
    {
        if (player == null || playerHealth == null || enemyAggro == null) return;

        if (!enemyAggro.isAggro) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + 1f / attackRate;
            Attack();
        }
    }

    void Attack()
    {
        if (playerHealth != null)
        {
            Debug.Log($"{name} attacks the player for {damage} damage!");
            playerHealth.TakeDamage(damage);
        }
    }
}