using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveOffset = new Vector3(0, 0, 5);
    public float speed = 2f;
    [HideInInspector] public Vector3 deltaMovement;

    private Vector3 startPos;
    private Vector3 endPos;
    private bool movingToEnd = true;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + moveOffset;
    }

    void Update()
    {
        Vector3 oldPos = transform.position;

        if (movingToEnd)
            transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, endPos) < 0.05f)
            movingToEnd = false;
        else if (Vector3.Distance(transform.position, startPos) < 0.05f)
            movingToEnd = true;

        deltaMovement = transform.position - oldPos;
    }
}
