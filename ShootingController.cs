using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject m_bulletPrefab;
    public Transform m_dot;
    public float m_bulletSpeed = 10f;
    public float m_shootDelay = 1.0f;

    private bool m_isHolding = false;
    private float m_shootTimer = 0f;



    /**/
    /*
    ShootingController::Update()

    NAME

            Update - manages the shooting mechanic

    SYNOPSIS

            void ShootingController::Update();



    DESCRIPTION

            This function is called once every frame. This is responsible for
            managing the shooting mechanic. It first checks for an input key.
            If the correct input is entered, start the shooting algorithm.
            The function gets the mouse location, calculates the direction that the
            bullet needs to take, and instantiates a bullet object. Lastly, it adds
            velocity to it, which specifies the speed of the bullet.

            Depending on whether the player is holding down the left mouse button or
            just tapping it, the shooting will either use a timer for the shots or 
            just shoot everytime the player taps the mouse button.

    RETURNS

            Return value is void

    */
    /**/
    void
    Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_isHolding = true;
            m_shootTimer = m_shootDelay; // shoot immediately on first click
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_isHolding = false;
        }

        if (m_isHolding && !Input.GetMouseButton(1))
        {
            m_shootTimer += Time.deltaTime;
            // shoot based on a delay
            if (m_shootTimer >= m_shootDelay) 
            {

                //check for upgades
                ShootingUpgrade();

                //reset m_shootTimer
                m_shootTimer = 0f;

                //gets mouse location
                Vector3 m_mousePosition = Input.mousePosition;
                //gets camera location
                Vector3 m_cameraPos = Camera.main.transform.position;

                //set proper mouse position's z-coordinate 
                m_mousePosition.z = m_dot.position.z - Camera.main.transform.position.z;

                //get mouse's location from screen coordinates to world coordinates
                Vector3 m_worldMousePosition = Camera.main.ScreenToWorldPoint(m_mousePosition);

                //sets the direction for shooting
                Vector3 m_direction = m_worldMousePosition - m_dot.position;
                //set z-direction to zero
                m_direction.z = 0f;
                //normalizes direction vector
                m_direction.Normalize();

                //create copy of bullet object
                GameObject m_bullet = Instantiate(m_bulletPrefab, m_dot.position, Quaternion.identity);
                //get rigidbody component of bullet object
                Rigidbody2D m_bulletRb = m_bullet.GetComponent<Rigidbody2D>();

                //fire bullet at direction with set speed
                m_bulletRb.velocity = m_direction * m_bulletSpeed;
            }
        }
    }
    /*void ShootingController::Update();*/



    /**/
    /*
    ShootingController::ShootingUpgrade()

    NAME

            ShootingUpgrade - manages the upgrades with the weapon

    SYNOPSIS

            void ShootingController::ShootingUpgrade();



    DESCRIPTION

            This function manages the upgrades with the weapon.
            Depending on the number of items in the player's inventory,
            the Player's weapon will act differently.

            The number of items is retrieved by calling the GetFilled()
            method of the InventoryController Component of the Player.

    RETURNS

            Return value is void

    */
    /**/
    void
    ShootingUpgrade()
    {
        int m_items = gameObject.GetComponent<InventoryController>().GetFilled();

        if(m_items == 1)
        {
            m_shootDelay = 0.2f;
        }
        else if(m_items == 2)
        {
            m_shootDelay = 0.1f;
        }
        else if(m_items == 3)
        {
            m_shootDelay = 0.05f;
        }   
        else
        {
            Debug.Log("No shooting upgrades needed at this time");
        }
    }
    /*void ShootingController::ShootingUpgrade();*/

}
