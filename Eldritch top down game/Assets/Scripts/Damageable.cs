using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    public int health;
    public int maxHealth;

    void Awake()
    {
        if (gameObject.layer != LayerMask.NameToLayer("Damageable"))
        {
            Debug.LogError("GameObject with script that does inherits from damageable is not on damageable layer");
            enabled = false;
        }
    }
    
    protected abstract void OnDeath();

    public void SetHealth(int amount, bool force = false)
    {
        if (force)
        {
            health = amount;
        }
        else
        {
            health = Mathf.Clamp(health, 0, maxHealth);
        }
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath();
        }
    }
}
