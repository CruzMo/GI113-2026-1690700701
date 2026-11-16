using System.Collections;
using UnityEngine;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class Door : MonoBehaviour
{
    [Header("Door Requirements")]
    [SerializeField] private Key requiredKey;
    [Min(1)]
    [SerializeField] private int keysNeeded = 1;
    [SerializeField] private KeyColor doorColor = KeyColor.Blue;

    [Header("Child References")]
    [SerializeField] private SpriteRenderer bottomRenderer;
    [SerializeField] private SpriteRenderer topRenderer;
    [SerializeField] private Collider2D doorCollider;
    [SerializeField] private GameObject indicatorRoot;
    [SerializeField] private SpriteRenderer indicatorIcon;
    [SerializeField] private TextMeshPro indicatorText;

    [Header("Door Sprites")]
    [SerializeField] private Sprite closedBottom;
    [SerializeField] private Sprite closedTop;
    [SerializeField] private Sprite openBottom;
    [SerializeField] private Sprite openTop;

    [Header("HUD Key Icons")]
    [SerializeField] private Sprite hudKeyBlue;
    [SerializeField] private Sprite hudKeyGreen;
    [SerializeField] private Sprite hudKeyRed;
    [SerializeField] private Sprite hudKeyYellow;

    [Header("Audio")]
    [SerializeField] private AudioClip openSound;

    private bool isOpen = false;
    private Coroutine feedbackCoroutine;

    public Key RequiredKey
    {
        get => requiredKey;
        set
        {
            requiredKey = value;
            if (requiredKey != null)
            {
                doorColor = requiredKey.Color;
            }
            UpdateVisuals();
        }
    }

    public int KeysNeeded
    {
        get => keysNeeded;
        set
        {
            keysNeeded = Mathf.Max(1, value);
            UpdateVisuals();
        }
    }

    public KeyColor Color
    {
        get => doorColor;
        set
        {
            doorColor = value;
            UpdateVisuals();
        }
    }

    private void Awake()
    {
        FindComponents();
#if UNITY_EDITOR
        LoadSpritesIfEmpty();
#endif
        UpdateVisuals();
    }

    private void OnValidate()
    {
        if (requiredKey != null)
        {
            doorColor = requiredKey.Color;
        }

        if (keysNeeded < 1)
        {
            keysNeeded = 1;
        }

        FindComponents();
#if UNITY_EDITOR
        LoadSpritesIfEmpty();
#endif
        UpdateVisuals();
    }

    public void FindComponents()
    {
        if (bottomRenderer == null)
        {
            var bottomTrans = transform.Find("Bottom");
            if (bottomTrans != null) bottomRenderer = bottomTrans.GetComponent<SpriteRenderer>();
        }

        if (topRenderer == null)
        {
            var topTrans = transform.Find("Top");
            if (topTrans != null) topRenderer = topTrans.GetComponent<SpriteRenderer>();
        }

        if (doorCollider == null)
        {
            doorCollider = GetComponent<Collider2D>();
        }

        if (indicatorRoot == null)
        {
            var indTrans = transform.Find("Indicator");
            if (indTrans != null) indicatorRoot = indTrans.gameObject;
        }

        if (indicatorRoot != null)
        {
            if (indicatorIcon == null)
            {
                var iconTrans = indicatorRoot.transform.Find("Icon");
                if (iconTrans != null) indicatorIcon = iconTrans.GetComponent<SpriteRenderer>();
            }

            if (indicatorText == null)
            {
                var textTrans = indicatorRoot.transform.Find("Text");
                if (textTrans != null) indicatorText = textTrans.GetComponent<TextMeshPro>();
            }
        }
    }

    public void UpdateVisuals()
    {
        if (isOpen)
        {
            if (bottomRenderer != null && openBottom != null) bottomRenderer.sprite = openBottom;
            if (topRenderer != null && openTop != null) topRenderer.sprite = openTop;
            if (indicatorRoot != null) indicatorRoot.SetActive(false);
            return;
        }

        if (bottomRenderer != null && closedBottom != null) bottomRenderer.sprite = closedBottom;
        if (topRenderer != null && closedTop != null) topRenderer.sprite = closedTop;

        if (indicatorIcon != null)
        {
            indicatorIcon.sprite = GetHudSprite(doorColor);
        }

        if (indicatorText != null)
        {
            indicatorText.text = "x" + keysNeeded;
            indicatorText.color = UnityEngine.Color.white;
        }

        if (indicatorRoot != null)
        {
            indicatorRoot.SetActive(true);
        }
    }

    private Sprite GetHudSprite(KeyColor color)
    {
        switch (color)
        {
            case KeyColor.Blue: return hudKeyBlue;
            case KeyColor.Green: return hudKeyGreen;
            case KeyColor.Red: return hudKeyRed;
            case KeyColor.Yellow: return hudKeyYellow;
            default: return hudKeyBlue;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isOpen || !Application.isPlaying) return;
        TryOpen(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOpen || !Application.isPlaying) return;
        TryOpen(collision.gameObject);
    }

    private void TryOpen(GameObject playerObj)
    {
        if (isOpen) return;

        if (playerObj.CompareTag("Player") || playerObj.name == "Player")
        {
            var player = playerObj.GetComponent<PlayerController>();
            if (player == null)
            {
                player = playerObj.GetComponentInParent<PlayerController>();
            }

            if (player != null)
            {
                int held = player.GetKeyCount(doorColor);
                if (held >= keysNeeded)
                {
                    player.UseKeys(doorColor, keysNeeded);
                    OpenDoor();
                }
                else
                {
                    int missing = keysNeeded - held;
                    ShowMissingFeedback(missing);
                }
            }
        }
    }

    public void OpenDoor()
    {
        isOpen = true;

        if (bottomRenderer != null && openBottom != null)
        {
            bottomRenderer.sprite = openBottom;
        }

        if (topRenderer != null && openTop != null)
        {
            topRenderer.sprite = openTop;
        }

        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }

        if (openSound != null)
        {
            AudioSource.PlayClipAtPoint(openSound, transform.position);
        }

        if (indicatorRoot != null)
        {
            indicatorRoot.SetActive(false);
        }

        Debug.Log($"Door opened with {doorColor} keys!");
    }

    private void ShowMissingFeedback(int missing)
    {
        if (indicatorText != null)
        {
            if (feedbackCoroutine != null)
            {
                StopCoroutine(feedbackCoroutine);
            }
            feedbackCoroutine = StartCoroutine(MissingFeedbackRoutine(missing));
        }

        Debug.Log($"Door locked! Need {missing} more {doorColor} key(s).");
    }

    private IEnumerator MissingFeedbackRoutine(int missing)
    {
        indicatorText.text = $"Need {missing}!";
        indicatorText.color = UnityEngine.Color.red;

        yield return new WaitForSeconds(2.0f);

        indicatorText.text = "x" + keysNeeded;
        indicatorText.color = UnityEngine.Color.white;
        feedbackCoroutine = null;
    }

