using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    private string m_source;
    public string m_nextLevelName;
    public Vector3 m_nextLevelSpawn;


    /**/
    /*
    PortalController::Start()

    NAME

            Start - initializes m_source

    SYNOPSIS

            void PortalController::Start();



    DESCRIPTION

            This function is called at the object's creation. It initializes 
            m_source with the current active scene's name

    RETURNS

            Return value is void

    */
    /**/
    void 
    Start()
    {
        //get active scene
        m_source = SceneManager.GetActiveScene().name;
    }
    /*void PortalController::Start();*/



    /**/
    /*
    PortalController::OnTriggerEnter2D()

    NAME

            OnTriggerEnter2D - Helps debugging

    SYNOPSIS

            void PortalController::OnTriggerEnter2D();



    DESCRIPTION

            This function is called whenever the player stands in front
            of a portal. This helps debugging by writing a message in
            the console whether or not the player is actually sensed.

    RETURNS

            Return value is void

    */
    /**/
    void
    OnTriggerEnter2D(Collider2D a_collision)
    {
        //check collision
        if (a_collision.CompareTag("Player"))
        {
            Debug.Log("player is in front of portal");
            //Debug.Log(previousLevelName);


        }
        else
        {
            Debug.Log("Player is not in front of portal");
        }
    }
    /*void PortalController::OnTriggerEnter2D();*/




    /**/
    /*
    PortalController::OnTriggerStay2D()

    NAME

            OnTriggerStay2D - manages portal use 

    SYNOPSIS

            void PortalController::OnTriggerStay2D(Collider2D a_collision);
            a_collision     --> the Collider2D component of the object this collided with



    DESCRIPTION

            This function is called whenever the player stands in front
            of a portal. This allows the player to use a portal and 
            go to another level of the game. It does this by checking
            whether the tag of a_collision is the Player's and if the
            player pressed "F". If yes, the player records the spawn-
            position in the PlayerPerfs, and goes to the next level.

    RETURNS

            Return value is void

    */
    /**/
    void
    OnTriggerStay2D(Collider2D a_collision)
    {
        //check collision
        if (a_collision.CompareTag("Player"))
        {
            //check input key
            if (Input.GetKeyDown(KeyCode.F))
            {
                //store spawn position in PlayerPrefs
                PlayerPrefs.SetString("spawnPosition", m_nextLevelSpawn.ToString());
                
                //loads next level
                SceneManager.LoadScene(m_nextLevelName);
            }
            else
            {
                Debug.Log("no inputs for portal use");
            }
        }
        else
        {
            Debug.Log("colliding with something that does not require a response");
        }
    }
    /*void PortalController::OnTriggerStay2D(Collider2D a_collision);*/
}
