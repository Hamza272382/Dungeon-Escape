using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int gems = 1; // Number of gems this diamond gives to the player

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") // Check if the collider belongs to the player
        {
            Player player = other.GetComponent<Player>(); // Get the Player component from the collider

            if(player != null)
            {
                player.AddGems(gems); // Add the gems to the player's total
                Destroy(this.gameObject); // Remove the diamond from the scene after collection
            }
        }
    }
}
