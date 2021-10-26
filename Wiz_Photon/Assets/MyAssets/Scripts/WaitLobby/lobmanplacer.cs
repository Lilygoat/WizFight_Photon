using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;

public class lobmanplacer : MonoBehaviourPunCallbacks
{
    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.InstantiateRoomObject(Path.Combine("Prefabs/WaitLobManager"),new Vector3(0,0,0), Quaternion.identity);
            DestroyImmediate(this.gameObject, true);
        }
        else
        {
            DestroyImmediate(this.gameObject, true);
        }
    }
}
