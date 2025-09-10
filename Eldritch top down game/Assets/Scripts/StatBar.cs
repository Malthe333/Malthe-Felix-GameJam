using UnityEngine;

public class StatBar : MonoBehaviour
{
    public bool jitter = false;
    public float jitterThreshold = 0.2f;
    public float statValue = 50;
    public float statMax = 100;
    // Needed to calculate how much to offset the end of the bar by
    public float pixelPerUnit = 20;

    public Transform bar;
    public Transform heart;
    public Transform bg;
    public Transform endPiece;

    /*
    private SpriteRenderer heartRenderer;
    
    void Start()
    {
        heartRenderer = heart.GetComponent<SpriteRenderer>();
        
        if (heartRenderer == null)
        {
            Debug.LogWarning("No heart sprite renderer", gameObject);
        }
    }
    */
    
    void Update()
    {
        bar.localScale = new Vector3(statValue, 1f, 1f);
        bg.localScale = new Vector3(statMax, 1f, 1f);
        endPiece.localPosition = new Vector3(statMax/pixelPerUnit, 0, 0);
        
        /*
        Color heartColor = Color.HSVToRGB(0, 0, statValue / statMax);
        heartRenderer.color = heartColor;
        */
        
        if (jitter && statValue/statMax < jitterThreshold)
        {
            heart.localPosition = new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), heart.position.z);
        }
        else
        {
            heart.localPosition = new Vector3(0f, 0f, heart.position.z);
        }
    }

    public void SetStatMax(float newMax, bool keepRelativeFullness = false)
    {
        if (keepRelativeFullness)
        {
            float fullness = statValue / statMax;
            statValue = fullness * newMax;
        }
        
        statMax = newMax;
        if (statValue > statMax)
        {
            statValue = statMax;
        }
    }

    public void SetStatValue(float newValue)
    {
        statValue = Mathf.Clamp(newValue, 0, statMax);
    }
}
