using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles the behavior of an acid projectile in the game.
public class AcidEffect : MonoBehaviour
{
    // Called when the script is first run
    private void Start()
    {
        // Destroy the acid projectile after 5 seconds to clean up the scene
        Destroy(this.gameObject, 5.0f);
    }

    // Called once per frame
    private void Update()
    {
        // Move the acid projectile to the right at a constant speed
        transform.Translate(Vector3.right * 3 * Time.deltaTime);
    }

    // Called when the acid projectile enters a trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object it collided with has the tag "Player"
        if (other.tag == "Player")
        {
            // Try to get the IDamageable component from the player
            IDamageable hit = other.GetComponent<IDamageable>();

            // If the player has an IDamageable component, apply damage
            if (hit != null)
            {
                hit.Damage(); // Call the Damage method
                //Debug.Log("Played Damaged!"); // (Commented out debug log)
                
                // Destroy the acid projectile after hitting the player
                Destroy(this.gameObject);
            }
        }
    }
}
