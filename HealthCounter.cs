using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthCounter : MonoBehaviour
{
    private TextMeshProUGUI m_healthText;
    private GameObject m_playerObject;



    /**/
    /*
    HealthCounter::Start();

    NAME

            Start - initializes m_playerObject and m_healthText

    SYNOPSIS

            void HealthCounter::Start(); 


    DESCRIPTION

            This function is called at the object's creation to initialize some of its
            variables. m_playerObject is initializes with a reference to the Player 
            object, and m_healthText with the TextMeshProUGUI (text UI) component of
            this object.
  


    RETURNS

            Return value is void

    */
    /**/
    void Start()
    {
        m_playerObject = GameObject.Find("Player");
        m_healthText = GetComponent<TextMeshProUGUI>();
    }
    /*void HealthCounter::Start(); */




    /**/
    /*
    HealthCounter::Update();

    NAME

            Update - updates the text UI

    SYNOPSIS

            void HealthCounter::Update(); 


    DESCRIPTION

            This function is called every frame once. It updates the text UI
            to display the Health count of the player. It gets the number by 
            calling the GetHealth() function of the player game object.
  


    RETURNS

            Return value is void

    */
    /**/
    void Update()
    {

        m_healthText.text = "Health: " + m_playerObject.GetComponent<PlayerController>().GetHealth(); ;


    }
    /*void HealthCounter::Update(); */
}
