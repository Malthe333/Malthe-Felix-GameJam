using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.Events;

public class HeroKnight : MonoBehaviour
{

    [SerializeField] float m_speed = 4.0f;

    [SerializeField] float m_rollForce = 6.0f;

    [SerializeField] GameObject m_slideDust;

    public UnityEvent OnDeath;

    private Animator m_animator;

    public bool isFlipped;

    private Rigidbody2D m_body2d;

    public UnityEvent OnDarkBolt;

    public int health = 100;

    private bool cancastDarkBolt = true;

    public int corruption = 0;

    public bool isdead = false;

    private Sensor_HeroKnight m_groundSensor;

    /*
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight   m_wallSensorR2;
    private Sensor_HeroKnight   m_wallSensorL1;
    private Sensor_HeroKnight   m_wallSensorL2;
    */

    private bool m_blocking = false;
    private bool m_rolling = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    private float m_rollDuration = 8.0f / 14.0f;
    private float m_rollCurrentTime;


    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        /*
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
        */

    }

    // Update is called once per frame
    void Update()
    {
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;



        // Increase timer that checks roll duration
        if (m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if (m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        /*
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }
        */

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        if (!m_blocking && !isdead)
        {
            m_body2d.linearVelocity = new Vector2(inputX * m_speed, inputY * m_speed);
        }
        else if (m_blocking)
        {
            m_body2d.linearVelocity = new Vector2(0, 0);
        }


        // Swap direction of sprite depending on walk direction
        if (inputX > 0 && !isdead)
        {
            GetComponent<SpriteRenderer>().flipX = false;
           
            m_facingDirection = 1;
        }

        else if (inputX < 0 && !isdead)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            
            m_facingDirection = -1;
        }




        //Attack
        if (Input.GetKeyDown("e") && m_timeSinceAttack > 0.25f && !m_rolling && !isdead)
        {
            m_currentAttack++;
            Debug.Log("Hurt");

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }

        // Block
        if (Input.GetMouseButtonDown(1) && !m_rolling && !isdead)
        {
            m_animator.SetTrigger("Block");
            m_blocking = true;
            m_animator.SetBool("IdleBlock", true);
        }

        else if (Input.GetMouseButtonUp(1) && !isdead)
        {
            m_animator.SetBool("IdleBlock", false);
            m_blocking = false;
        }

        // Roll
        if (Input.GetKeyDown("left shift") && !m_rolling && !isdead && corruption >= 1)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.linearVelocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.linearVelocity.y);
        }



        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon || Mathf.Abs(inputY) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }
        //Dark Bolt
        if (Input.GetKeyDown("f") && !m_rolling && !isdead && cancastDarkBolt && corruption >= 3)
        {
            m_animator.SetTrigger("DarkBolt");
            ///EffectsAnimator.SetTrigger("DarkBolt");
            cancastDarkBolt = false;
            Invoke("ResetDarkBolt", 3.0f);
            if (m_facingDirection > 0)
            {
                isFlipped = false;
            }
            else if (m_facingDirection < 0)
            {
                isFlipped = true;
            }
            OnDarkBolt.Invoke();
        }


    }

    private void ResetDarkBolt()
    {
        cancastDarkBolt = true;
    }

    public void TakeDamagePlayer(int enemyDamage)
    {
        health -= enemyDamage;
        if (health > 0 && !isdead)
        {
            m_animator.SetTrigger("Hurt");
        }
        else if (health <= 0 && !isdead)
        {
            m_animator.SetTrigger("Death");
            OnDeath.Invoke();
            isdead = true;

        }

    }

    public void IncreaseCorruption(int amount)
    {
        corruption += amount;
    }

    public void DecreaseCorruption(int amount)
    {
        corruption -= amount;
    }
    }
    

    

