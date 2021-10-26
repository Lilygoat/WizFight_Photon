using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class WaitLobbyMan : MonoBehaviourPunCallbacks
{
    LobbyWaitButtonBetween[] slots = new LobbyWaitButtonBetween[4];
    public GameObject canv;
    public GameObject startbtn;

    void Start()
    {
        canv = PhotonNetwork.InstantiateRoomObject("Prefabs/WaitingLobbyCanvas", new Vector3(0,0,0), Quaternion.identity);
        if(PhotonNetwork.IsMasterClient)
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Pslots");
            for(int i=0; i<temp.Length; i++)
            {
                slots[i] = temp[i].GetComponent<LobbyWaitButtonBetween>();
                
            }

            writeSlot();
        }
        if(PhotonNetwork.IsMasterClient)
        {
            addStart();
        }
        
    }


    void Update()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            writeSlot();
        }
    }

    private void writeSlot()
    {
        for(int x=0; x<PhotonNetwork.PlayerList.Length; x++)
        {
            slots[x].nameholder = PhotonNetwork.PlayerList[x].NickName;
        }
    }

    private void addStart()
    {
        GameObject tmp =  Instantiate(startbtn);
        tmp.transform.SetParent(canv.transform, false);
        tmp.GetComponent<Button>().onClick.AddListener(goToSelection);
    }

    private void goToSelection()
    {
        foreach(Photon.Realtime.Player x in PhotonNetwork.PlayerList)
        {
            GameObject temp = PhotonNetwork.Instantiate("Prefabs/PlayerOb", new Vector3(0,0,0), Quaternion.identity);
            temp.GetComponent<PlayerInfoPack>().PlayerName = x.NickName;
            temp.GetComponent<PlayerInfoPack>().clientIDnum = x.UserId;
            temp.tag = "PLAYER";
            DontDestroyOnLoad(temp);
            temp.GetComponent<PhotonView>().TransferOwnership(x);

            Debug.Log(x.ToString());
        }

        PhotonNetwork.LoadLevel(2);
    }
    

    [PunRPC]
    public void createPlayerObjectForClient(string nam, PhotonMessageInfo info)
    {
        GameObject myp = PhotonNetwork.InstantiateRoomObject("Prefabs/PlayerOb", new Vector3(0,0,0), Quaternion.identity);

        myp.GetComponent<PlayerInfoPack>().PlayerName = nam;
        myp.GetComponent<PlayerInfoPack>().clientIDnum = info.Sender.UserId;

        DontDestroyOnLoad(myp);
    }
}
