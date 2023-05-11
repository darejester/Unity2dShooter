using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public int m_maxResource = 100;
    public float m_resourceRegen = 0.5f;
    public Transform m_dot;

    private float m_resource;
    private GameObject m_playerReference;



    /**/
    /*
    SkillController::Start()

    NAME

            Start - initializes m_resource and m_playerReference

    SYNOPSIS

            void SkillController::Start();



    DESCRIPTION

            This function is called at the object's creation. It initializes m_resource
            with the m_maxResource, the m_playerReference with the Player GameObject.

    RETURNS

            Return value is void

    */
    /**/
    void Start()
    {
        m_resource = m_maxResource;
        m_playerReference = GameObject.Find("Player");
    }
    /*void SkillController::Start();*/





    /**/
    /*
    SkillController::Update()

    NAME

            Update - manages any usage of any skills

    SYNOPSIS

            void SkillController::Update();



    DESCRIPTION

            This function manages any usage of any skills.
            Depending on the Player input, it will use the skill
            tied to the said input.

            Additionally, this function is also responsbile for
            the regeneration of the resouce. It will continue on
            adding to the resource pool until it is full.

    RETURNS

            Return value is void

    */
    /**/
    void Update()
    {
        //Regenerates the player's resources if it is not full (at max)
        if (m_resource < m_maxResource)
        {
            // multiply by Time.deltaTime to make the increase rate frame-rate independent
            m_resource += m_resourceRegen * Time.deltaTime; 
        }
        
        
        //uses the appropriate skill depending on the player input
        if (Input.GetKeyDown(KeyCode.E))
        {
            SkillSentry();
        }
        
    }
    /*void SkillController::Update();*/




    /**/
    /*
    SkillController::SkillSentry()

    NAME

            SkillSentry - implements the Sentry Skill

    SYNOPSIS

            void SkillController::SkillSentry();



    DESCRIPTION

            This function implements the sentry skill. When called,
            this will start by updating the resource pool depending 
            on how much the skill costs to use. After, it will proceed
            with creating a copy of the player as a sentry and making
            the appropriate modifications to it for the desired behaviour.
            Lastly, it will Destroy the sentry after the set duration.

    RETURNS

            Return value is void

    */
    /**/
    void
    SkillSentry()
    {
        int m_cost = 10;
        float m_duration = 10f;

        if(m_cost <= m_resource)
        {
            m_resource -= m_cost;

            // Create a clone
            GameObject m_sentry = Instantiate(m_playerReference, m_dot.position, Quaternion.identity);

            //changes the tag of the sentry
            m_sentry.tag = "sentry";
            //changes the layer the sentry is on
            m_sentry.gameObject.layer = LayerMask.NameToLayer("item");

            //retrieves the rigidbody2d component of sentry
            Rigidbody2D m_cloneRb = m_sentry.GetComponent<Rigidbody2D>();
            //changes the body type to kinematic
            m_cloneRb.isKinematic = true;

            //disables components that are not relevant to the behaviour of the sentry
            m_sentry.GetComponent<PlayerController>().enabled = false;
            m_sentry.GetComponent<InventoryController>().enabled = false;
            m_sentry.GetComponent<SkillController>().enabled = false;
            m_sentry.GetComponent<ShieldController>().enabled = false;

            // Destroy the sentry after 10 seconds
            Destroy(m_sentry, m_duration);
        }
        else
        {
            Debug.Log("Not enough resources");
        }
       

    }
    /*void SkillController::SkillSentry();*/




    /**/
    /*
    SkillController::GetResource()

    NAME

            GetResource - gets m_resource

    SYNOPSIS

            int SkillController::GetResource();



    DESCRIPTION

            This function retrieves the value of m_resource. 

    RETURNS

            Return value is an int that represents the current resource of the
            player

    */
    /**/
    public float
    GetResource()
    {
        return m_resource;
    }
}
