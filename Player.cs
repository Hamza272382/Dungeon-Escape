using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput; // For mobile input support

public class Player : MonoBehaviour, IDamageable
{
    // Components
    private Rigidbody2D _rigid;
    private PlayerAnimations _anim;
    private SpriteRenderer _sprite;
    private SpriteRenderer _swordSprite;

    // Movement and Jumping
    [SerializeField] private float _jumpForce = 10.0f;
    [SerializeField] private bool _grounded = false;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _speed = 3.0f;
    private bool _resetjumpNeeded = false;

    // Health and Gems
    public int Health { get; set; }
    public int Diamonds;

    void Start()
    {
        // Initialize components
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimations>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _swordSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = 4;
    }

    void Update()
    {
        Movement();
        CheckForGrounded();
    }

    void Movement()
    {
        // Read horizontal input (supports mobile using CrossPlatformInput)
        float move = CrossPlatformInputManager.GetAxis("Horizontal");

        // Prevent movement if dead
        if (Health < 1)
            return;

        // Handle sprite flipping based on direction
        if (move > 0)
            Flip(true);
        else if (move < 0)
            Flip(false);

        // Attack with B button (e.g., mobile or controller)
        if (CrossPlatformInputManager.GetButtonDown("BButton") && _grounded == true)
        {
            _anim.Attack();
        }

        // Jump with A button if grounded
        if (CrossPlatformInputManager.GetButtonDown("AButton") && _grounded == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            _grounded = false;
            _resetjumpNeeded = true;
            StartCoroutine(ResetJumpNeededRoutine());
        }

        // Apply horizontal movement
        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);

        // Trigger animation states
        _anim.Move(move);
        _anim.Jump(true);
    }

    void CheckForGrounded()
    {
        // Use raycast to check if player is grounded
        RaycastHit2D _hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, _groundLayer.value);

        if (_hitInfo.collider != null)
        {
            Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.green);

            if (_resetjumpNeeded == false)
            {
                _anim.Jump(false);
                _grounded = true;
            }
        }
    }

    void Flip(bool FaceRight)
    {
        // Adjust sprite and sword direction based on facing side
        if (FaceRight == true)
        {
            _sprite.flipX = false;
            _swordSprite.flipX = false;
            _swordSprite.flipY = false;

            Vector3 newPos = _swordSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordSprite.transform.localPosition = newPos;
        }
        else
        {
            _sprite.flipX = true;
            _swordSprite.flipX = false;
            _swordSprite.flipY = true;

            Vector3 newPos = _swordSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordSprite.transform.localPosition = newPos;
        }
    }

    public void Damage()
    {
        if (Health < 1)
            return;

        Debug.Log("Player Damaged!");
        Health--;

        // Update UI health
        UIManager.Instance.UpdateLives(Health);

        if (Health < 1)
        {
            _anim.Death();
        }
    }

    IEnumerator ResetJumpNeededRoutine()
    {
        // Short delay before allowing next jump detection
        yield return new WaitForSeconds(0.1f);
        _resetjumpNeeded = false;
    }

    public void AddGems(int amount)
    {
        // Increase diamond count and update UI
        Diamonds += amount;
        UIManager.Instance.UpdateGemCount(Diamonds);
    }
}
