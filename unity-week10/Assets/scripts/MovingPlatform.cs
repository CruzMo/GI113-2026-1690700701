using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class MovingPlatform : MonoBehaviour
{
    [Header("Waypoints")]
    [Tooltip("Starting position of the platform in world space")]
    [SerializeField] private Vector2 startPoint = new Vector2(-4.0f, 2.2f);

    [Tooltip("Ending position of the platform in world space")]
    [SerializeField] private Vector2 endPoint = new Vector2(-1.5f, 2.2f);

    [Header("Movement Settings")]
    [Tooltip("Movement speed of the platform")]
    [Range(0.1f, 20f)]
    [SerializeField] private float moveSpeed = 2.0f;

    [Tooltip("Pause duration (seconds) when reaching either end")]
    [SerializeField] private float waitTimeAtEnds = 0.2f;

    private Rigidbody2D rb;
    private BoxCollider2D boxCol;
    private Vector2 targetPoint;
    private float waitTimer = 0f;
    private bool isWaiting = false;

    // The player currently standing on top of this platform
    private Rigidbody2D standingPlayerRb;

    public Vector2 StartPoint
    {
        get => startPoint;
        set => startPoint = value;
    }

    public Vector2 EndPoint
    {
        get => endPoint;
        set => endPoint = value;
    }

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = Mathf.Max(0.1f, value);
    }

    public float WaitTimeAtEnds
    {
        get => waitTimeAtEnds;
        set => waitTimeAtEnds = Mathf.Max(0f, value);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.useFullKinematicContacts = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        targetPoint = endPoint;
    }

    private void FixedUpdate()
    {
        if (isWaiting)
        {
            waitTimer += Time.fixedDeltaTime;
            if (waitTimer >= waitTimeAtEnds)
            {
                isWaiting = false;
                waitTimer = 0f;
                targetPoint = (targetPoint == endPoint) ? startPoint : endPoint;
            }
            return;
        }

        Vector2 currentPos = rb.position;
        Vector2 newPos = Vector2.MoveTowards(currentPos, targetPoint, moveSpeed * Time.fixedDeltaTime);
        Vector2 deltaPos = newPos - currentPos;

        rb.MovePosition(newPos);

        // Move the standing player together with the platform so they don't slide or get left behind
        if (standingPlayerRb != null)
        {
            standingPlayerRb.position += deltaPos;
        }

        if (Vector2.Distance(newPos, targetPoint) < 0.005f)
        {
            isWaiting = true;
            waitTimer = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluatePlayerContact(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluatePlayerContact(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (IsPlayer(collision.gameObject))
        {
            if (standingPlayerRb != null && collision.gameObject == standingPlayerRb.gameObject)
            {
                standingPlayerRb = null;
            }
        }
    }

    private void EvaluatePlayerContact(Collision2D collision)
    {
        if (IsPlayer(collision.gameObject))
        {
            // Verify the player is standing on top of the platform (not underneath or beside it)
            float platformTop = transform.position.y + (boxCol != null ? boxCol.size.y * 0.5f : 0.32f) - 0.15f;
            if (collision.collider.bounds.min.y >= platformTop)
            {
                standingPlayerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            }
            else
            {
                if (standingPlayerRb != null && collision.gameObject == standingPlayerRb.gameObject)
                {
                    standingPlayerRb = null;
                }
            }
        }
    }

    private bool IsPlayer(GameObject obj)
    {
        return obj.CompareTag("Player") || obj.name == "Player";
    }

    private void OnDisable()
    {
        standingPlayerRb = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine((Vector3)startPoint, (Vector3)endPoint);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector3)startPoint, 0.2f);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere((Vector3)endPoint, 0.2f);
    }
}
