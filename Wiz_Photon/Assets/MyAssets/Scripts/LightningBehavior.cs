using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LightningBehavior : MonoBehaviourPunCallbacks
{
    private float timeAlive;
    public float lifespan;

    void Update()
    {
        timeAlive += Time.deltaTime;

        if(timeAlive >= lifespan)
        {
            Destroy(this.gameObject);
        }
    }


    void OnTriggerEnter(Collider target)
    {
        if(target.gameObject.GetComponent<ClientPlayerController>() && PhotonNetwork.IsMasterClient == true)
        {
            target.gameObject.GetComponent<ClientPlayerController>().Kill();
        }
        
    }

    
}
