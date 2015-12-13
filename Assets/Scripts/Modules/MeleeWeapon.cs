using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [Tooltip("The damage dealt by this melee weapon")]
    [Range(0, 1000)]
    public float damage = 1.0f;

    [Tooltip("How far this melee weapon knocks back its collider")]
    public float knockBack = 5.0f;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform == transform.parent)
        {
            return;
        }
        ApplyDamageAndKnockBack(other.gameObject);
    }

    private void ApplyDamageAndKnockBack(GameObject target)
    {
        Health health = target.GetComponent<Health>();
        if (health)
        {
            health.ApplyDamage(damage);
        }
        CharacterMover characterMover = target.GetComponent<CharacterMover>();
        Rigidbody2D body2D = target.GetComponent<Rigidbody2D>();
        if (characterMover)
        {
            characterMover.AddForce((target.transform.position - transform.parent.transform.position).normalized * knockBack);
        }
        else if (body2D)
        {
            body2D.AddForce((target.transform.position - transform.parent.transform.position).normalized * knockBack);
        }
    }
}