#if UNITY_EDITOR
    public void LoadSpritesIfEmpty()
    {
        string path = "Assets/Art/Kenney/kenney_new-platformer-pack-1.1/Sprites/Tiles/Default/";
        if (closedBottom == null) closedBottom = LoadTileSprite(path + "door_closed.png", "door_closed_0");
        if (closedTop == null) closedTop = LoadTileSprite(path + "door_closed_top.png", "door_closed_top_0");
        if (openBottom == null) openBottom = LoadTileSprite(path + "door_open.png", "door_open_0");
        if (openTop == null) openTop = LoadTileSprite(path + "door_open_top.png", "door_open_top_0");

        if (hudKeyBlue == null) hudKeyBlue = LoadTileSprite(path + "hud_key_blue.png", "hud_key_blue_0");
        if (hudKeyGreen == null) hudKeyGreen = LoadTileSprite(path + "hud_key_green.png", "hud_key_green_0");
        if (hudKeyRed == null) hudKeyRed = LoadTileSprite(path + "hud_key_red.png", "hud_key_red_0");
        if (hudKeyYellow == null) hudKeyYellow = LoadTileSprite(path + "hud_key_yellow.png", "hud_key_yellow_0");

        if (openSound == null) openSound = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Art/Kenney/kenney_new-platformer-pack-1.1/Sounds/sfx_disappear.ogg");
    }

    private static Sprite LoadTileSprite(string assetPath, string spriteName)
    {
        var assets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
        if (assets != null)
        {
            foreach (var a in assets)
            {
                if (a is Sprite s && s.name == spriteName) return s;
            }
        }
        return AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
    }
#endif
}

