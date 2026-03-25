using UnityEngine;
using TMPro;

public class Backstab : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public TMP_Text feedbackText;

    [Header("Settings")]
    public float attackRange = 2f;
    [Range(-1f, 1f)]
    public float backstabThreshold = 0.5f;
    public float normalDamage = 20f;

    void Start()
    {

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
            else
                Debug.LogWarning("No GameObject with tag 'Player' found in the scene!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryBackstab();
    }

    void TryBackstab()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float nearestDist = Mathf.Infinity;

        foreach (var e in enemies)
        {
            float dist = Vector3.Distance(player.position, e.transform.position);
            if (dist <= attackRange && dist < nearestDist)
            {
                nearest = e;
                nearestDist = dist;
            }
        }

        if (nearest == null)
        {
            ShowFeedback("No enemy in range");
            return;
        }

        Vector3 enemyToPlayer = (player.position - nearest.transform.position).normalized;
        float dot = Vector3.Dot(nearest.transform.forward, enemyToPlayer);

        Health enemyHealth = nearest.GetComponent<Health>();
        if (enemyHealth == null)
        {
            Debug.LogWarning("Enemy has no Health component!");
            return;
        }

        if (dot <= backstabThreshold)
        {
            ShowFeedback("Backstab Successful!");
            enemyHealth.TakeDamage(enemyHealth.maxHealth);
        }
        else
        {
            ShowFeedback("Hit!");
            enemyHealth.TakeDamage(normalDamage);
        }
    }

    void ShowFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            CancelInvoke(nameof(ClearFeedback));
            Invoke(nameof(ClearFeedback), 2f);
        }
        else
        {
            Debug.Log(message);
        }
    }

    void ClearFeedback()
    {
        if (feedbackText != null)
            feedbackText.text = "";
    }
}
