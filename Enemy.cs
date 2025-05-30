using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract base class for all enemy types
public abstract class Enemy : MonoBehaviour
{
    // Enemy health points
    [SerializeField]
    protected int health;

    // Movement speed of the enemy
    [SerializeField]
    protected int speed;

    // Gems rewarded to the player when this enemy dies
    [SerializeField]
    protected int gems;

    // Patrol points for enemy movement
    [SerializeField]
    protected Transform pointA, pointB;

    // Current patrol target
    protected Vector3 currentTarget;

    // SpriteRenderer for flipping enemy sprite
    protected SpriteRenderer sprite;

    // Animator to control enemy animations
    protected Animator anim;

    // Indicates if the enemy was hit by the player
    protected bool isHit = false;

    // Reference to the player
    protected Player player;

    // Indicates if the enemy is dead
    protected bool isDead = false;

    // Prefab for the diamond drop when enemy is defeated
    public GameObject diamondPrefab;

    // Initialization logic for the enemy
    public virtual void Init()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Called every frame
    public virtual void Update()
    {
        // Skip movement if in idle animation and not in combat
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat") == false)
        {
            return;
        }

        // Allow movement only if enemy is alive
        if (isDead == false)
            Movement();
    }

    // Called on game start
    private void Start()
    {
        Init();
    }

    // Handles patrolling and tracking the player
    public virtual void Movement()
    {
        // Flip sprite based on current target
        if (currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        // Switch patrol target when reaching point A or B
        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            anim.SetTrigger("Idle"); // Play idle animation
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            anim.SetTrigger("Idle"); // Play idle animation
        }

        // Move toward the current target if not hit
        if (isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        // Check distance to player and exit combat if too far
        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        if (distance > 2.0f)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }

        // Determine direction to player and flip sprite accordingly during combat
        Vector3 direction = player.transform.localPosition - transform.localPosition;

        if (direction.x > 0 && anim.GetBool("InCombat") == true)
        {
            sprite.flipX = false;
        }
        else if (direction.x < 0 && anim.GetBool("InCombat") == true)
        {
            sprite.flipX = true;
        }
    }
}
