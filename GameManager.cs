using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance; // Singleton instance

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Game Manager is Null;"); // Log warning if instance accessed before initialization
            }
            return _instance;
        }
    }

    public bool HasKeyToCastle { get; set; } // Flag to track if player has the castle key

    public Player Player { get; private set; } // Reference to the player object

    private void Awake()
    {
        _instance = this; // Assign singleton instance on Awake
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); // Find player in the scene and cache the reference
    } 
}
