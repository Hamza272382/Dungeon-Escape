using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GiantMoss is a specific enemy type that inherits from Enemy and implements the IDamageable interface
public class GiantMoss : Enemy, IDamageable
{
    // Property to store the current health (required by IDamageable)
    public int Health { get; set; }

    // Override the Init method to initialize GiantMoss-specific values
    public override void Init()
    {
        base.Init(); // Call base enemy initialization
        Health = base.health; // Set current health from base health
    }

    // Override Movement method (currently just calls base method)
    public override void Movement()
    {
        base.Movement(); // Use base class movement logic
    }

    // Called when the GiantMoss takes damage
    public void Damage()
    {
        // Do nothing if already dead
        if (isDead == true)
        {
            return;
        }

        Debug.Log("MossGiant:: Damage()");

        Health--; // Decrease health by 1
        anim.SetTrigger("Hit"); // Play hit animation
        isHit = true; // Mark as hit to pause patrol movement
        anim.SetBool("InCombat", true); // Switch to combat state

        // If health is depleted, trigger death sequence
        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death"); // Play death animation

            // Instantiate diamond drop at current position
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;

            // Set gems in the dropped diamond
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }
}
