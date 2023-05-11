using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotController : MonoBehaviour
{
    public float m_rotationSpeed = 5f;

    void Start()
    {
        
    }


    /**/
    /*
    DotController::Update()

    NAME

            Update - rotates the Dot around the player towards the mouse location

    SYNOPSIS

            void DotController::Update();


    DESCRIPTION

            This function is called once per frame. It gets the mouse's position in screen coordinates
            using Ray and converts it to world coordinates. It then calculates the normalized direction 
            of the mouse relative to the position of this object and rotates the object to point at the 
            mouse. 


    RETURNS

            Return value is void

    */
    /**/
    void Update()
    {
        // Cast a ray from the camera to the mouse position
        Ray m_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Calculate the position where the ray intersects the XY plane
        float m_distance;
        Vector3 m_worldMousePosition = Vector3.zero;
        if (new Plane(Vector3.forward, transform.position).Raycast(m_ray, out m_distance))
        {
            m_worldMousePosition = m_ray.GetPoint(m_distance);
        }
        else
        {
            Debug.Log("Ray does not intersect the XY plane");
        }

        // Calculate the direction from its location to the mouse position
        Vector2 lookDirection = (m_worldMousePosition - transform.position).normalized;

        // Calculate the angle that the dot needs to rotate to face the mouse
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        // Set the rotation of the  dot
        Quaternion m_rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, m_rotation, Time.deltaTime * m_rotationSpeed);
    }

    /*void DotController::Update();*/
}

