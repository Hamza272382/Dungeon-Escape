using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
   private bool _canDamage = true; // Flag to control if damage can be applied to avoid multiple hits in quick succession

   private void OnTriggerEnter2D(Collider2D other)
   {
        //Debug.Log("Hit: " + other.name); // Debug log to see which object was hit

        IDamageable hit = other.GetComponent<IDamageable>(); // Check if the collided object implements IDamageable interface

        if(hit != null)
        {
            if(_canDamage == true)
            {
                hit.Damage(); // Apply damage to the hit object
                _canDamage = false; // Prevent further damage until reset
                StartCoroutine(ResetDamage()); // Start cooldown before next possible damage
            }
        }
   }

   // Coroutine to reset the ability to damage after a short delay
   IEnumerator ResetDamage()
   {
        yield return new WaitForSeconds(0.5f); // Wait half a second before allowing damage again
        _canDamage = true;
   }
}
