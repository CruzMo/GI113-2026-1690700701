using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Spring : MonoBehaviour
{
    [Header("Bounce Settings")]
    [Tooltip("Upward launch velocity applied to player (Normal jump is ~6.0)")]
    [Range(1f, 30f)]
    [SerializeField] private float bounceForce = 12.0f;

    [Tooltip("Minimum time between launches to prevent accidental double-launches")]
    [SerializeField] private float bounceCooldown = 0.25f;

    [Header("Visual & Animation")]
    [SerializeField] private Sprite springIdle;
    [SerializeField] private Sprite springActive;
    [SerializeField] private float bounceAnimationDuration = 0.3f;

    [Header("Audio")]
    [SerializeField] private AudioClip bounceSound;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private float lastBounceTime = -10f;
    private Coroutine bounceRoutine;

    public float BounceForce
    {
        get => bounceForce;
        set => bounceForce = Mathf.Max(0.1f, value);
    }

    public Sprite SpringIdle
    {
        get => springIdle;
        set => springIdle = value;
    }

    public Sprite SpringActive
    {
        get => springActive;
        set => springActive = value;
    }

    public AudioClip BounceSound
    {
        get => bounceSound;
        set => bounceSound = value;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (spriteRenderer != null && springIdle != null)
        {
            spriteRenderer.sprite = springIdle;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryBounce(collision.gameObject, collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryBounce(collision.gameObject, null);
    }

    private void TryBounce(GameObject candidate, Collision2D collision)
    {
        if (Time.time - lastBounceTime < bounceCooldown) return;

        if (candidate.CompareTag("Player") || candidate.name == "Player")
        {
            var playerRb = candidate.GetComponent<Rigidbody2D>();
            if (playerRb == null)
            {
                playerRb = candidate.GetComponentInParent<Rigidbody2D>();
            }

            if (playerRb == null) return;

            // Check if player lands from above
            if (collision != null && collision.contactCount > 0)
            {
                if (playerRb.position.y < transform.position.y - 0.2f)
                {
                    return; // Hit from below or side
                }
            }
            else
            {
                if (playerRb.position.y < transform.position.y - 0.2f)
                {
                    return;
                }
            }

            // Launch the player upwards
            lastBounceTime = Time.time;
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);

            // Play audio
            if (bounceSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(bounceSound);
            }
            else if (bounceSound != null)
            {
                AudioSource.PlayClipAtPoint(bounceSound, transform.position);
            }

            // Play animation
            var anim = GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetTrigger("Bounce");
            }
            else
            {
                if (bounceRoutine != null)
                {
                    StopCoroutine(bounceRoutine);
                }
                bounceRoutine = StartCoroutine(BounceAnimationRoutine());
            }

            Debug.Log($"[Spring] Launched player with bounce force {bounceForce}!");
        }
    }

    private IEnumerator BounceAnimationRoutine()
    {
        if (spriteRenderer != null && springActive != null)
        {
            spriteRenderer.sprite = springActive;
        }

        yield return new WaitForSeconds(bounceAnimationDuration);

        if (spriteRenderer != null && springIdle != null)
        {
            spriteRenderer.sprite = springIdle;
        }

        bounceRoutine = null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 start = transform.position;
        Vector3 end = start + Vector3.up * (bounceForce / 2.0f);
        Gizmos.DrawLine(start, end);
        Gizmos.DrawWireSphere(end, 0.2f);
    }
#endif
}

