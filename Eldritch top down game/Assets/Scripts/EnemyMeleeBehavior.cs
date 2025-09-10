using UnityEngine;

public class EnemyMeleeBehavior : MonoBehaviour
{
    
    private Animator animator;
    private BoxCollider2D enemy_collider;

    public BoxCollider2D SkeletonboxCollider2D;

    public bool attackFatigue = false;

    private bool dead = false;


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

        if (layer == LayerMask.NameToLayer("Player") && !attackFatigue && !dead)
        {
            animator.SetTrigger("IsInAttackRange");
            attackFatigue = true;
            animator.SetBool("AttackFatigue", attackFatigue);
            Invoke("ResetAttackFatigue", 1f);
        }
    }


    void ResetAttackFatigue()
    {
        attackFatigue = false;
        animator.SetBool("AttackFatigue", attackFatigue);
    }
    void TakeDamage(int damage)
    {

        health -= damage;
        if (health > 0)
        {
            animator.SetTrigger("IsHit");
        }
        else if (health <= 0)
        {
            animator.SetTrigger("isDead");
            Destroy(gameObject, 1f);
            dead = true;
            SkeletonboxCollider2D.enabled = false;
        }


    }

    // Update is called once per frame
    void Update()
    {

        Vector3 scale = transform.localScale;   
        if (playertransform.position.x < transform.position.x && !dead)
        {
            scale.x = -2.42f;
        }
        else if (!dead)
        {
            scale.x = 2.42f;
        }
        transform.localScale = scale;

        if (!attacking && !dead)
        {
            Vector2 direction = (playertransform.position - transform.position).normalized;
            transform.position += (Vector3)direction * Time.deltaTime;
        }

        
        /*
        if (transform.position.x < playertransform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
           
        }
        else if (transform.position.x > playertransform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        */


    }
}
