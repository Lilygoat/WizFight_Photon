using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CycloneBehavior : MonoBehaviourPunCallbacks
{
    private float timeAlive;
    public float lifespan;
    public float thrust;

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
            Vector3 temp = new Vector3(Random.Range(-1,2), Random.Range(0,1), Random.Range(-1,2));
            target.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(temp*thrust, this.transform.position, ForceMode.Impulse);
            Debug.Log(target.gameObject.name + " launched at " + temp*thrust);
        }
        
    }
}
