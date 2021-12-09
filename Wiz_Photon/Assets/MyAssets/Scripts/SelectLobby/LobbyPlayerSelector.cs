using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class LobbyPlayerSelector : MonoBehaviourPunCallbacks
{
    public int playernum;
    public Button L1;
    public Button R1;
    public Button L2;
    public Button R2;
    public Button L3;
    public Button R3;

    public Image colIm;
    public TMP_Text pow1txt;
    public TMP_Text pow2txt;
    void Start()
    {
        PhotonView temp = PhotonView.Get(this.transform.parent.gameObject);

        L1.onClick.AddListener(delegate{incrimentPow1(false);});
        R1.onClick.AddListener(delegate{incrimentPow1(true);});
        L3.onClick.AddListener(delegate{incrimentPow2(false);});
        R3.onClick.AddListener(delegate{incrimentPow2(true);});
        L2.onClick.AddListener(delegate{incrimentColor(false);});
        R2.onClick.AddListener(delegate{incrimentColor(true);});

        GameObject[] plist =  GameObject.FindGameObjectsWithTag("PLAYER");
        foreach(GameObject x in plist)
        {
            if(playernum == x.GetComponent<PlayerInfoPack>().playerNumber)
            {
                setImage(x.GetComponent<PlayerInfoPack>().PlayerColor);
                setPow(1, x.GetComponent<PlayerInfoPack>().power1);
                setPow(2, x.GetComponent<PlayerInfoPack>().power2);
            }
        }
    }

    void Update()
    {
        GameObject[] plist =  GameObject.FindGameObjectsWithTag("PLAYER");
        foreach(GameObject x in plist)
        {
            if(playernum == x.GetComponent<PlayerInfoPack>().playerNumber)
            {
                setImage(x.GetComponent<PlayerInfoPack>().PlayerColor);
                setPow(1, x.GetComponent<PlayerInfoPack>().power1);
                setPow(2, x.GetComponent<PlayerInfoPack>().power2);
            }
        }
    }


    private void incrimentPow1(bool pos)
    {
        GameObject[] plist =  GameObject.FindGameObjectsWithTag("PLAYER");
        
        foreach(GameObject x in plist)
        {
            if(PhotonNetwork.LocalPlayer.ActorNumber == playernum && playernum == x.GetComponent<PlayerInfoPack>().playerNumber)
            {
                x.GetComponent<PlayerInfoPack>().incpow(1,pos);
                
            }
            setPow(1, x.GetComponent<PlayerInfoPack>().power1);
        }
    }

    private void incrimentPow2(bool pos)
    {
        GameObject[] plist =  GameObject.FindGameObjectsWithTag("PLAYER");
        foreach(GameObject x in plist)
        {
            if(PhotonNetwork.LocalPlayer.ActorNumber == playernum && playernum == x.GetComponent<PlayerInfoPack>().playerNumber)
            {
                x.GetComponent<PlayerInfoPack>().incpow(2, pos);
                
            }
            setPow(2, x.GetComponent<PlayerInfoPack>().power2);
        }
    }

    private void incrimentColor(bool pos)
    {
        GameObject[] plist =  GameObject.FindGameObjectsWithTag("PLAYER");
        foreach(GameObject x in plist)
        {
            if(PhotonNetwork.LocalPlayer.ActorNumber == playernum && playernum == x.GetComponent<PlayerInfoPack>().playerNumber)
            {
                x.GetComponent<PlayerInfoPack>().incColor(pos);
                
            }
            setImage(x.GetComponent<PlayerInfoPack>().PlayerColor);
        }
    }

    private void setImage(int num)
    {
        //Debug.Log("SetIm: " + num);
        if(PlayerPrefs.GetInt("ColorBlindMode", 0) == 0)
        {
            switch(num)
            {
                case 1:
                    colIm.color = new Color(0,0,0);
                    break;
                case 2:
                    colIm.color = new Color(.95f,.90f,.25f);
                    break;
                case 3:
                    colIm.color = new Color(.90f,.60f,0);
                    break;
                case 4:
                    colIm.color = new Color(.80f,.40f,0);
                    break;
                case 5:
                    colIm.color = new Color(.80f,.60f,.70f);
                    break;
                case 6:
                    colIm.color = new Color(0,.45f,.70f);
                    break;
                case 7:
                    colIm.color = new Color(.35f,.70f,.90f);
                    break;
                case 8:
                    colIm.color = new Color(0,.60f,.50f);
                    break;
            }
        }
        
    }

    private void setPow(int pow, int num)
    {
        //Debug.Log("Set pow" + pow + ": " + num);
        if(pow == 1)
        {
            switch(num)
            {
                case 1:
                    pow1txt.text = "Ramp";
                    break;
                case 2:
                    pow1txt.text = "Push";
                    break;
                case 3:
                    pow1txt.text = "Shock";
                    break;
                case 4:
                    pow1txt.text = "Cyclone";
                    break;
                case 5:
                    pow1txt.text = "Freeze";
                    break;
                case 6:
                    pow1txt.text = "Polymorph";
                    break;
                case 7:
                    pow1txt.text = "Charge";
                    break;
                case 8:
                    pow1txt.text = "Random";
                    break;
            }
        }
        else if(pow == 2)
        {
            switch(num)
            {
                case 1:
                    pow2txt.text = "Ramp";
                    break;
                case 2:
                    pow2txt.text = "Push";
                    break;
                case 3:
                    pow2txt.text = "Shock";
                    break;
                case 4:
                    pow2txt.text = "Cyclone";
                    break;
                case 5:
                    pow2txt.text = "Freeze";
                    break;
                case 6:
                    pow2txt.text = "Polymorph";
                    break;
                case 7:
                    pow2txt.text = "Charge";
                    break;
                case 8:
                    pow2txt.text = "Random";
                    break;
            }
        }
        
    }
    
}
