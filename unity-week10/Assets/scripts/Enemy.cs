using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] public float leftDistance = 2.5f;
    [SerializeField] public float rightDistance = 2.5f;
    [SerializeField] public float moveSpeed = 1.5f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Vector3 originPosition;
    private float leftLimitX;
    private float rightLimitX;
    private int direction = 1; // 1 = moving right, -1 = moving left

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set Kinematic as required
        rb.bodyType = RigidbodyType2D.Kinematic;

        originPosition = transform.position;
        leftLimitX = originPosition.x - leftDistance;
        rightLimitX = originPosition.x + rightDistance;
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = rb.position;
        float newX = currentPos.x + direction * moveSpeed * Time.fixedDeltaTime;

        if (direction > 0 && newX >= rightLimitX)
        {
            newX = rightLimitX;
            direction = -1;
        }
        else if (direction < 0 && newX <= leftLimitX)
        {
            newX = leftLimitX;
            direction = 1;
        }

        rb.MovePosition(new Vector2(newX, currentPos.y));

        // Flip sprite to face movement direction
        if (spriteRenderer != null)
        {
            // Kenney sprites face right by default; flipX = true when moving left
            spriteRenderer.flipX = direction < 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When touching the player, deal damage (player loses 1 life)
        if (collision.CompareTag("Player") || collision.gameObject.name == "Player")
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player == null)
            {
                player = collision.GetComponentInParent<PlayerController>();
            }

            if (player != null)
            {
                player.TakeDamage();
            }
        }
    }

    private void OnDrawGizmos()
    {
        // While editing it follows the enemy when moved; in Play mode it stays where the enemy started
        Vector3 basePos = Application.isPlaying ? originPosition : transform.position;
        Vector3 leftLimit = new Vector3(basePos.x - leftDistance, basePos.y, basePos.z);
        Vector3 rightLimit = new Vector3(basePos.x + rightDistance, basePos.y, basePos.z);

        // Draw line between the two limits at the enemy's height
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(leftLimit, rightLimit);

        // Red wire sphere at the left limit
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(leftLimit, 0.2f);

        // Green wire sphere at the right limit
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(rightLimit, 0.2f);
    }
}

