using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : Damageable
{
    public Transform playerTransform;
    public float movementSpeed;
    public bool canMove = true;
    
    protected bool isMoving;
    
    private Rigidbody2D rb;
    private float turnTime;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (playerTransform == null)
        {
            Debug.LogWarning("No player transform assigned", this);
            enabled = false;
        }
    }

    protected abstract void Attack();
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Attack();
        }
    }

    protected void TrackPlayer()
    {
        if (canMove) {
            transform.localScale =
                playerTransform.position.x < transform.position.x ?
                new Vector3(-1, 1, 1) :
                new Vector3(1, 1, 1);

            Vector2 movementDirection = (playerTransform.position - transform.position).normalized;
            Vector2 velocity = movementDirection * movementSpeed;
            rb.linearVelocity = velocity;

            isMoving = true;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            
            isMoving = false;
        }
    }
}
