using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : Entity {


    // Animation 
    SpriteRenderer spriteRenderer;
    Animator animator;

    // Variables for detection
    private Transform player;
    private float speed = 3.0f;
    [SerializeField] public float visionRange = 5f;
    [SerializeField] public float attackRange = 1.8f;
    private bool canAttack = true;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= visionRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime); // Move towards player
            animator.SetBool("walk", true);
        } else
        {
            animator.SetBool("walk", false);
        }

        if (distanceToPlayer <= attackRange && canAttack)
        {
            // Attack
            AttackPlayer();
            animator.SetBool("attack", true);
        }

        // Flip Sprite
        if (transform.position.x > player.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

    }

   private void AttackPlayer()
    {
        Debug.Log("Attack");
        canAttack = false;
        player.GetComponent<PlayerInput>().TakeDamage();
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        
        canAttack = true;
    }
}
