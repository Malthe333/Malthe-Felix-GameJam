using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Altar : MonoBehaviour
{
    public KeyCode activationKey = KeyCode.P;
    public float activationTime = 3f;
    public bool isReusable = false;
    public UnityEvent OnActivate;
    public Color activeColor = Color.red;
    public Color inactiveColor = Color.darkRed;

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

                if (isReusable)
                {
                    keyDownTime = Time.time;
                }
                else
                {
                    altarFunctioning = false;
                }
            }
        }

        if (altarFunctioning)
        {
            spriteRenderer.color = activeColor;
        }
        else
        {
            spriteRenderer.color = inactiveColor;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnAltar = true;
        }

        if (Input.GetKey(activationKey))
        {
            keyDownTime = Time.time;
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
