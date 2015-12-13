using UnityEngine;

public class Health : MonoBehaviour
{
    [Tooltip("Current hit points")]
    [Range(0, 1000)]
    public float currentHitPoints = 1000;

    [Tooltip("Max hit points")]
    [Range(0, 1000)]
    public float maxHitPoints = 1000;

    [Tooltip("How long in seconds health cannot be reduced for after being hit")]
    public float invunerabilityCooldown = 1.0f;

    private float invunerabilityTime;

    void Awake()
    {
        currentHitPoints = Mathf.Min(currentHitPoints, maxHitPoints);
        invunerabilityTime = invunerabilityCooldown;
    }

    void Update()
    {
        UpdateInvunerabilityCooldown();
        if (IsDead())
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Reduces the current hit points
    /// </summary>
    public bool ApplyDamage(float damage)
    {
        if(invunerabilityCooldown > 0)
        {
            return false;
        }
        invunerabilityCooldown = invunerabilityTime;
        currentHitPoints -= damage;
        currentHitPoints = Mathf.Max(0, currentHitPoints);
        return true;
    }

    /// <summary>
    /// Increases the current hit points
    /// </summary>
    public void Heal(float health)
    {
        currentHitPoints += health;
        currentHitPoints = Mathf.Min(currentHitPoints, maxHitPoints);
    }

    /// <summary>
    /// Checks if the current hit points are depleted
    /// </summary>
    public bool IsDead()
    {
        return currentHitPoints <= 0;
    }

    private void UpdateInvunerabilityCooldown()
    {
        if (invunerabilityCooldown > 0)
        {
            invunerabilityCooldown -= Time.deltaTime;
            invunerabilityCooldown = Mathf.Max(invunerabilityCooldown, 0);
        }
    }
}
