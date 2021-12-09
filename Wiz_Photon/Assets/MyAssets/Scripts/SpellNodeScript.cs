using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class SpellNodeScript : MonoBehaviourPunCallbacks
{
    public float LightningRadius;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    [PunRPC]
    void recieveSpellMessages(int playerNumber, Vector2 coordinates, int attackType)
    {
        switch(attackType)
        {
            case 1: 
                break;
            case 2:
                
                break;
            case 3:
                castLightning(coordinates);
                break;
            case 4:
                castCyclone(coordinates);
                break;
            
            
        }
        Debug.Log("Player: " + playerNumber + "  Cast: " + attackType);
    }

    private void castLightning(Vector2 coords)
    {
        // Generates New coordinates to spawn at within a circle with radius R(LightningRadius)
        float theta = 2* Mathf.PI * Random.Range(0.0f, 1.0f);
        float genrad = LightningRadius * Random.Range(0.0f, 1.0f);
        float genx = coords.x + genrad * Mathf.Cos(theta);
        float geny = coords.y + genrad * Mathf.Cos(theta);

        PhotonNetwork.Instantiate(Path.Combine("Prefabs/LightningBolt"), new Vector3(genx, 0, geny), Quaternion.identity);
    }

    private void castPush(int pnum)
    {
        
    }

    private void castCyclone(Vector2 coords)
    {
        PhotonNetwork.Instantiate(Path.Combine("Prefabs/Cyclone"), new Vector3(coords.x, 0, coords.y), Quaternion.identity);
    }
}
