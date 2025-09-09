using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HeroKnightColorChange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Corrupt()
    {
        spriteRenderer.color = Color.red;
    }

    public void Purify()
    {
        spriteRenderer.color = Color.white;
    }
}
