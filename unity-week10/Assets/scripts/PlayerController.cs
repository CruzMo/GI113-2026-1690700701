
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] string playerName = "T4Y";

    [Range(1, 100)]
    [SerializeField] float moveSpeed = 5.5f;

    [SerializeField] float jumpForce = 5.5f;
    int totalCoin = 5;
    int coin;
    bool isGrounded = true;

    Rigidbody2D rb;
    SpriteRenderer sprites;
    Animator anim;
    Vector2 move;

    private static readonly int IsWalkingHash = Animator.StringToHash("isWalking");
    private static readonly int IsGroundedHash = Animator.StringToHash("isGrounded");
    private static readonly int HurtHash = Animator.StringToHash("Hurt");

    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text debugText;

    // Fall & Respawn
    public float fallLimit = -6.0f;
    Vector3 startPosition;

    [Header("Audio")]
    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip hurtSound;
    AudioSource audioSource;

    [Header("Lives & Health")]
    [SerializeField] int maxLives = 3;
    private int currentLives;
    [SerializeField] Image[] heartImages;
    [SerializeField] Sprite heartFull;
    [SerializeField] Sprite heartEmpty;
    [SerializeField] float invincibilityDuration = 1.0f;
    private bool isInvincible = false;
    private bool isDead = false;

    [Header("Game Over UI")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Button retryButton;

    [Header("Level Complete UI")]
    [SerializeField] GameObject levelCompletePanel;
    [SerializeField] Button restartButton;
    [SerializeField] AudioClip levelCompleteSound;

    [Header("Keys Inventory & HUD")]
    [SerializeField] private TMP_Text blueKeyText;
    [SerializeField] private TMP_Text greenKeyText;
    [SerializeField] private TMP_Text redKeyText;
    [SerializeField] private TMP_Text yellowKeyText;
    private int[] keyCounts = new int[4];

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprites = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        currentLives = maxLives;
        UpdateHeartsUI();
        UpdateKeysUI();

        if (coinText == null)
        {
            var coinTextObj = GameObject.Find("CoinText");
            if (coinTextObj != null) coinText = coinTextObj.GetComponent<TMP_Text>();
        }
        UpdateCoinUI();

        if (debugText != null && !string.IsNullOrEmpty(playerName))
        {
            debugText.text = playerName;
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (retryButton != null)
        {
            retryButton.onClick.AddListener(ReloadScene);
        }

        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(false);
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(ReloadScene);
        }
    }

    void Update()
    {
        // Pressing R reloads the scene at any time
        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            ReloadScene();
            return;
        }

        if (isDead)
        {
            return;
        }

        // Fall check
        if (transform.position.y < fallLimit)
        {
            FallDeath();
            return;
        }

        // Movement & Jump
        var moveAction = InputSystem.actions.FindAction("move");
        if (moveAction != null)
        {
            move = moveAction.ReadValue<Vector2>();
        }

        var jumpAction = InputSystem.actions.FindAction("jump");
        if (jumpAction != null && jumpAction.WasCompletedThisFrame() && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }

        // Sprite flip
        if (move.x > 0.1f)
        {
            sprites.flipX = false;
        }
        else if (move.x < -0.1f)
        {
            sprites.flipX = true;
        }

        // Animation parameters
        bool isWalking = Mathf.Abs(move.x) > 0.1f;
        if (anim != null)
        {
            anim.SetBool(IsWalkingHash, isWalking);
            anim.SetBool(IsGroundedHash, isGrounded);
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;
        rb.linearVelocity = new Vector2(move.x * moveSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contactCount > 0 && collision.GetContact(0).normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contactCount > 0 && collision.GetContact(0).normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin") || collision.gameObject.name.StartsWith("Coin"))
        {
            AddCoin();
            Destroy(collision.gameObject);
        }
    }

    public void AddCoin()
    {
        coin++;
        UpdateCoinUI();

        if (audioSource != null && coinSound != null)
        {
            audioSource.PlayOneShot(coinSound);
        }
        else if (coinSound != null)
        {
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
        }
    }

    public void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = $"Coin {coin}/{totalCoin}";
        }
    }

    public void TakeDamage()
    {
        if (isDead || isInvincible) return;

        currentLives--;
        UpdateHeartsUI();

        if (audioSource != null && hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }

        if (anim != null)
        {
            anim.SetTrigger(HurtHash);
        }

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            StartCoroutine(InvincibilityRoutine());
        }
    }

    private void FallDeath()
    {
        currentLives--;
        UpdateHeartsUI();

        if (audioSource != null && hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            Respawn();
        }
    }

    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        float elapsed = 0f;
        float blinkInterval = 0.1f;

        while (elapsed < invincibilityDuration)
        {
            if (sprites != null)
            {
                sprites.enabled = !sprites.enabled;
            }
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        if (sprites != null)
        {
            sprites.enabled = true;
        }
        isInvincible = false;
    }

    private void UpdateHeartsUI()
    {
        if (heartImages == null) return;

        for (int i = 0; i < heartImages.Length; i++)
        {
            if (heartImages[i] != null)
            {
                heartImages[i].sprite = (i < currentLives) ? heartFull : heartEmpty;
            }
        }
    }

    private void GameOver()
    {
        ShowEndScreen(gameOverPanel, retryButton, "Game Over!");
    }

    public void CompleteLevel(AudioClip customSound = null)
    {
        var sound = customSound != null ? customSound : levelCompleteSound;
        if (audioSource != null && sound != null)
        {
            audioSource.PlayOneShot(sound);
        }
        else if (sound != null)
        {
            AudioSource.PlayClipAtPoint(sound, transform.position);
        }

        ShowEndScreen(levelCompletePanel, restartButton, "Level Complete!");
    }

    private void ShowEndScreen(GameObject panel, Button buttonToSelect, string logMessage)
    {
        isDead = true;
        StopAllCoroutines();

        if (sprites != null)
        {
            sprites.enabled = true;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (anim != null)
        {
            anim.SetBool(IsWalkingHash, false);
        }

        if (panel != null)
        {
            panel.SetActive(true);
        }

        if (buttonToSelect != null && EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(buttonToSelect.gameObject);
        }

        Time.timeScale = 0f;
        Debug.Log(logMessage);
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        var activeScene = SceneManager.GetActiveScene();
        if (!string.IsNullOrEmpty(activeScene.name))
        {
            SceneManager.LoadScene(activeScene.name);
        }
        else if (activeScene.buildIndex >= 0)
        {
            SceneManager.LoadScene(activeScene.buildIndex);
        }
    }

    public void Respawn()
    {
        transform.position = startPosition;
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
        Debug.Log("Respawned! Back to start.");
    }

    public void AddKey(KeyColor color)
    {
        keyCounts[(int)color]++;
        UpdateKeysUI();
    }

    public int GetKeyCount(KeyColor color)
    {
        return keyCounts[(int)color];
    }

    public bool UseKeys(KeyColor color, int amount)
    {
        int idx = (int)color;
        if (keyCounts[idx] >= amount)
        {
            keyCounts[idx] -= amount;
            UpdateKeysUI();
            return true;
        }
        return false;
    }

    public void UpdateKeysUI()
    {
        if (blueKeyText != null) blueKeyText.text = keyCounts[(int)KeyColor.Blue].ToString();
        if (greenKeyText != null) greenKeyText.text = keyCounts[(int)KeyColor.Green].ToString();
        if (redKeyText != null) redKeyText.text = keyCounts[(int)KeyColor.Red].ToString();
        if (yellowKeyText != null) yellowKeyText.text = keyCounts[(int)KeyColor.Yellow].ToString();
    }
}
