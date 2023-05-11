using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float m_speed = 2.0f;
    public int m_health = 500;
    public int m_damage = 1;
    public float m_followDistance = 100.0f;
    public GameObject m_bulletPrefab;
    public GameObject m_bullet2Prefab;
    public float m_bulletSpeed = 20f;
    public float m_shootDelay = 1.5f;

    private GameObject m_bulletType;
    private float m_shootTimer = 0f;
    private bool m_isDead = false;
    private Transform m_player;
    private Vector3 m_initialPosition;




    /**/
    /*
    BossController::Start()

    NAME

            Start - initializes m_player

    SYNOPSIS

            void BossController::Start();


    DESCRIPTION

            This function is called at the object's creation. It initializes m_player
            with the Player-game-object's transform, the m_initialPosition with
            the position of the object at the start of the scene, and m_bulletType
            with the first bullet prefab.


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
        m_bulletType = m_bulletPrefab;
    }
    /*void BossController::Start();*/




    /**/
    /*
    BossController::Update()

    NAME

            Update - moves this object towards the player

    SYNOPSIS

            void BossController::Update();


    DESCRIPTION

            This function is called once per frame. It calculates the direction of the player
            from this object, normalizes the vector, and moves the object towards the player
            at a certain speed. All of these calculations are to be done only when: m_isDead
            has a value of 0 or false, which means the enemy object is still alive, if
            the player is close enough to the enemy object, and if the player is not disabled. 
            The distance at which the enemy will follow the player is determined by 
            m_followDistance.

            In addition to moving towards the player, the enemy object also shoots at the player.
            It instantiates a whatever object the m_bulletType has and adds velocity to it so it 
            will fly towards the player. The m_shootDelay, m_bulletType, and the visibility of the 
            Boss change depending on the health (m_health) of the Boss. This will simulate the
            different stages of the Boss fight.




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

            if(m_health == 400)
            {
                
                transform.position = m_initialPosition;
                m_shootDelay = 0.5f;
                
            }
            else if (m_health == 300)
            {
                transform.position = m_initialPosition;
                m_bulletType = m_bullet2Prefab;
            }
            else if(m_health == 200)
            {
                transform.position = m_initialPosition;
                gameObject.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                Debug.Log("No changes to Boss");
            }


            //counter for shooting (counts seconds/frames)
            m_shootTimer += Time.deltaTime;

            if (m_shootTimer >= m_shootDelay) // shoot every 1 second
            {
                m_shootTimer = 0f; // reset timer

                //bullet instantiation position
                Vector3 m_bulletSpawnPosition = transform.position + m_direction * 1;

                //create copy of bullet object
                GameObject m_bullet = Instantiate(m_bulletType, m_bulletSpawnPosition, Quaternion.identity);

                //get rigidbody component of bullet object
                Rigidbody2D m_bulletRb = m_bullet.GetComponent<Rigidbody2D>();

                //fire bullet at direction with set speed
                m_bulletRb.velocity = m_direction * m_bulletSpeed;
            }

        }
    }
    /*void BossController::Update();*/




    /**/
    /*
    BossController::OnTriggerEnter2D()

    NAME

            OnTriggerEnter2D - checks what this object collided with, and "damages" player

    SYNOPSIS

            void BossController::OnTriggerEnter2D(Collider2D a_collision);
            a_collision     --> the Collider2D component of the object this collided with


    DESCRIPTION

            This function checks what  the Boss object it collided with. It starts 
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
            
        }
        else if (a_collision.gameObject.CompareTag("MapBounds"))
        {
            gameObject.SetActive(false);
            //Invoke(nameof(Respawn), m_respawnTime);
        }
        else
        {
            Debug.Log("Collided with somehting that does not need a response");
        }
    }
    /*void BossController::OnTriggerEnter2D(Collider2D a_collision);*/




    /**/
    /*
    BossController::TakeDamage()

    NAME

            TakeDamage - updates m_health

    SYNOPSIS

            void BossController::TakeDamage(int a_damage);
            a_damage     --> the amount of damage to be deducted from m_health


    DESCRIPTION

            This function updates m_health. It proceeds with subtracting a_damage from m_health.
            If m_health is less than or equal to zero, this object gets disabled and clears out
            all enemies in the scene.

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
            //Invoke(nameof(Respawn), m_respawnTime);
            GameObject[] m_minions = GameObject.FindGameObjectsWithTag("enemy");
            foreach (GameObject m_minion in m_minions)
            {
                Destroy(m_minion);
            }

        }
        else
        {
            Debug.Log("Boss is not dead");
        }
    }
    /*void BossController::TakeDamage(int a_damage);*/




}
