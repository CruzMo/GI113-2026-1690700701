using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Goal : MonoBehaviour
{
    [SerializeField] private AudioClip completeSound;
    private bool isCompleted = false;

    private void Awake()
    {
        var col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When the player touches it (the first time only)
        if (isCompleted || !Application.isPlaying) return;

        if (collision.CompareTag("Player") || collision.gameObject.name == "Player")
        {
            var player = collision.GetComponent<PlayerController>();
            if (player == null)
            {
                player = collision.GetComponentInParent<PlayerController>();
            }

            if (player != null)
            {
                isCompleted = true;
                player.CompleteLevel(completeSound);
            }
        }
    }
}

