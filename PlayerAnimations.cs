using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _anim;        // Reference to the player's main animator
    private Animator _SworAnim;    // Reference to the sword's animator (child object)

    // Called once at the start
    void Start()
    {
        // Get the Animator component from the player's child object (body)
        _anim = GetComponentInChildren<Animator>();

        // Get the Animator component from the second child (sword)
        _SworAnim = transform.GetChild(1).GetComponent<Animator>();
    }

    // Sets the movement float parameter for blend tree animations
    public void Move(float move)
    {
        _anim.SetFloat("Move", Mathf.Abs(move)); // Use absolute value to ensure positive input
    }

    // Sets the jump boolean to control jump animation state
    public void Jump(bool jump)
    {
        _anim.SetBool("Jump", jump);
    }

    // Triggers the attack animation on both the player and the sword
    public void Attack()
    {
        _anim.SetTrigger("Attack");              // Trigger player attack animation
        _SworAnim.SetTrigger("ArcAnimation");    // Trigger sword swing animation
    }

    // Triggers the player's death animation
    public void Death()
    {
        _anim.SetTrigger("Death");               // Trigger death animation
    }
}
