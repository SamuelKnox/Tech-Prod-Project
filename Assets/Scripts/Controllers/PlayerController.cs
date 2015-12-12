using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Meleeist))]
[RequireComponent(typeof(CharacterMover))]
public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Meleeist meleeist;
    private CharacterMover characterMover;

    void Awake()
    {
        animator = GetComponent<Animator>();
        meleeist = GetComponent<Meleeist>();
        characterMover = GetComponent<CharacterMover>();
        characterMover.onControllerCollidedEvent += onControllerCollider;
        characterMover.onTriggerEnterEvent += onTriggerEnterEvent;
        characterMover.onTriggerStayEvent += onTriggerStayEvent;
        characterMover.onTriggerExitEvent += onTriggerExitEvent;
    }

    void Update()
    {
        characterMover.Move(Input.GetAxis(InputNames.HorizontalMovement));
        if (Input.GetButtonDown(InputNames.Jump))
        {
            if (Input.GetAxis(InputNames.VerticalMovement) < 0)
            {
                characterMover.DropDownPlatform();
            }
            else
            {
                characterMover.Jump();
            }
        }
        if (Input.GetButtonDown(InputNames.MeleeAttack))
        {
            meleeist.SwingWeapon();
        }
    }

    void onControllerCollider(RaycastHit2D hit)
    {
        // bail out on plain old ground hits cause they arent very interesting
        if (hit.normal.y == 1f)
            return;

        // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
        //Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
    }


    void onTriggerEnterEvent(Collider2D col)
    {
        Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
    }

    void onTriggerStayEvent(Collider2D col)
    {
        Debug.Log("onTriggerStayEvent: " + col.gameObject.name);
    }


    void onTriggerExitEvent(Collider2D col)
    {
        Debug.Log("onTriggerExitEvent: " + col.gameObject.name);
    }
}
