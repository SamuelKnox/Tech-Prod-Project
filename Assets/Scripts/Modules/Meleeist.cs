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
    /// Swings the child sword via the Animator
    /// </summary>
    public void SwingWeapon()
    {
        if (!transform.Find(GameObjectNames.MeleeWeapon))
        {
            return;
        }
        animator.SetTrigger(AnimationParameters.TriggerSwingMeleeWeapon);
    }
}