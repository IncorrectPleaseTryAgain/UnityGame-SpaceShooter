using UnityEngine;

public class HealthLogic : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private bool isAlive;
    [SerializeField] private HealthBarLogic healthBar;
    [SerializeField] private IHealthResponder responder;

    private void Awake()
    {
        currentHealth = maxHealth;
        isAlive = true;
        if (healthBar) { healthBar.SetMaxHealth(maxHealth); }
        responder = GetComponent<IHealthResponder>();
    }

    public void ApplyDamage(float damage)
    {
        if (isAlive) 
        { 
            currentHealth -= damage;
            if (healthBar) { healthBar.SetHealth(currentHealth); }

            isAlive = currentHealth > 0;
            if (!isAlive) { responder.DeathHandler(); }
        }
    }

    public void ApplyHealth(float amount)
    {
        if (isAlive)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth) { currentHealth = maxHealth; }
            if (healthBar) { healthBar.SetHealth(currentHealth); }
        }
    }

    public bool IsAlive() { return isAlive; }
    public float GetCurrentHealth() { return currentHealth; }
    public float GetMaxHealth() { return maxHealth; }

    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        if (currentHealth > maxHealth) { currentHealth = maxHealth; }
        if (healthBar) { healthBar.SetMaxHealth(maxHealth); }
    }
}
