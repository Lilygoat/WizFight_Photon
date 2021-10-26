using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LobbyWaitButtonBetween : MonoBehaviourPunCallbacks, IPunObservable
{
    public string nameholder = "OPEN";
    void Start()
    {
        
    }

    
    void Update()
    {
        GetComponent<TMP_InputField>().text = nameholder;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.nameholder);
        }
        else
        {
            this.nameholder = (string)stream.ReceiveNext();
        }
    }
}
