using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerLifeBoat : MonoBehaviourPunCallbacks
{
    void Update()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("PLAYER");

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
