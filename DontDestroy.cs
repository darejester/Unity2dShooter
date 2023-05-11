using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    /**/
    /*
    DontDestroy::Awake()

    NAME

            Awake - makes this object undestroyable

    SYNOPSIS

            void DontDestroy::Awake();


    DESCRIPTION

            This function is called when an object of this class is instantiated. The 
            function calls the unity function DontDestroyOnLoad on this class' object,
            which prevents the object from getting destroyed when loading a new scene

    RETURNS

            Return value is void

    */
    /**/
    void
    Awake()
    {
        // Keep this game object from being destroyed when loading a new scene
        DontDestroyOnLoad(this.gameObject);
    }
    /*void DontDestroy::Awake();*/
}
