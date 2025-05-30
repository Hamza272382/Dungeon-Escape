using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interface to be implemented by any damageable entity (e.g., player, enemies)
public interface IDamageable
{
    int Health { get; set; } // Property to get or set the health of the entity

    void Damage(); // Method to apply damage to the entity
}
