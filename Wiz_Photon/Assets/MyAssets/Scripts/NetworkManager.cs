using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon;
using TMPro;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public bool isActive = false;
    public bool isHost = false;
    public string HOSTROOMCODE = "";
    public string CONNECTINGROOMCODE = "";
    public TMP_Text rcholder;
    public TMP_InputField inpi;
    public TMP_InputField inpiname;
    public TMP_Text roomcount;
    
    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Successfully connected to: " + PhotonNetwork.CloudRegion);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void createHost()
    {
        HOSTROOMCODE = generateRoomCode();
        RoomOptions rm = new RoomOptions();
        rm.MaxPlayers = 4;
        rm.PlayerTtl = 8000;
        rm.EmptyRoomTtl = 8000;
        isHost = true;
        

        PhotonNetwork.CreateRoom(HOSTROOMCODE, rm);

        
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created with code: " + HOSTROOMCODE);
        rcholder.text = HOSTROOMCODE;

        //GameObject myp = PhotonNetwork.InstantiateRoomObject("Prefabs/PlayerOb", new Vector3(0,0,0), Quaternion.identity);

        //myp.GetComponent<PlayerInfoPack>().PlayerName = inpiname.text;

        //DontDestroyOnLoad(myp);
        PhotonNetwork.NickName = inpiname.text;

        PhotonNetwork.LoadLevel(1);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("[Room Creation Error] Code: " + returnCode + "  Message: " + message);
    }

    public override void OnJoinedRoom()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Connected to room: " + CONNECTINGROOMCODE);
            rcholder.text = CONNECTINGROOMCODE;

            
        }
    }

    private string generateRoomCode()
    {
        string allow = "QWERTYUIOPLKJHGFDSAZXCVBNM1234567890";
        string roomcode = "";
        for(int tot = 0; tot<5; tot++)
        {
            roomcode += allow[Random.Range(0,allow.Length)];
        }
        /////////////////////////////////
        //      REMOVE THIS SHIT
        roomcode = "qqqqq";
        ////////////////////////////////
        return roomcode;
    }

    public void JoinAsClient()
    {
        CONNECTINGROOMCODE = inpi.text;
        PhotonNetwork.NickName = inpiname.text;
        PhotonNetwork.JoinRoom(CONNECTINGROOMCODE);
    }

    
    void Update()
    {
        if(PhotonNetwork.PlayerList != null && PhotonNetwork.PlayerList.Length > 0)
        {
            roomcount.text = PhotonNetwork.PlayerList.Length + "/4";
        }
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
