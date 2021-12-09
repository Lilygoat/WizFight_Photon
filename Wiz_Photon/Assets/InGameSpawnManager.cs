using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class InGameSpawnManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject[] spawnPoints = new GameObject[4];
    public GameObject countDowntimer;
    private float timer;
    public bool detectforwin;
    public string currentText;

    void Start()
    {
        GameStartSpawns();
        /* if(!PhotonNetwork.IsMasterClient)
        {
            DestroyImmediate(this,true);
        } */
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        startRound();

        if(PhotonNetwork.IsMasterClient)
        {
            if(detectforwin == true)
            {
                findAWin();
            }
        } 
        
        
    }

    private void startRound()
    {
        if(timer < 1.5)
        {
            
            currentText = "Ready?";
            writeText();
            countDowntimer.GetComponent<TMP_Text>().fontSize = 115.0f;
            GameObject[] temp = GameObject.FindGameObjectsWithTag("PLAYERDB");
            foreach(GameObject x in temp)
            {
                x.GetComponent<ClientPlayerController>().canControl = false;
            }
        }
        else if(timer < 2.5)
        {
            countDowntimer.GetComponent<TMP_Text>().fontSize = 200.0f;
            currentText = "3";
            writeText();
        }
        else if(timer < 3.5)
        {
            countDowntimer.GetComponent<TMP_Text>().fontSize = 200.0f;
            currentText = "2";
            writeText();
        }
        else if(timer < 4.5)
        {
            countDowntimer.GetComponent<TMP_Text>().fontSize = 200.0f;
            currentText = "1";
            writeText();
        }
        else if(timer < 5.5)
        {
            countDowntimer.GetComponent<TMP_Text>().fontSize = 200.0f;
            currentText = "GO!";
            writeText();
            GameObject[] temp = GameObject.FindGameObjectsWithTag("PLAYERDB");
            foreach(GameObject x in temp)
            {
                x.GetComponent<ClientPlayerController>().canControl = true;
            }

            detectforwin = true;
        }
        else if(timer > 5.5)
        {
            countDowntimer.gameObject.SetActive(false);
        }
    }

    private void GameStartSpawns()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("PLAYERDB");

        List<GameObject> players = new List<GameObject>(temp);
        int x = 0;
        while(players.Count > 0)
        {
            int rando = 0;
            if(players.Count>1)
            {
                rando = Random.Range(0,players.Count);
            }
            
            players[rando].transform.position = spawnPoints[x].transform.position;
            players[rando].gameObject.GetComponent<ClientPlayerController>().canControl = false;
            x++;
            players.RemoveAt(rando);
        }
    }

    private void findAWin()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("PLAYERDB");
        int count = 0;
        GameObject winner = null;
        foreach(GameObject x in temp)
        {
            if(x.GetComponent<ClientPlayerController>().isAlive == true)
            {
                count++;
                winner = x;
            }
        }
        

        if(count<=1)
        {
            detectforwin = false;

            GameObject thing = GameObject.FindGameObjectWithTag("Scoreboard");
            thing.GetComponent<Scorecontrol>().recieveScores(winner.GetComponent<PhotonView>().ViewID);

            randomSelectNextLevel();
        }
    }

    private void randomSelectNextLevel()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("PLAYERDB");
        foreach(GameObject x in temp)
        {
            x.GetComponent<ClientPlayerController>().isAlive = true;
        }

        int nextlvl = Random.Range(4,6);
        PhotonNetwork.LoadLevel(nextlvl);
    }

    private void writeText()
    {
        countDowntimer.GetComponent<TMP_Text>().text = currentText;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.currentText);
            
        }
        else
        {
            this.currentText = (string)stream.ReceiveNext();
        }
    }
    
}
