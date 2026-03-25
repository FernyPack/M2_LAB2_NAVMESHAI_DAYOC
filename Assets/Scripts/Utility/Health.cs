using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBarFill;

    private bool hasDied = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        if (hasDied) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
            Die();
    }

    void UpdateHealthUI()
    {
        if (healthBarFill != null)
            healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    void Die()
    {
        if (hasDied) return;
        hasDied = true;

        if (CompareTag("Enemy"))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.AddKill();

            Destroy(gameObject);
        }
        else if (CompareTag("Player"))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.LoseLevel();
            CharacterController cc = GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            PlayerMovement pm = GetComponent<PlayerMovement>();
            if (pm != null) pm.enabled = false;

            FirstPersonLook fpl = GetComponent<FirstPersonLook>();
            if (fpl != null) fpl.enabled = false;
        }
    }
}