using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // duration of the bullet staying in the scene
    public float m_lifetime = 2f;
    public int m_damage = 1;
    public string m_target;


    /**/
    /*
    BulletController::start()

    NAME

            Start - starts the timer for the lifetime of the object and destorys it

    SYNOPSIS

            void BulletController::Start();


    DESCRIPTION

            This function is called at the object's creation. The function 
            calls the Destroy function of the gameObject to destroy it after 
            its lifetime.

    RETURNS

            Return value is void

    */
    /**/
    void
    Start()
    {

        // Destroy this object after its lifetime
        Destroy(gameObject, m_lifetime);
    }
    /*void BulletController::start(); */





    /**/
    /*
    BulletController::OnTriggerEnter2D()

    NAME

            OnTriggerEnter2D - checks what this object collided with, and "damages" enemy
                               or player

    SYNOPSIS

            void BulletController::OnTriggerEnter2D(Collider2D a_collision);
            a_collision     --> the Collider2D component of the object this collided with


    DESCRIPTION

            This function inflicts damage to the game object it collided with. It starts 
            off by checking the target of the object. Depending on the target it recieved, 
            the function proceeds with getting the a_collision's Controller component.
            It then calls the TakeDamage function of the Controller and "damages" the 
            gameObject.

    RETURNS

            Return value is void

    */
    /**/
    void
    OnTriggerEnter2D(Collider2D a_collision)
    {
        //check collision
        if (m_target == "enemy")
        {
            if(a_collision.GetComponent<EnemyController>() != null)
            {
                //get enemy controller object
                EnemyController m_enemyController = a_collision.GetComponent<EnemyController>();

                //check enemy controller object
                if (m_enemyController != null)
                {
                    //damage enemy
                    m_enemyController.TakeDamage(m_damage);
                }
                else
                {
                    Debug.Log("does not have an EnemyController component");
                }
            }
            else if(a_collision.GetComponent<BossController>() != null)
            {
                //get boss controller object
                BossController m_bossController = a_collision.GetComponent<BossController>();

                //check enemy controller object
                if (m_bossController != null)
                {
                    //damage enemy
                    m_bossController.TakeDamage(m_damage);
                }
                else
                {
                    Debug.Log("does not have a BossController component");
                }
            }
            else
            {
                Debug.Log("enemy does not have a controller componenet");
            }
            

            
        }
        else if (m_target == "Player")
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
        else
        {
            Debug.Log("Collided with something that does not require a response");
        }

    }
    /*void BulletController::OnTriggerEnter2D(Collider2D a_collision); */




}
