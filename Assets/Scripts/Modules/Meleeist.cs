using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Meleeist : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Changes the child sword
    /// </summary>
    public void EquipWeapon(MeleeWeapon meleeWeapon)
    {
        Destroy(transform.Find(GameObjectNames.MeleeWeapon).gameObject);
        meleeWeapon.gameObject.name = GameObjectNames.MeleeWeapon;
        meleeWeapon.transform.parent = transform;
    }

    /// <summary>
    /// Swings the child sword over the head of the Meleeist via the Animator
    /// </summary>
    public void ChopAttack()
    {
        MeleeWeapon meleeWeapon = null;
        var meleeWeaponTransform = transform.Find(GameObjectNames.MeleeWeapon) as Transform;
        if (meleeWeaponTransform)
        {
            meleeWeapon = meleeWeaponTransform.GetComponent<MeleeWeapon>();
        }
        if (!meleeWeapon)
        {
            return;
        }
        animator.SetTrigger(Animations.TriggerChopAttack);
    }

    /// <summary>
    /// Swings the child sword over the head of the Meleeist via the Animator
    /// </summary>
    public void LungeAttack()
    {
        MeleeWeapon meleeWeapon = null;
        var meleeWeaponTransform = transform.Find(GameObjectNames.MeleeWeapon) as Transform;
        if (meleeWeaponTransform)
        {
            meleeWeapon = meleeWeaponTransform.GetComponent<MeleeWeapon>();
        }
        if (!meleeWeapon)
        {
            return;
        }
        animator.SetTrigger(Animations.TriggerLungeAttack);
    }
}