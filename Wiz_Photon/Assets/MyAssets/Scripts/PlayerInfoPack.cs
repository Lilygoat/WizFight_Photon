using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInfoPack : MonoBehaviourPunCallbacks, IPunObservable
{
    public string PlayerName;
    public string clientIDnum;
    public int playerNumber;
    public int power1;
    public int powlimit;
    public int power2;
    public int PlayerColor = 1;
    public int colorLimit = 8;

    void Update()
    {
        getPlayerName();
        getPlayerNumber();
    }

    public void getPlayerNumber()
    {
        playerNumber = GetComponent<PhotonView>().Owner.ActorNumber;        
    }

    private void getPlayerName()
    {
        PlayerName = GetComponent<PhotonView>().Owner.NickName;
        this.gameObject.name = PlayerName;

        this.gameObject.tag = "PLAYER";
    }

    public void incpow(int pow, bool pos)
    {
        Debug.Log("pow: " + pow + " pos: " + pos);
        if(pos)
        {
            if(pow == 1)
            {
                if(power1 >= powlimit)
                {
                    power1 = 0;
                }
                else
                {
                    power1++;
                }
            }
            else if(pow == 2)
            {
                if(power2 >= powlimit)
                {
                    power2 = 0;
                }
                else
                {
                    power2++;
                }
            }
        }
        else
        {
            if(pow == 1)
            {
                if(power1 <= 0)
                {
                    power1 = powlimit;
                }
                else
                {
                    power1--;
                }
            }
            else if(pow == 2)
            {
                if(power2 <= 0)
                {
                    power2 = powlimit;
                }
                else
                {
                    power2--;
                }
            }
        }
    }

    public void incColor(bool pos)
    {
        //Debug.Log("Color: " + pos);
        if(pos)
        {
            if(PlayerColor >= colorLimit)
            {
                PlayerColor = 1;
            }
            else
            {
                PlayerColor++;
            }
        }
        else
        {
            if(PlayerColor <= 1)
            {
                PlayerColor = colorLimit;
            }
            else
            {
                PlayerColor--;
            }
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.PlayerName);
            stream.SendNext(this.clientIDnum);
            stream.SendNext(this.playerNumber);
            stream.SendNext(this.power1);
            stream.SendNext(this.power2);
            stream.SendNext(this.PlayerColor);
        }
        else
        {
            this.PlayerName = (string)stream.ReceiveNext();
            this.clientIDnum = (string)stream.ReceiveNext();
            this.playerNumber = (int)stream.ReceiveNext();
            this.power1 = (int)stream.ReceiveNext();
            this.power2 = (int)stream.ReceiveNext();
            this.PlayerColor = (int)stream.ReceiveNext();
        }
    }
}
