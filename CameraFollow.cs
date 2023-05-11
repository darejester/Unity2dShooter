using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform m_target;
    public float m_smoothing = 5f;
    public Vector3 m_offset = new Vector3(0f, 0f, -20f);

    private Vector3 m_targetCamPos;






    /**/
    /*
    CameraFollow::FixedUpdate();

    NAME

            FixedUpdate - updates camera's position to follow player

    SYNOPSIS

            void CameraFollow::FixedUpdate(); 


    DESCRIPTION

            This function updates the position of the camera to follow the target object smoothly.
            The target object is specified by m_traget. It starts of by calculating the target 
            position for the camera by adding the specified offset to the target's current position. 
            It then moves the camera towards the target position smoothly from the camera's current 
            position. 
    
            The speed at which the camera moves is controlled by m_smoothing.


    RETURNS

            Return value is void

    */
    /**/
    void
    FixedUpdate()
    {
        //sets a destination for the camera
        m_targetCamPos = m_target.position + m_offset;

        //moves camera to destination smoothly
        transform.position = Vector3.Lerp(transform.position, m_targetCamPos, m_smoothing * Time.deltaTime);
    }
    /* void CameraFollow::FixedUpdate();*/
}

