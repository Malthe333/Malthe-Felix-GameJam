using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public bool jitter = false;
    public float healthFullness;

    public Transform bar;
    public Transform heart;
    
    void Update()
    {
        bar.localScale = new Vector3(healthFullness, 1f, 1f);
        
        if (jitter)
        {
            heart.localPosition = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), heart.position.z);
        }
    }
}
