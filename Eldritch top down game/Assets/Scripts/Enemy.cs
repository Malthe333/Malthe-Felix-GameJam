using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : Damageable
{
    public string playerTag = "Player";
    public float movementSpeed;
    public bool canMove = true;
    
    protected bool isMoving;
    
    private Rigidbody2D rb;
    private float turnTime;
    private Transform playerTransform;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;
        
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
            Vector3 scale = transform.localScale;

            if (playerTransform.position.x < transform.position.x)
            {
                scale.x = -Mathf.Abs(scale.x);
            }
            else
            {
                scale.x = Mathf.Abs(scale.x);
            }
            
            transform.localScale = scale;
            
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
