using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Meleeist))]
[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(Health))]
public class EnemyController : MonoBehaviour
{
    [Tooltip("Target which the Enemy will attempt to attack")]
    public GameObject target;

    [Tooltip("Range at which the enemy will swing their melee weapon")]
    public float rangeToAttack = 1.0f;

    [Tooltip("Range to stop pursing the target")]
    public float rangeToStop = 0.5f;

    private Animator animator;
    private Meleeist meleeist;
    private CharacterMover characterMover;
    private Health health;

    void Awake()
    {
        animator = GetComponent<Animator>();
        meleeist = GetComponent<Meleeist>();
        characterMover = GetComponent<CharacterMover>();
        health = GetComponent<Health>();
        characterMover.onControllerCollidedEvent += onControllerCollider;
        characterMover.onTriggerEnterEvent += onTriggerEnterEvent;
        characterMover.onTriggerStayEvent += onTriggerStayEvent;
        characterMover.onTriggerExitEvent += onTriggerExitEvent;
    }

    void Update()
    {
        UpdateAnimation();
        if (target)
        {
            if (Vector2.Distance(target.transform.position, transform.position) > rangeToStop)
            {
                characterMover.Move((target.transform.position - transform.position).normalized.x);
            }
            else
            {
                characterMover.Move(0);
            }
            if (Vector2.Distance(target.transform.position, transform.position) <= rangeToAttack)
            {
                meleeist.ChopAttack();
            }
        }
    }

    void onControllerCollider(RaycastHit2D hit)
    {
        //Debug.Log("onControllerCollider: " + hit.transform.gameObject.name);
    }


    void onTriggerEnterEvent(Collider2D col)
    {
        //Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
    }

    void onTriggerStayEvent(Collider2D col)
    {
        //Debug.Log("onTriggerStayEvent: " + col.gameObject.name);
    }


    void onTriggerExitEvent(Collider2D col)
    {
        //Debug.Log("onTriggerExitEvent: " + col.gameObject.name);
    }

    private void UpdateAnimation()
    {
        animator.SetFloat(Animations.FloatHorizontalMovement, Mathf.Abs(characterMover.GetVelocity().x));
        animator.SetFloat(Animations.FloatVerticalMovement, characterMover.GetVelocity().y);
    }
}
