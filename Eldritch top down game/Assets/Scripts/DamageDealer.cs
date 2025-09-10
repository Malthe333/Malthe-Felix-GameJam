using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageDealer : MonoBehaviour
{
    public int damage;

    public int enemyDamage;

    public bool isEnemy = true;

    private LayerMask damageableLayer;

    void Awake()
    {
        damageableLayer = LayerMask.NameToLayer("Damageable");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == damageableLayer && !isEnemy)
        {
            other.SendMessage("TakeDamage", damage); // Activate when player hits skeleton
        }
        else if (other.gameObject.tag == "Player")
{
            other.SendMessage("TakeDamagePlayer", enemyDamage); // Activate when enemy hits player
        }
    }
}
