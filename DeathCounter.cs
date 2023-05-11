using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathCounter : MonoBehaviour
{
    private TextMeshProUGUI m_deathText;
    private GameObject m_playerObject;



    /**/
    /*
    DeathCounter::Start();

    NAME

            Start - initializes m_playerObject and m_deathText

    SYNOPSIS

            void DeathCounter::Start(); 


    DESCRIPTION

            This function is called at the object's creation to initialize some of its
            variables. m_playerObject is initializes with a reference to the Player 
            object, and m_deathText with the TextMeshProUGUI (text UI) component of
            this object.
  


    RETURNS

            Return value is void

    */
    /**/
    void Start()
    {
        m_playerObject = GameObject.Find("Player");
        m_deathText = GetComponent<TextMeshProUGUI>();
    }
    /*void DeathCounter::Start(); */




    /**/
    /*
    DeathCounter::Update();

    NAME

            Update - updates the text UI

    SYNOPSIS

            void DeathCounter::Update(); 


    DESCRIPTION

            This function is called every frame once. It updates the text UI
            to display the death count of the player. It gets the number by 
            calling the GetDeathCount() function of the player game object.
  


    RETURNS

            Return value is void

    */
    /**/
    void Update()
    {

        m_deathText.text = "Deaths: " + m_playerObject.GetComponent<PlayerController>().GetDeathCount(); ;
        
        
    }
    /*void DeathCounter::Update(); */
}
