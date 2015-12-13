using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Meleeist))]
[RequireComponent(typeof(CharacterMover))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("Controller number which will be used for this player.")]
    [Range(1, 4)]
    public int playerNumber = 1;

    [Tooltip("Color of this player")]
    public Color playerColor = Color.white;

    [Tooltip("Speed at which the player can dash")]
    [Range(0, 25)]
    public float dashSpeed = 5.0f;

    private InputNames inputNames;
    private Animator animator;
    private Meleeist meleeist;
    private CharacterMover characterMover;

    private const float dropDownForceRequired = 0.5f;

    void Awake()
    {
        inputNames = new InputNames(playerNumber);
        animator = GetComponent<Animator>();
        meleeist = GetComponent<Meleeist>();
        characterMover = GetComponent<CharacterMover>();
        characterMover.onControllerCollidedEvent += onControllerCollider;
        characterMover.onTriggerEnterEvent += onTriggerEnterEvent;
        characterMover.onTriggerStayEvent += onTriggerStayEvent;
        characterMover.onTriggerExitEvent += onTriggerExitEvent;
    }

    void Start()
    {
        SetColor();
    }

    void Update()
    {
        UpdateAnimation();
        characterMover.Move(Input.GetAxis(inputNames.HorizontalMovement));
        if (Input.GetButton(inputNames.Jump))
        {
            if (Input.GetAxis(inputNames.VerticalMovement) <= -dropDownForceRequired)
            {
                characterMover.DropDownPlatform();
            }
            else
            {
                characterMover.Jump();
            }
        }
        if (Input.GetButtonDown(inputNames.ChopAttack))
        {
            meleeist.ChopAttack();
        }
        if (Input.GetButtonDown(inputNames.LungeAttack))
        {
            meleeist.LungeAttack();
        }
        if (Input.GetButton(inputNames.Dash) && characterMover.IsGrounded())
        {
            //Vector2 dashForce = characterMover.GetVelocity().normalized;
            //dashForce.x += Input.GetAxis(inputNames.HorizontalMovement) * dashSpeed;
            //dashForce.y += Input.GetAxis(inputNames.VerticalMovement) * dashSpeed;
            if(Input.GetAxis(inputNames.HorizontalMovement) < 0)
            {
                characterMover.AddForce(new Vector2(-dashSpeed, 0));
            }
            else if (Input.GetAxis(inputNames.HorizontalMovement) > 0)
            {
                characterMover.AddForce(new Vector2(dashSpeed, 0));
            }
        }
    }

    void onControllerCollider(RaycastHit2D hit)
    {
        //Debug.Log("onControllerCollider: " + hit.transform.gameObject.name);
    }


    void onTriggerEnterEvent(Collider2D collider2D)
    {
        //Debug.Log("onTriggerEnterEvent: " + collider2D.gameObject.name);
    }

    void onTriggerStayEvent(Collider2D collider2D)
    {
        //Debug.Log("onTriggerStayEvent: " + collider2D.gameObject.name);
    }


    void onTriggerExitEvent(Collider2D collider2D)
    {
        //Debug.Log("onTriggerExitEvent: " + collider2D.gameObject.name);
    }

    private void SetColor()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            spriteRenderer.color = playerColor;
        }
    }

    private void UpdateAnimation()
    {
        animator.SetFloat(Animations.FloatHorizontalMovement, Mathf.Abs(characterMover.GetVelocity().x));
        animator.SetFloat(Animations.FloatVerticalMovement, characterMover.GetVelocity().y);
    }
}
