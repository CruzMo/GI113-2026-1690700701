using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SpikeTrap : MonoBehaviour
{
    [Header("Hazard Settings")]
    [Tooltip("Delay in seconds before respawning the player")]
    [Range(0f, 2f)]
    [SerializeField] private float respawnDelay = 0.2f;

    [Tooltip("Whether touching the spike should deal damage (reduce player lives)")]
    [SerializeField] private bool dealDamage = true;

    [Header("Audio (Optional)")]
    [SerializeField] private AudioClip hitSound;

    private AudioSource audioSource;
    private bool isRespawning = false;

    public float RespawnDelay
    {
        get => respawnDelay;
        set => respawnDelay = Mathf.Max(0f, value);
    }

    public bool DealDamage
    {
        get => dealDamage;
        set => dealDamage = value;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        // Ensure collider is set to Trigger for hazard pass-through detection
        var col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isRespawning) return;

        if (collision.CompareTag("Player") || collision.gameObject.name == "Player")
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player == null)
            {
                player = collision.GetComponentInParent<PlayerController>();
            }

            if (player != null)
            {
                StartCoroutine(HazardRoutine(player));
            }
        }
    }

    private IEnumerator HazardRoutine(PlayerController player)
    {
        isRespawning = true;

        // Play trap sound if assigned
        if (hitSound != null)
        {
            if (audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
            }
            else
            {
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
            }
        }

        // Deal damage using the existing damage system
        if (dealDamage)
        {
            player.TakeDamage();
        }

        // Optional respawn delay
        if (respawnDelay > 0f)
        {
            yield return new WaitForSeconds(respawnDelay);
        }

        // Only respawn if the game is not paused (e.g., game over screen is not active)
        if (Time.timeScale > 0f)
        {
            // Call the existing respawn system
            player.Respawn();
        }

        isRespawning = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.2f, 0.2f, 0.6f);
        var col = GetComponent<Collider2D>();
        if (col != null)
        {
            Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
        }
    }
#endif
}

