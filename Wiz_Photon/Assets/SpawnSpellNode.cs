using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;


public class SpawnSpellNode : MonoBehaviourPunCallbacks
{
    public GameObject spawn;
    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.InstantiateRoomObject(Path.Combine("Prefabs/SpellNode"), new Vector3(0,0,0), Quaternion.identity);
            DestroyImmediate(this.gameObject, true);
        }
        else
        {
            DestroyImmediate(this.gameObject, true);
        }
    }
}
