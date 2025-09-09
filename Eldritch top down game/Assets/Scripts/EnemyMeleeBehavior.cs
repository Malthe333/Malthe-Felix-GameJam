using UnityEngine;

public class EnemyMeleeBehavior : MonoBehaviour
{
    
    private Animator animator;
    private BoxCollider2D enemy_collider;


   [SerializeField] bool attacking = false;

    public int attackDamage = 10;
    public int health = 30;

    public Transform playertransform;

    void Start()
    {
        enemy_collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Attack when player enters trigger
    void OnTriggerEnter2D(Collider2D other)
{
    int layer = other.gameObject.layer;

    if (layer == LayerMask.NameToLayer("SwordHitbox"))
    {
        health -= 10;
        animator.SetTrigger("IsHit");
        Debug.Log("Enemy Health: " + health);
        if (health <= 0)
        {
            animator.SetTrigger("Death");
            Destroy(gameObject, 1f);
        }
    }
    else if (layer == LayerMask.NameToLayer("Player"))
    {
        animator.SetTrigger("IsInAttackRange");
    }
}

    // Update is called once per frame
    void Update()
    {

        if (!attacking)
        {
            Vector2 direction = (playertransform.position - transform.position).normalized;
            transform.position += (Vector3)direction * Time.deltaTime;
        }

        if (transform.position.x < playertransform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
           
        }
        else if (transform.position.x > playertransform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
