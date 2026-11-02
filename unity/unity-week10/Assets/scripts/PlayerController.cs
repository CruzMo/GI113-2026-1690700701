using System.Data.SqlTypes;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
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
    Vector2 move;

    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text debugText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprites = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
        // เดินซ้ายขาว กระโดดขึ้น
        move = InputSystem.actions.FindAction("move").ReadValue<Vector2>();
        if (InputSystem.actions.FindAction("jump").WasCompletedThisFrame() && isGrounded) 
        {
          rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }


        if (move.x > 0.1f)
        {
            sprites.flipX = false;
        }
        else if (move.x < -0.1f)
        {
            sprites.flipX = true;
        }

    }

    //50hz update
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(move.x * moveSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.GetContact(0).normal.y > 0.5f) 
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
     {
        isGrounded = false;
     }

    
      
    }




