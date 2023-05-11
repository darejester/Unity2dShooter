using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float m_moveSpeed = 5f;
    public float m_jumpForce = 20f;
    public float m_groundCheckRadius = 1.02f;
    public LayerMask m_whatIsGround;
    public LayerMask m_whatIsJumpable;
    public int m_health = 100;
    public int m_maxHealth = 100;
    public float m_respawnTime = 3.0f;
    public int m_deathCount = 0;

    private Vector3 m_initialPosition;
    private Rigidbody2D m_rb;
    private bool m_canJump;
    private static PlayerController m_instance;
    private bool m_isDead = false;




    /**/
    /*
    PlayerController::Start()

    NAME

            Start - initializes m_rb with the RigidBody2d component

    SYNOPSIS

            void PlayerController::Start();



    DESCRIPTION

            This function is called at the object's creation. It initializes 
            m_rb with the RigidBody2d component, and m_initialPosition with
            the current position of the player object

    RETURNS

            Return value is void

    */
    /**/
    void
    Start()
    {
        //get this object's rigidbody component
        m_rb = GetComponent<Rigidbody2D>();
        m_initialPosition = transform.position;

    }
    /*void PlayerController::Start();*/



    /**/
    /*
    PlayerController::Update()

    NAME

            Update - Manages player controls

    SYNOPSIS

            void PlayerController::Update();



    DESCRIPTION

            This function is called once every frame. It manages player
            controls. It checks the direction the player needs to go
            depending on the value of m_horizontal. It then calculates 
            the velocity to move the player. Additionally, this also
            updates the player's initial position everytime it touches
            the ground/terrain. It also checks whether the player wants 
            to jump by checking the input key. All of these are only done
            when the player is alive, which is represented by the value of
            m_isDead.

    RETURNS

            Return value is void

    */
    /**/
    void
    Update()
    {
        if (!m_isDead)
        {
            // Check if the player is grounded
            m_canJump = Physics2D.OverlapCircle(transform.position, m_groundCheckRadius, m_whatIsJumpable);

            // Get horizontal direction ((A)left or (D)right)
            float m_horizontal = Input.GetAxis("Horizontal");

            // Set velocity / move player
            m_rb.velocity = new Vector2(m_horizontal * m_moveSpeed, m_rb.velocity.y);

            if(Physics2D.OverlapCircle(transform.position, m_groundCheckRadius, m_whatIsGround))
            {
                // update initial position with the current position of the player
                m_initialPosition = transform.position;
            }
            else
            {
                Debug.Log("player not colliding with ground");
            }
            

            // check if grounded and for input key
            if (Input.GetButtonDown("Jump") && m_canJump)
            {
                Debug.Log(m_canJump);
                //Set jump velocity to jump
                m_rb.velocity = new Vector2(m_rb.velocity.x, m_jumpForce);
            }
            else
            {
                Debug.Log("Player cannot jump");
            }
        }
        else
        {
            Debug.Log("Player is dead");
        }
    }
    /*void PlayerController::Update();*/




    /**/
    /*
    PlayerController::OnTriggerEnter2D()

    NAME

            OnTriggerEnter2D - checks if the player fell out of map bounds

    SYNOPSIS

            void PlayerController::OnTriggerEnter2D(Collider2D a_collision);
            a_collision     --> the Collider2D component of the object this collided with


    DESCRIPTION

            This function checks if the player fell out of map bounds. When the player
            collides with an object with the MapBounds tag, it will be disabled, death 
            counter will be incremented and the respawn routine will start.

    RETURNS

            Return value is void

    */
    /**/
    void
    OnCollisionEnter2D(Collision2D a_collision)
    {
        if (a_collision.gameObject.CompareTag("MapBounds"))
        {
            m_deathCount++;
            gameObject.SetActive(false);
            Invoke(nameof(Respawn), m_respawnTime);
        }
        else
        {
            Debug.Log("Player collided with something that does not require a response");
        }
    }
    /*void PlayerController::OnTriggerEnter2D(Collider2D a_collision);*/




    /**/
    /*
    PlayerController::OnDrawGizmosSelected()

    NAME

            OnDrawGizmosSelected - adds "gizmo" interface for testing

    SYNOPSIS

            void PlayerController::TakeDOnDrawGizmosSelectedamage();


    DESCRIPTION

            This function creates a custom interface for when Gizmos are
            enambled. This helps with debugging whether or not the player
            is actually grounded or not, which is needed for the jump
            mechanic

    RETURNS

            Return value is void

    */
    /**/
    void
    OnDrawGizmosSelected()
    {
        // Draw ground check circle in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_groundCheckRadius);
    }
    /*void PlayerController::TakeDOnDrawGizmosSelectedamage();*/



    /**/
    /*
    PlayerController::Respawn()

    NAME

            Respawn - enables/revives the player object

    SYNOPSIS

            void PlayerController::Respawn();



    DESCRIPTION

            This function enables/revives the player object. When called, this function
            sets the health of the player object back to full and sets it active again.
            

    RETURNS

            Return value is void

    */
    /**/
    void
    Respawn()
    {
        Debug.Log("Respawn called");


        m_isDead = false;

        // enable the enemy object and reset its health
        m_health = m_maxHealth;
        gameObject.SetActive(true);

        // reset the enemy's position to its original position
        transform.position = m_initialPosition;
    }
    /*void PlayerController::Respawn();*/



    /**/
    /*
    PlayerController::TakeDamage()

    NAME

            TakeDamage - updates m_health

    SYNOPSIS

            void PlayerController::TakeDamage(int a_damage);
            a_damage     --> the amount of damage to be deducted from m_health


    DESCRIPTION

            This function updates m_health. It proceeds with subtracting a_damage from m_health.
            If m_health is less than or equal to zero, this object gets disabled, sets respawn 
            location, and starts the Respawn routine.

    RETURNS

            Return value is void

    */
    /**/
    public void
    TakeDamage(int a_damage)
    {
        //update current health
        m_health -= a_damage;
        Debug.Log(m_health);

        //check current health
        if (m_health <= 0)
        {
            // Player is dead
            m_deathCount++;
            m_isDead = true;

            gameObject.SetActive(false);
            Invoke(nameof(Respawn), m_respawnTime);
        }
        else
        {
            Debug.Log("Player is not dead");
        }
    }
    /*void PlayerController::TakeDamage(int a_damage);*/




    /**/
    /*
    PlayerController::GetDeathCount()

    NAME

            GetDeathCount - updates m_health

    SYNOPSIS

            int PlayerController::GetDeathCount();



    DESCRIPTION

            This function retrieves the value of m_deathCount. 

    RETURNS

            Return value is an int that represents the number of times the player died

    */
    /**/
    public int
    GetDeathCount()
    {
        return m_deathCount;
    }
    /*int PlayerController::GetDeathCount();*/




    /**/
    /*
    PlayerController::GetHealth()

    NAME

            GetHealth - gets m_health

    SYNOPSIS

            int PlayerController::GetHealth();



    DESCRIPTION

            This function retrieves the value of m_health. 

    RETURNS

            Return value is an int that represents the current health of the
            player

    */
    /**/
    public int
    GetHealth()
    {
        return m_health;
    }
    /*int PlayerController::GetHealth();*/

}


