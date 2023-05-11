using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public bool[] m_isFilled;
    public GameObject[] m_slots;
    public Canvas m_inventoryCanvas;



    /**/
    /*
    InventoryController::Start()

    NAME

            Start - initializes m_inventory Canvas as invisible


    SYNOPSIS

            void InventoryController::Start();


    DESCRIPTION

            This function initializes m_inventory Canvas as invisible. It does
            this by calling the SetActive function and passing false to it.

    RETURNS

            Return value is void

    */
    /**/
    void
    Start()
    {
        //initializes the visibility of the canvas to false(invisible)
        Debug.Log("inactive");
        m_inventoryCanvas.gameObject.SetActive(false);

        //initialize inventory slots with a default sprite
    }
    /*void InventoryController::Start();*/



    /**/
    /*
    InventoryController::Update()

    NAME

            Update - changes the visibility of the inventory canvas


    SYNOPSIS

            void InventoryController::Update();


    DESCRIPTION

            This function is called once every frame changes the visibility of the 
            inventory canvas. It starts by checking the input key. If it is "I" 
            (player pressed "I"), it will toggle the visibility of the canvas to 
            the opposite (visible if it was invisible, and vice versa). It does 
            this with the help of the SetActive function.

    RETURNS

            Return value is void

    */
    /**/
    void
    Update()
    {

        //checks input key
        if (Input.GetKeyDown(KeyCode.I))
        {
            //gets the boolean value whether the canvas is visible or not
            bool m_currentState = m_inventoryCanvas.gameObject.activeSelf;

            //sets canvas' visibilty
            m_inventoryCanvas.gameObject.SetActive(!m_currentState);
        }
        else
        {
            Debug.Log("no inputs for inventory use");
        }
    }
    /*void InventoryController::Update();*/




    /**/
    /*
    InventoryController::GetFilled()

    NAME

            GetFilled - gets the number of filled inventory slots


    SYNOPSIS

            void InventoryController::GetFilled();


    DESCRIPTION

            This function is gets the numberof filled inventory
            slots and returns it.

            The number of filled inventory slots are retrieved 
            by iterating through each element of m_isFilled and
            checking if it is true or not (filled or not).

    RETURNS

            Return value is void

    */
    /**/
    public int
    GetFilled()
    {
        int m_counter = 0;

        //count each element in the m_isFilled that is filled
        foreach (bool m_element in m_isFilled)
        {
            if(m_element == true)
            {
                m_counter++;
            }
            else
            {
                Debug.Log("slot in m_isFilled is not filled");
            }
            
        }

        return m_counter;
    }
    /*void InventoryController::GetFilled();*/

}


