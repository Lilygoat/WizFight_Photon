using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Scorecontrol : MonoBehaviourPunCallbacks, IPunObservable
{
    public int p1score;
    public int p2score;
    public int p3score;
    public int p4score;

    public GameObject p1s;
    public GameObject p1n;
    public GameObject p2s;
    public GameObject p2n;
    public GameObject p3s;
    public GameObject p3n;
    public GameObject p4s;
    public GameObject p4n;


    void Start()
    {
        
    }


    void Update()
    {
        drawSores();
    }

    public void recieveScores(int id)
    {
        switch(id-1000-PhotonNetwork.PlayerList.Length)
        {
            case 1:
                p1score++;
            break;
            case 2:
                p2score++;
            break;
            case 3:
                p3score++;
            break;
            case 4:
                p4score++;
            break;
        }
    }

    private void drawSores()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("PLAYER");
        foreach(GameObject x in temp)
        {
            switch(x.GetComponent<PhotonView>().ViewID-1000){
                case 1:
                    p1n.GetComponent<TMP_Text>().text = x.gameObject.name;
                    p1s.GetComponent<TMP_Text>().text = p1score.ToString();
                break;
                case 2:
                    p2n.GetComponent<TMP_Text>().text = x.gameObject.name;
                    p2s.GetComponent<TMP_Text>().text = p2score.ToString();
                break;
                case 3:
                    p3n.GetComponent<TMP_Text>().text = x.gameObject.name;
                    p3s.GetComponent<TMP_Text>().text = p3score.ToString();
                break;
                case 4:
                    p4n.GetComponent<TMP_Text>().text = x.gameObject.name;
                    p4s.GetComponent<TMP_Text>().text = p4score.ToString();
                break;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.p1score);
            stream.SendNext(this.p2score);
            stream.SendNext(this.p3score);
            stream.SendNext(this.p4score);
        }
        else
        {
            this.p1score = (int)stream.ReceiveNext();
            this.p2score = (int)stream.ReceiveNext();
            this.p3score = (int)stream.ReceiveNext();
            this.p4score = (int)stream.ReceiveNext();
        }
    }
}
