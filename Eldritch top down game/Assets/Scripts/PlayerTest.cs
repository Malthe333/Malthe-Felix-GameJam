using UnityEngine;

public class PlayerTest : Damageable
{
    protected override void OnDeath()
    {
        Invoke(nameof(Disable), .5f);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
