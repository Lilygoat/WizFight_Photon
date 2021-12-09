using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SelectLobbyManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBattle()
    {
        spawnPlayerObs();
        int r = Random.Range(4,6);
        PhotonNetwork.Instantiate("Prefabs/Scoreboard", new Vector3(0,0,0), Quaternion.identity);
        PhotonNetwork.LoadLevel(3);
    }

    private void spawnPlayerObs()
    {
        GameObject[] plist =  GameObject.FindGameObjectsWithTag("PLAYER");
        foreach(GameObject z in plist)
        {
            GameObject temp = PhotonNetwork.Instantiate("Prefabs/Doughboy", new Vector3(0,0,0), Quaternion.identity);

            temp.GetComponent<ClientPlayerController>().ownerPlayerNum = z.GetComponent<PlayerInfoPack>().playerNumber;
            
        }
        
    }
}
