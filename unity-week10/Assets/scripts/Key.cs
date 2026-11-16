using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Key : MonoBehaviour
{
    [SerializeField] private KeyColor keyColor = KeyColor.Blue;
    [SerializeField] private AudioClip pickupSound;

    [Header("Sprites")]
    [SerializeField] private Sprite spriteBlue;
    [SerializeField] private Sprite spriteGreen;
    [SerializeField] private Sprite spriteRed;
    [SerializeField] private Sprite spriteYellow;

    private SpriteRenderer spriteRenderer;

    public KeyColor Color
    {
        get => keyColor;
        set
        {
            keyColor = value;
            UpdateSprite();
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        var col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.isTrigger = true;
        }

#if UNITY_EDITOR
        LoadSpritesIfEmpty();
#endif
        UpdateSprite();
    }

    private void OnValidate()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

#if UNITY_EDITOR
        LoadSpritesIfEmpty();
#endif
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (spriteRenderer == null) return;

        switch (keyColor)
        {
            case KeyColor.Blue:
                if (spriteBlue != null) spriteRenderer.sprite = spriteBlue;
                break;
            case KeyColor.Green:
                if (spriteGreen != null) spriteRenderer.sprite = spriteGreen;
                break;
            case KeyColor.Red:
                if (spriteRed != null) spriteRenderer.sprite = spriteRed;
                break;
            case KeyColor.Yellow:
                if (spriteYellow != null) spriteRenderer.sprite = spriteYellow;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Application.isPlaying) return;

        if (collision.CompareTag("Player") || collision.gameObject.name == "Player")
        {
            var player = collision.GetComponent<PlayerController>();
            if (player == null)
            {
                player = collision.GetComponentInParent<PlayerController>();
            }

            if (player != null)
            {
                player.AddKey(keyColor);

                if (pickupSound != null)
                {
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position);
                }

                Destroy(gameObject);
            }
        }
    }

#if UNITY_EDITOR
    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.isTrigger = true;
        }
        LoadSpritesIfEmpty();
        UpdateSprite();
    }

    public void LoadSpritesIfEmpty()
    {
        string path = "Assets/Art/Kenney/kenney_new-platformer-pack-1.1/Sprites/Tiles/Default/";
        if (spriteBlue == null) spriteBlue = LoadTileSprite(path + "key_blue.png", "key_blue_0");
        if (spriteGreen == null) spriteGreen = LoadTileSprite(path + "key_green.png", "key_green_0");
        if (spriteRed == null) spriteRed = LoadTileSprite(path + "key_red.png", "key_red_0");
        if (spriteYellow == null) spriteYellow = LoadTileSprite(path + "key_yellow.png", "key_yellow_0");
        if (pickupSound == null) pickupSound = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Art/Kenney/kenney_new-platformer-pack-1.1/Sounds/sfx_gem.ogg");
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

