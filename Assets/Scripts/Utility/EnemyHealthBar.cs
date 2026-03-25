using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Health health;
    public Image fillImage;
    public Vector3 offset = new Vector3(0, 2f, 0);

    void Update()
    {
        if (health != null && fillImage != null)
        {
            fillImage.fillAmount = health.currentHealth / (float)health.maxHealth;

            transform.position = transform.parent.position + offset;

            Camera cam = Camera.main;
            if (cam != null)
                transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        }
    }
}
