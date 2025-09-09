using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageDealer : MonoBehaviour
{
    public int damage;

    private LayerMask damageableLayer;

    void Awake()
    {
        damageableLayer = LayerMask.NameToLayer("Damageable");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == damageableLayer)
        {
            other.SendMessage("TakeDamage", damage);
        }
    }
}
