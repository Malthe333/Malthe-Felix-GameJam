using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Animator))]
public class WizardBehaviour : Enemy
{
    private Animator animator;

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
        
        AnimatorClipInfo animatorClipInfo = animator.GetCurrentAnimatorClipInfo(0)[0];
        string animationName = animatorClipInfo.clip.name;
    }

    protected override void Attack()
    {
        // Debug.Log("Attack Triggered");
        AnimatorClipInfo animationInfo = animator.GetCurrentAnimatorClipInfo(0)[0];
        string animationName = animationInfo.clip.name;

        if (animationName == "Attack1")
        {
            animator.SetTrigger(Attack2);
        } else if (animationName == "Attack2")
        {
            animator.SetTrigger(Attack1);
        }
        else
        {
            int trigger = Random.value > 0.5f ? Attack1 : Attack2;
            animator.SetTrigger(trigger);
        }
    }

    protected override void OnDeath()
    {
        animator.SetTrigger(Death);
        Destroy(gameObject, 1.2f);
    }
}
