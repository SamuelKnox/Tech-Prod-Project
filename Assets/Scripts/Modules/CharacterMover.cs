using UnityEngine;
using Prime31;
using System;

[RequireComponent(typeof(CharacterController2D))]
public class CharacterMover : MonoBehaviour
{
    [Tooltip("Speed at which the character can run left and right")]
    [Range(0, 25)]
    public float runSpeed = 8.0f;

    [Tooltip("How fast the character can change directions")]
    [Range(0, 100)]
    public float groundDamping = 20.0f;

    [Tooltip("How fast the character can change direction while in the air")]
    [Range(0, 100)]
    public float inAirDamping = 5.0f;

    [Tooltip("How high the character can jump")]
    [Range(0, 25)]
    public float jumpHeight = 3.0f;

    public event Action<RaycastHit2D> onControllerCollidedEvent;
    public event Action<Collider2D> onTriggerEnterEvent;
    public event Action<Collider2D> onTriggerStayEvent;
    public event Action<Collider2D> onTriggerExitEvent;

    private const float DropDownRate = 3.0f;

    private float normalizedHorizontalSpeed;
    private CharacterController2D characterController2D;
    private RaycastHit2D lastControllerColliderHit;
    private Vector2 velocity;
    private Rigidbody2D body2D;
    private bool jumping;


    void Awake()
    {
        characterController2D = GetComponent<CharacterController2D>();
        body2D = GetComponent<Rigidbody2D>();
        characterController2D.onControllerCollidedEvent += onCharacterControllerCollider;
        characterController2D.onTriggerEnterEvent += onCharacterTriggerEnterEvent;
        characterController2D.onTriggerStayEvent += onCharacterTriggerStayEvent;
        characterController2D.onTriggerExitEvent += onCharacterTriggerExitEvent;
    }

    void onCharacterControllerCollider(RaycastHit2D hit)
    {
        if (onControllerCollidedEvent != null)
        {
            onControllerCollidedEvent(hit);
        }
    }


    void onCharacterTriggerEnterEvent(Collider2D collider2D)
    {
        if (onTriggerEnterEvent != null)
        {
            onTriggerEnterEvent(collider2D);
        }
    }


    void onCharacterTriggerStayEvent(Collider2D collider2D)
    {
        if (onTriggerStayEvent != null)
        {
            onTriggerStayEvent(collider2D);
        }
    }

    void onCharacterTriggerExitEvent(Collider2D collider2D)
    {
        if (onTriggerExitEvent != null)
        {
            onTriggerExitEvent(collider2D);
        }
    }

    /// <summary>
    /// Moves the character left or right
    /// </summary>
    public void Move(float horizontalMovement)
    {
        if (horizontalMovement < 0)
        {
            normalizedHorizontalSpeed = -1;
            if (transform.localScale.x > 0f)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        else if (horizontalMovement > 0)
        {
            normalizedHorizontalSpeed = 1;
            if (transform.localScale.x < 0f)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            normalizedHorizontalSpeed = 0;
        }
    }

    /// <summary>
    /// Makes the character jump if they are able
    /// </summary>
    public void Jump()
    {
        jumping = true;
    }

    /// <summary>
    /// Drops the character down on a One Way Platform
    /// </summary>
    public void DropDownPlatform()
    {
        if (characterController2D.isGrounded)
        {
            velocity.y *= DropDownRate;
            characterController2D.ignoreOneWayPlatformsThisFrame = true;
        }
    }

    void Update()
    {
        if (characterController2D.isGrounded)
        {
            velocity.y = 0;
        }
        if (jumping)
        {
            ApplyJump();
        }
        ApplyHorizontalMovement();
        ApplyGravity();
        ApplyVelocity();

    }

    private void ApplyJump()
    {
        if (characterController2D.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -Physics2D.gravity.y);
        }
        jumping = false;
    }

    private void ApplyHorizontalMovement()
    {
        var smoothedMovementFactor = characterController2D.isGrounded ? groundDamping : inAirDamping;
        velocity.x = Mathf.Lerp(velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor);
    }

    private void ApplyGravity()
    {
        velocity += Physics2D.gravity * body2D.gravityScale * Time.deltaTime;
    }

    private void ApplyVelocity()
    {
        characterController2D.move(velocity * Time.deltaTime);
        velocity = characterController2D.velocity;
    }
}
