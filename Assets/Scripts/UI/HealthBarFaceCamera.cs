using UnityEngine;

public class HealthBarFaceCamera : MonoBehaviour
{
    public Transform playerCamera;

    void LateUpdate()
    {
        if (playerCamera != null)
        {
            Vector3 direction = transform.position - playerCamera.position;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}