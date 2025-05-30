using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spider enemy class that inherits from Enemy and implements IDamageable interface
public class Spider : Enemy, IDamageable
{
    // Reference to the acid effect GameObject that the spider will shoot
    public GameObject AcidEffect;

    // Property for spider's health as required by IDamageable
    public int Health { get; set; }

    // Boolean flag to check if spider has finished dying and been destroyed
    public bool enemyDead = false;

    // Initialize spider's components and health
    public override void Init()
    {
        base.Init();            // Call base class Init method
        Health = base.health;   // Set current health from base health
    }

    // Override Update method (currently empty since spider might not patrol like other enemies)
    public override void Update()
    {
        // Intentionally left empty - this spider might be stationary or event-driven
    }

    // Handle damage taken by the spider
    public void Damage()
    {
        // Return if already dead
        if (isDead == true)
        {
            return;
        }

        Health--;   // Decrease health

        // If health is zero or less, trigger death
        if (Health < 1)
        {
            isDead = true;                        // Mark as dead
            anim.SetTrigger("Death");             // Play death animation

            // Instantiate a diamond at the spider's position
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;

            // Assign the number of gems to the diamond
            diamond.GetComponent<Diamond>().gems = base.gems;

            // Start coroutine to clean up the spider after a delay
            StartCoroutine(DeadRoutine());
        }
    }

    // Override Movement method (currently empty; spider likely doesn't patrol like others)
    public override void Movement()
    {
        // Intentionally left blank - spider does not patrol between points
    }

    // Handles spider's attack behavior (e.g., shooting acid)
    public void Attack()
    {
        // Instantiate the acid projectile at spider's current position
        Instantiate(AcidEffect, transform.position, Quaternion.identity);
    }

    // Coroutine to handle delay before destroying the spider GameObject
    IEnumerator DeadRoutine()
    {
        yield return new WaitForSeconds(1.0f); // Wait 1 second before removing the enemy
        enemyDead = true;                      // Mark as completely dead
        Destroy(this.gameObject);              // Remove spider from the scene
    }
}
