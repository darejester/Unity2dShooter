using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class resourceCounter : MonoBehaviour
{
    private TextMeshProUGUI m_resourceText;
    private GameObject m_playerObject;


    /**/
    /*
    resourceCounter::Start();

    NAME

            Start - initializes m_playerObject and m_resourceText

    SYNOPSIS

            void resourceCounter::Start(); 


    DESCRIPTION

            This function is called at the object's creation to initialize some of its
            variables. m_playerObject is initializes with a reference to the Player 
            object, and m_resourceText with the TextMeshProUGUI (text UI) component of
            this object.
  


    RETURNS

            Return value is void

    */
    /**/
    void Start()
    {
        m_playerObject = GameObject.Find("Player");
        m_resourceText = GetComponent<TextMeshProUGUI>();
    }
    /*void resourceCounter::Start(); */




    /**/
    /*
    resourceCounter::Update();

    NAME

            Update - updates the text UI

    SYNOPSIS

            void resourceCounter::Update(); 


    DESCRIPTION

            This function is called every frame once. It updates the text UI
            to display the Resource count of the player. It gets the number by 
            calling the GetResource() function of the player game object.
  


    RETURNS

            Return value is void

    */
    /**/
    void Update()
    {

        m_resourceText.text = "Resource: " + m_playerObject.GetComponent<SkillController>().GetResource().ToString("0");


    }
    /*void resourceCounter::Update(); */
}
