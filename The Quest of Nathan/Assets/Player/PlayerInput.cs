using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Player Animation
using UnityEngine.Animations;

public class PlayerInput : Entity
{

    // Audio
    public AudioSource audioSource;
    public AudioClip playerAttackSound;
    
    // Sprite Renderer and Animation
    Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    [SerializeField] private BoxCollider2D playerCollider;

    // Player Attack
    private bool isAttacking;
    [SerializeField] GameObject attackHitbox;
    SpriteRenderer attackHitboxSprite;


    // Create variables for player movement
    [SerializeField] int movementSpeed;
    private float horizontalInput;
    private float verticalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<BoxCollider2D>();
        attackHitboxSprite = attackHitbox.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
        PlayerAttack();
    }

    private void PlayerControl()
    {
        // Get input from player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 10);
            if (rb.velocity.y > .1f)
            {
                animator.SetBool("jumping", true);
            }
        }
        if (rb.velocity.y < -.1f)
        {
            animator.SetBool("jumping", false);
        }

        // Movement

        if (horizontalInput > 0)
        {
            sprite.flipX = false;
            animator.SetBool("running", true);
        }
        else if (horizontalInput < 0)
        {
            animator.SetBool("running", true);
            sprite.flipX = true;
        }   
        else
        {
            animator.SetBool("running", false);
        }

        // Move player
        rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
    }

    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            isAttacking = true;
            audioSource.PlayOneShot(playerAttackSound);
            animator.SetBool("attacking", true);
            attackHitboxSprite.enabled = true;
            // Attacking Cooldown
            StartCoroutine(AttackCooldown());

        }
    }

    // When the attackHitbox collides with an enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // attackHitbox collides with an enemy, not the player
        if(collision.gameObject.tag == "Enemy" && attackHitboxSprite.enabled == true)
        {
            // Get the enemy script
            Entity enemy = collision.gameObject.GetComponent<Entity>();
            // Call the TakeDamage function from the enemy script
            enemy.TakeDamage();
        }   
    }


    private bool IsGrounded()
    {
        // Player can only jump once if they are on the Ground or Platform Layer
        return Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Ground", "Platform"));
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        animator.SetBool("attacking", false);
        attackHitboxSprite.enabled = false;
    }
}
