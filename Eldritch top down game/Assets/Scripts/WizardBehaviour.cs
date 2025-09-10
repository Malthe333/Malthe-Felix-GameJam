using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Animator))]
public class WizardBehaviour : Enemy
{
    public float minTimeBetweenAttacks = 0.5f;
    public float afterAttackDelay = 2f;
    
    private Animator animator;
    private float lastAttackTime;

    // Precompute animation hashes
    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int Attack1 = Animator.StringToHash("Attack1");
    private static readonly int Attack2 = Animator.StringToHash("Attack2");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    
    new void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        TrackPlayer();

        if (isMoving)
        {
            animator.SetBool(IsRunning, true);
        }
        else
        {
            animator.SetBool(IsRunning, false);
        }
    }

    protected override void Attack()
    {
        if (Time.time - lastAttackTime < minTimeBetweenAttacks)
        {
            return;
        }

        lastAttackTime = Time.time;
        
        int trigger = Random.value > 0.5f ? Attack1 : Attack2;
        animator.SetTrigger(trigger);

        BlockMovement();
        CancelInvoke();
        Invoke(nameof(AllowMovement), afterAttackDelay);
    }

    protected override void OnDeath()
    {
        animator.SetTrigger(Death);
        Destroy(gameObject, 1.2f);
    }

    void BlockMovement()
    {
        canMove = false;
    }

    void AllowMovement()
    {
        canMove = true;
    }
}
