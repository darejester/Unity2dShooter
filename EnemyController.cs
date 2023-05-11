using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float m_speed = 2.0f;
    public int m_health = 10;
    public int m_damage = 1;
    public float m_respawnTime = 3.0f;
    public float m_followDistance = 10.0f;
    public float m_jumpForce = 20f;
    public GameObject m_bulletPrefab;
    public float m_bulletSpeed = 20f;
    public float m_shootDelay = 1.0f;

    private float m_shootTimer = 0f;
    private int m_maxHealth = 10;
    private bool m_isDead = false;
    private Transform m_player;
    private Vector3 m_initialPosition;




    /**/
    /*
    EnemyController::Start()

    NAME

            Start - initializes m_player

    SYNOPSIS

            void EnemyController::Start();


    DESCRIPTION

            This function is called at the object's creation. It initializes m_player
            with the Player-game-object's transform, and the m_initialPosition with
            the position of the object at the start of the scene


    RETURNS

            Return value is void

    */
    /**/
    void
    Start()
    {
        //initialize player reference
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        m_initialPosition = transform.position;
    }
    /*void EnemyController::Start();*/




    /**/
    /*
    EnemyController::Update()

    NAME

            Update - moves this object towards the player

    SYNOPSIS

            void EnemyController::Update();


    DESCRIPTION

            This function is called once per frame. It calculates the direction of the player
            from this object, normalizes the vector, and moves the object towards the player
            at a certain speed. All of these calculations are to be done only when: m_isDead
            has a value of 0 or false, which means the enemy object is still alive, if
            the player is close enough to the enemy object, and if the player is not disabled. 
            The distance at which the enemy will follow the player is determined by 
            m_followDistance.

            In addition to moveing towards the player, the enemy object also shoots at the player.
            It instantiates a bullet prefab and adds velocity to the bullet so it will fly towards 
            the player.




    RETURNS

            Return value is void

    */
    /**/
    void
    Update()
    {
        if (!m_isDead && m_player.gameObject.activeSelf && (Vector2.Distance(transform.position, m_player.position) < m_followDistance))
        {
            //get direction of player from this object
            Vector3 m_direction = m_player.position - transform.position;
            //normalize vector
            m_direction.Normalize();

            //moves this object towards target/player
            transform.position += m_direction * m_speed * Time.deltaTime;

            m_shootTimer += Time.deltaTime;


            if (m_shootTimer >= m_shootDelay) // shoot every 1 second
            {
                m_shootTimer = 0f; // reset timer

                // Calculate the spawn position of the bullet
                Vector3 m_bulletSpawnPosition = transform.position + m_direction * 1;

                //create copy of bullet object
                GameObject m_bullet = Instantiate(m_bulletPrefab, m_bulletSpawnPosition, Quaternion.identity);
                //get rigidbody component of bullet object
                Rigidbody2D m_bulletRb = m_bullet.GetComponent<Rigidbody2D>();

                //fire bullet at direction with set speed
                m_bulletRb.velocity = m_direction * m_bulletSpeed;
            }
            else
            {
                Debug.Log("Shooting is on cooldown");
            }

        }
        else
        {
            Debug.Log("enemy does not see the player");
        }
    }
    /*void EnemyController::Update();*/




    /**/
    /*
    EnemyController::OnTriggerEnter2D()

    NAME

            OnTriggerEnter2D - checks what this object collided with, and "damages" player

    SYNOPSIS

            void EnemyController::OnTriggerEnter2D(Collider2D a_collision);
            a_collision     --> the Collider2D component of the object this collided with


    DESCRIPTION

            This function checks what  the enemy object it collided with. It starts 
            off by checking the Tag of a_collision. If the tag is the player's (Player), the
            the function proceeds with getting the a_collision's PlayerController component.
            It then calls the TakeDamage function of m_playerController and "damages" the 
            player. If the tag is MapBounds, it will disable the enemy object and starts
            the Respawn routine.

    RETURNS

            Return value is void

    */
    /**/
    void
    OnTriggerEnter2D(Collider2D a_collision)
    {
        //check collision
        if (a_collision.CompareTag("Player"))
        {
            //get player controller object
            PlayerController m_playerController = a_collision.GetComponent<PlayerController>();
            
            //check player controller object
            if (m_playerController != null)
            {
                //damage player
                m_playerController.TakeDamage(m_damage);
            }
            else
            {
                Debug.Log("Player does not have a PlayerController component");
            }
            
        }
        else if (a_collision.gameObject.CompareTag("MapBounds"))
        {
            gameObject.SetActive(false);
            Invoke(nameof(Respawn), m_respawnTime);
        }
        else
        {
            Debug.Log("collided with something that does not require a response");
        }
    }
    /*void EnemyController::OnTriggerEnter2D(Collider2D a_collision);*/




    /**/
    /*
    EnemyController::TakeDamage()

    NAME

            TakeDamage - updates m_health

    SYNOPSIS

            void EnemyController::TakeDamage(int a_damage);
            a_damage     --> the amount of damage to be deducted from m_health


    DESCRIPTION

            This function updates m_health. It proceeds with subtracting a_damage from m_health.
            If m_health is less than or equal to zero, this object gets disabled and invokes the
            Respawn function after the respawn Timer.

    RETURNS

            Return value is void

    */
    /**/
    public void
    TakeDamage(int a_damage)
    {
        //update health
        m_health -= a_damage;
        Debug.Log(m_health);

        //check health
        if (m_health <= 0 && !m_isDead)
        {
            m_isDead = true;

            gameObject.SetActive(false);
            Invoke(nameof(Respawn), m_respawnTime);

        }
        else
        {
            Debug.Log("enemy is not dead");
        }
    }
    /*void EnemyController::TakeDamage(int a_damage);*/



    /**/
    /*
    EnemyController::Respawn()

    NAME

            Respawn - enables/revives the enemey object

    SYNOPSIS

            void EnemyController::Respawn();



    DESCRIPTION

            This function enables/revives the enemey object. When called, this function
            sets the health of the enemy object back to full and sets it active again.
            This function also resets the position of the enemy object back to its
            original position.

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
    /* void EnemyController::Respawn();*/

}
