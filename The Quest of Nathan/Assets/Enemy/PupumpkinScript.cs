using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PupumpkinScript : Entity
{
    [SerializeField] GameObject positionPoints1;
    [SerializeField] GameObject positionPoints2;

    private Animator animator;

    SpriteRenderer spriteRenderer;

    Rigidbody2D pupumpkin;

    [SerializeField] float enemyMovementSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        pupumpkin = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        pupumpkin.velocity = new Vector2(-enemyMovementSpeed, pupumpkin.velocity.y);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > positionPoints1.transform.position.x)
        {
            spriteRenderer.flipX = false;
            MoveLeft();
        }
        else if (transform.position.x < positionPoints2.transform.position.x)
        {
            spriteRenderer.flipX = true;
            MoveRight();
        }

    }
    // Move to the right
    void MoveRight()
    {
        pupumpkin.velocity = new Vector2(enemyMovementSpeed, pupumpkin.velocity.y);
        animator.SetBool("walking", true);
    }
    // Move to the left
    void MoveLeft()
    {
        pupumpkin.velocity = new Vector2(-enemyMovementSpeed, pupumpkin.velocity.y);
        animator.SetBool("walking", true);
    }

    // When the player collides with the enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerInput player = collision.gameObject.GetComponent<PlayerInput>();
            player.TakeDamage();
        }
    }
}
