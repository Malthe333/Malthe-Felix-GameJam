using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Altar : MonoBehaviour
{
    public KeyCode activationKey = KeyCode.P;
    public float activationTime = 3f;
    public bool isReusable = false;
    public UnityEvent OnActivate;

    private float keyDownTime;
    private bool playerOnAltar;
    private bool altarFunctioning = true;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        altarFunctioning = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        if (playerOnAltar)
        {
            if (Input.GetKeyDown(activationKey))
            {
                keyDownTime = Time.time;
            }

            if (Time.time - keyDownTime >= activationTime && Input.GetKey(activationKey) && altarFunctioning)
            {
                OnActivate?.Invoke();
                altarFunctioning = false;
            }
        }

        if (altarFunctioning)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = Color.gray;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnAltar = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnAltar = false;
        }

        if (isReusable)
        {
            altarFunctioning = true;
        }
    }
}
