using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float moveDistance = 5.0f; // Distance the platform moves
    public float moveSpeed = 2.0f; // Speed at which the platform moves
    public Vector3 moveDirection = Vector3.up; // Direction of movement
    private Vector3 startPos;
    private Vector3 endPos;
    private bool movingTowardsEnd = true;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + moveDirection.normalized * moveDistance;
    }

    void Update()
    {
        float step = moveSpeed * Time.deltaTime;

        if (movingTowardsEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, step);
            if (Vector3.Distance(transform.position, endPos) < 0.001f)
                movingTowardsEnd = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, step);
            if (Vector3.Distance(transform.position, startPos) < 0.001f)
                movingTowardsEnd = true;
        }
    }
}
