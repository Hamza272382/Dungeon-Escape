using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skeleton enemy class that inherits from Enemy and implements IDamageable interface
public class Skeleton : Enemy, IDamageable
{
    // Property required by IDamageable to track health
    public int Health { get; set; }

    // Initialize the skeleton enemy
    public override void Init()
    {
        base.Init();               // Call the base class Init method
        Health = base.health;      // Set the current health from base health
    }

    // Handle enemy movement (delegates to base movement logic)
    public override void Movement()
    {
        base.Movement();           // Use movement logic from base Enemy class
    }

    // Handle damage taken by the skeleton
    public void Damage()
    {
        // If already dead, exit early
        if (isDead == true)
        {
            return;
        }

        Debug.Log("Skeleton:: Damage()");

        Health--;                          // Decrease health
        anim.SetTrigger("Hit");           // Play hit animation
        isHit = true;                     // Mark as hit to stop patrol temporarily
        anim.SetBool("InCombat", true);   // Switch to combat animation state

        // If health is zero or less, trigger death logic
        if (Health < 1)
        {
            isDead = true;                         // Mark as dead
            anim.SetTrigger("Death");              // Play death animation

            // Spawn a diamond at the skeleton's position
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;

            // Set the number of gems in the dropped diamond
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }
}
