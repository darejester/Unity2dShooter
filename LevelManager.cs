using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform m_playerTransform;


    /**/
    /*
    LevelManager::Start()

    NAME

            Start - uses PlayerPerfs to get player's spawn point


    SYNOPSIS

            void LevelManager::Start();


    DESCRIPTION

            This function is called at the object's creation. It starts with 
            checking if a reference to the player object's transform has been assigned.
            If not, it finds the player object in the scene and assigns its transform 
            to the reference. After, it retrieves a spawn position from the player 
            preferences(PlayerPerfs) using the key "spawnPosition". It then clears
            the key from the player preferences as a preparation for the next time
            it's needed.

    RETURNS

            Return value is void

    */
    /**/
    void
    Start()
    {
        //check for m_player_transform
        if (m_playerTransform == null)
        {
            //get Player object
            GameObject m_player = GameObject.FindWithTag("Player");
            
            //check if player found
            if (m_player != null)
            {
                m_playerTransform = m_player.transform;
            }
            else
            {
                Debug.LogWarning("No player object found in the scene.");
                return;
            }
        }
        else
        {
            Debug.Log("player transform is not found");
        }

        // Get the spawn position from PlayerPrefs
        string m_spawnPositionString = PlayerPrefs.GetString("spawnPosition");

        //initialize spawn position vector to 0
        Vector3 m_spawnPosition = Vector3.zero;

        //check spawn position string
        if (!string.IsNullOrEmpty(m_spawnPositionString))
        {
            //set spawn position
            m_spawnPosition = StringToVector3(m_spawnPositionString);
            // Set the player's position to the spawn position
            m_playerTransform.position = m_spawnPosition;
        }
        else
        {
            //set spawn position
            m_spawnPosition = m_playerTransform.position;
        }

        

        // Clear the spawn position from PlayerPrefs
        PlayerPrefs.DeleteKey("spawnPosition");
    }
    /*void LevelManager::Start();*/




    /**/
    /*
    LevelManager::StringToVector3()

    NAME

            StringToVector3 - converts string from PlayerPerfs to Vector3


    SYNOPSIS

            Vector3 LevelManager::StringToVector3();
            a_string        --> the string from PlayerPerfs to be converted to Vector3


    DESCRIPTION

            This function takes in a string as input and returns a Vector3. It
            starts off by removing the parantheses and spliting the string using
            the commas. It then parses each one as floats for each part of a 
            vector3 and returns it.

    RETURNS

            Return value is a Vector3 that represents the spawn location of the
            player

    */
    /**/
    // Helper function to convert a string to a Vector3
    Vector3
    StringToVector3(string a_string)
    {
        // Remove the parentheses
        a_string = a_string.Replace("(", "").Replace(")", "");

        // Split the items
        string[] m_array = a_string.Split(',');

        // Parse the items as floats
        float x = float.Parse(m_array[0]);
        float y = float.Parse(m_array[1]);
        float z = 0f;

        // Return the Vector3
        return new Vector3(x, y, z);
    }
    /*Vector3 LevelManager::StringToVector3();*/
}
