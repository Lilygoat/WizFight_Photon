using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;

public class PlayerLifeBoat : MonoBehaviourPunCallbacks
{
    public InputActionAsset actionset;
    void Update()
    {
        //Get controllers
        GameObject[] temp = GameObject.FindGameObjectsWithTag("PLAYER");
        DontDestroyOnLoad(this.gameObject);
        GameObject sb = GameObject.FindGameObjectWithTag("Scoreboard");

        if(temp.Length == 0)
        {

        }
        else
        {
            foreach(GameObject x in temp)
            {
                DontDestroyOnLoad(x);
            }
        }

        if(sb != null)
        {
            DontDestroyOnLoad(sb);
        }

        //Get Doughboys
        temp = GameObject.FindGameObjectsWithTag("PLAYERDB");

        if(temp.Length == 0)
        {

        }
        else
        {
            foreach(GameObject x in temp)
            {
                DontDestroyOnLoad(x);
            }
        }
        
    }
}
