using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpController : MonoBehaviour
{
    private InventoryController m_inventory;
    public GameObject m_item;



    /**/
    /*
    PickUpController::Start()

    NAME

            Start - initializes m_inventory with the InventoryController component of Player


    SYNOPSIS

            void PickUpController::Start();


    DESCRIPTION

            This function is called at the object's creation. It starts off by looking for the 
            Player Object. It then initializes m_inventory with the InventoryController component 
            of Player.

    RETURNS

            Return value is void

    */
    /**/
    void Start()
    {
        //initialize the inventory with the Player's
        m_inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryController>();
    }
    /*void PickUpController::Start();*/




    /**/
    /*
    PickUpController::OnTriggerStay2D()

    NAME

            OnTriggerStay2D - lets player pick up an item

    SYNOPSIS

            void PickUpController::OnTriggerStay2D(Collider2D a_collision);



    DESCRIPTION

            This function is called when a collision is detected. It starts
            off by checking the tag of the item it collided with and if 
            the player presses "F". It then looks for the first inventory
            slot that is empty and fills it with the item. Lastly, it
            destroys the item from the scene.

    RETURNS

            Return value is void

    */
    /**/
    void
    OnTriggerStay2D(Collider2D a_collision)
    {
        //check for collision with player
        if(a_collision.CompareTag("Player"))
        {
            Debug.Log("hit");
            //check for input key
            if (Input.GetKeyDown(KeyCode.F))
            {
                
                //loops through whole inventory to look for empty slot
                for (int i = 0; i < m_inventory.m_slots.Length; i++)
                {
                    //if slot is empty
                    if (m_inventory.m_isFilled[i] == false)
                    {
                        //get the slot's image component
                        Image m_inventorySlotImage = m_inventory.m_slots[i].GetComponent<Image>();

                        //add item to inventory  (inventory slot location)
                        Instantiate(m_item, m_inventory.m_slots[i].transform, false);
                        //set the sprite of the item as the slot's
                        m_inventorySlotImage.sprite = m_item.GetComponent<SpriteRenderer>().sprite;
                        //set the color of the item as the slot's
                        m_inventorySlotImage.color = m_item.GetComponent<SpriteRenderer>().color;

                        //delete item in scene
                        Debug.Log("destroy");
                        Destroy(m_item.gameObject);

                        //set the whether the slot is filled or not to be true
                        m_inventory.m_isFilled[i] = true;
                        break;

                    }
                    else
                    {
                        Debug.Log("m_filled slot is filled");
                    }
                }
            }
            else
            {
                Debug.Log("no inputs for item pick up");
            }
            
        }
        else
        {
            Debug.Log("collided with something that does not require a response");
        }
    }
    /*void PickUpController::OnTriggerStay2D(Collider2D a_collision);*/



}
