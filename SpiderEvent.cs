using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is used to trigger the spider's attack, likely from an animation event
public class SpiderEvent : MonoBehaviour
{
    // Reference to the Spider script
    private Spider spider;

    // Called when the script starts
    private void Start()
    {
        // Get the Spider component from the parent GameObject
        spider = transform.parent.GetComponent<Spider>();
    }

    // Method to trigger the spider's attack (can be called from animation event)
    public void Fire()
    {
        // Call the Attack method on the spider
        spider.Attack();
    }
}
