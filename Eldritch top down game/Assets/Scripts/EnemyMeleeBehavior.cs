using UnityEngine;

public class EnemyMeleeBehavior : MonoBehaviour
{
    
    private Animator animator;
    private BoxCollider2D collider;
    public Transform playertransform;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Attack when player enters trigger
    void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
    {
        animator.SetTrigger("Attack");
    }
}

    // Update is called once per frame
    void Update()
    {


        Vector2 direction = (playertransform.position - transform.position).normalized;
        transform.position += (Vector3)direction * Time.deltaTime;

    }
}
