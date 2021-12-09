using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class ClientPlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    
    private CharacterController controller;
    public float SPEED;
    public float rotationSpeed;
    private Vector3 lookdir;
    public int ownerPlayerNum;
    public PlayerInput localPlayerInput;
    private float subdatecounter;
    public bool isAlive;
    public bool canControl;
    public GameObject spellaura;

    public float cooldown1;
    public float cooldown2;


    void Start()
    {
        if(GetComponent<PhotonView>().AmOwner)
        {
            controller = GetComponent<CharacterController>();
            localPlayerInput = GetComponent<PlayerInput>();
            parentSelf();
            isAlive = true;
            canControl = true;
        }

        cooldown1 = 3.0f;
        cooldown2 = 3.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GetComponent<PhotonView>().IsMine);
        if(GetComponent<PhotonView>().IsMine == true && canControl == true && GetComponent<PlayerInput>().actions != null)
        {
            movement();
        }

        if(subdatecounter>=1.0f)
        {
            parentSelf();
        }
        else if(subdatecounter<1.0f)
        {
            subdatecounter += Time.deltaTime;
        }

        countCooldowns();
        checkHeight();

        if(canControl == true && cooldown1 >= 3.0f && localPlayerInput.actions["Primary Fire"].triggered)
        {
            GameObject[] plist =  GameObject.FindGameObjectsWithTag("PLAYER");
            foreach(GameObject x in plist)
            {
                if(x.GetComponent<PhotonView>().Owner == this.GetComponent<PhotonView>().Owner)
                {
                    castSpell(x.GetComponent<PlayerInfoPack>().power1);
                    cooldown1 = 0.0f;
                }
                
            }

        }
        
        if(canControl == true && cooldown2 >= 3.0f && localPlayerInput.actions["Secondary Fire"].triggered)
        {
            GameObject[] plist =  GameObject.FindGameObjectsWithTag("PLAYER");
            foreach(GameObject x in plist)
            {
                if(x.GetComponent<PhotonView>().Owner == this.GetComponent<PhotonView>().Owner)
                {
                    castSpell(x.GetComponent<PlayerInfoPack>().power2);
                    cooldown2 = 0.0f;
                }
            }
            
        }
        
        if(isAlive == false)
        {
            canControl = false;
        }
    }

    private void movement()
    {
        Vector3 tempMove = new Vector3(localPlayerInput.actions["Move"].ReadValue<Vector2>().x,0,localPlayerInput.actions["Move"].ReadValue<Vector2>().y);

        tempMove = tempMove*SPEED;

        if(tempMove != new Vector3(0,0,0))
        {
            lookdir = tempMove;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(lookdir),Time.deltaTime * rotationSpeed);

        transform.Translate(tempMove*SPEED, Space.World);
    }

    ///// NOTE: THIS DOES NOT ACTUALLY PARENT THE PLAYER DOUGHBOYS, THEY JUST SET OWNERSHIP INSTEAD /////
    private void parentSelf()
    {
        GameObject[] plist =  GameObject.FindGameObjectsWithTag("PLAYER");
        
        foreach(GameObject x in plist)
        {
            if(x.GetComponent<PlayerInfoPack>().playerNumber == ownerPlayerNum)
            {
                //Set ownership
                GetComponent<PhotonView>().TransferOwnership(x.GetComponent<PhotonView>().Owner);

                //Set Name
                this.name = "" + x.gameObject.name + "'s Doughboy";

                GameObject tmp = GameObject.FindGameObjectWithTag("Lifeboat");
                GetComponent<PlayerInput>().actions = tmp.GetComponent<PlayerLifeBoat>().actionset;

                //Check CBmoded
                if(PlayerPrefs.GetInt("ColorBlindMode", 0) == 0)
                {
                    //Set model color
                    switch(x.GetComponent<PlayerInfoPack>().PlayerColor)
                    {
                        case 1:
                            GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(0,0,0);
                            break;
                        case 2:
                            GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(.95f,.90f,.25f);
                            break;
                        case 3:
                            GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(.90f,.60f,0);
                            break;
                        case 4:
                            GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(.80f,.40f,0);
                            break;
                        case 5:
                            GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(.80f,.60f,.70f);
                            break;
                        case 6:
                            GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(0,.45f,.70f);
                            break;
                        case 7:
                            GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(.35f,.70f,.90f);
                            break;
                        case 8:
                            GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(0,.60f,.50f);
                            break;
                    }
                }
            } 

            
        }

        
        //Debug.Log("Set Parent");
    }

   

    private void castSpell(int spell)
    {
        GameObject g = GameObject.Find("SpellNode(Clone)");

        PhotonView p = PhotonView.Get(g);

        Vector2 c = new Vector2(spellaura.transform.position.x, spellaura.transform.position.z);

        p.RPC("recieveSpellMessages", RpcTarget.MasterClient, ownerPlayerNum, c, spell);
    }

    public void Kill()
    {
        isAlive = false;
    }

    public void Revive()
    {
        isAlive = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //stream.SendNext(this.controller);
            stream.SendNext(this.SPEED);
            stream.SendNext(this.ownerPlayerNum);
            stream.SendNext(this.subdatecounter);
            //stream.SendNext(this.localPlayerInput);
        }
        else
        {
            //this.controller = (CharacterController)stream.ReceiveNext();
            this.SPEED = (float)stream.ReceiveNext();
            this.ownerPlayerNum = (int)stream.ReceiveNext();
            this.subdatecounter = (float)stream.ReceiveNext();
            //this.localPlayerInput = (PlayerInput)stream.ReceiveNext();
        }
    }

    private void countCooldowns()
    {
        cooldown1 += Time.deltaTime;
        cooldown2 += Time.deltaTime;
    }

    private void checkHeight()
    {
        if(transform.position.y < -15)
        {
            isAlive = false;
        }
    }
}
