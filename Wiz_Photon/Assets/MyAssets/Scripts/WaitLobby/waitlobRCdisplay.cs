using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class waitlobRCdisplay : MonoBehaviourPunCallbacks, IPunObservable
{
    public string code;
    // Start is called before the first frame update
    void Start()
    {
        code = PhotonNetwork.CurrentRoom.Name;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMP_Text>().text = code;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.code);
        }
        else
        {
            this.code = (string)stream.ReceiveNext();
        }
    }
}
