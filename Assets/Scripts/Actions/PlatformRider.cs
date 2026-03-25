using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlatformRider : MonoBehaviour
{
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void LateUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, groundDistance + 0.2f))
        {
            if (hit.collider.CompareTag("MovingPlatform"))
            {
                MovingPlatform mp = hit.collider.GetComponent<MovingPlatform>();
                if (mp != null)
                    controller.Move(mp.deltaMovement);
            }
        }
    }
}
