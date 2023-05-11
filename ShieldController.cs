using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{

    private Transform m_shield;
    private Vector3 m_originalScale;
    private GameObject m_playerReference;



    /**/
    /*
    ShieldController::Start()

    NAME

            Start - initializes variables for use.

    SYNOPSIS

            void ShieldController::Start();



    DESCRIPTION

            This function is called at the object's creation. This
            initializes all of the variables ready for use: m_playerReference
            with the Player object, m_shield with the dot object's transform,
            and m_originalScale with the dot object's original scale.

    RETURNS

            Return value is void

    */
    /**/
    void Start()
    {
        m_playerReference = GameObject.FindGameObjectWithTag("Player");
        m_shield = m_playerReference.transform.Find("Pivot/Dot").transform;
        m_originalScale = m_shield.localScale;
    }
    /*void ShieldController::Start();*/



    /**/
    /*
    ShieldController::Update()

    NAME

            Update - manages the shield mechanic

    SYNOPSIS

            void ShieldController::Update();



    DESCRIPTION

            This function is called once every frame. It first
            checks whether the left mouse button is pressed or not.
            If it is pressed, it will change the scale of the shield.
            Then if the player lets go of the button, the shield will
            revert back to its original scale.

    RETURNS

            Return value is void

    */
    /**/
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 m_newScale = m_shield.localScale;
            m_newScale.y = 1f;
            m_shield.localScale = m_newScale;

        }
        else if (Input.GetMouseButtonUp(1))
        {
            m_shield.localScale = m_originalScale;

        }
        else
        {
            Debug.Log("no inputs for shield use");
        }


    }
    /*void ShieldController::Update();*/
}
