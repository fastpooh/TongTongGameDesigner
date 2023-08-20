using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using Unity.VisualScripting;

public class MultiDuckCtrl : MonoBehaviourPunCallbacks
{
    // Photon variables
    private PhotonView pv;
    private GM_MultiPlay gm;

    /*
    private GameObject arrow1;
    private GameObject arrow2;
    */

    // Move related variables
    private CharacterController controller;
    private Rigidbody rb;
    private new Transform transform;
    public Vector3 moveVec;

    // Control movement of boat
    public float controlOverBoat = 5f;  // With higher values, the boat follows your command better
    public int rotateSpeed = 50;        // With higher values, the boat turns faster     
    public float maxSpeed = 7f;         // The maximum speed of boat (boat accelerates from speed 0)

    // Health related variables
    // private Image healthbar;
    private int maxHP = 3;
    public int duckHp;                      // might cause error because enemy ship is taking duck HP value in Start() function
    public bool isDead = false;


    
    void Start()
    {
        // Initial settings
        controller = GetComponent<CharacterController>();
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        // Photon related settings
        pv = GetComponent<PhotonView>();
        gm = GameObject.Find("Manager").GetComponent<GM_MultiPlay>();

        // Who is my duck?
        /*
        if (PhotonNetwork.IsMasterClient)
            arrow2.SetActive(false);
        else
            arrow1.SetActive(false);
        */

        // Initialize HP

        duckHp = maxHP;

        // Find UI
        //healthbar = GameObject.Find("DuckHPbar").GetComponent<Image>();
    }

    void Update()
    {
        // Move, update when character is mine, 2 players in game and enough time pass
        if(pv.IsMine)
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                StartCoroutine(ScoreBoardOn());
            }

            if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && gm.time > 3)
            {
                MoveTurn();
            }
        }

        // My character indicator removed
        /*
        if(gm.time > 3)
        {
            arrow1.SetActive(false);
            arrow2.SetActive(false);
        }
        */
    }


    // Input : wasd
    float hAxis => Input.GetAxis("Horizontal");
    float vAxis => Input.GetAxis("Vertical");
    
    void MoveTurn()
    {
        if (!isDead) // Only move and turn when boat is alive
        {
            transform.Rotate(Vector3.up*rotateSpeed*2*Time.deltaTime*hAxis);
            moveVec = transform.forward;
            
            if(vAxis < -0.9)
                moveVec = -moveVec;

            rb.AddForce(moveVec * controlOverBoat * 8f);

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if(pv.IsMine)
        {
            if(this.CompareTag("Player1") && coll.CompareTag("Bomb2"))
            {
                duckHp--;
                if(duckHp <= 0)
                {
                    isDead = true;
                    duckHp = 0;
                }
                pv.RPC("syncHitByBomb", RpcTarget.Others, null);
            }

            if(this.CompareTag("Player2") && coll.CompareTag("Bomb1"))
            {
                duckHp--;
                if(duckHp <= 0)
                {
                    isDead = true;
                    duckHp = 0;
                }
                pv.RPC("syncHitByBomb", RpcTarget.Others, null);
            }
        }
    }

    IEnumerator ScoreBoardOn()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("Manager").transform.GetChild(0).gameObject.SetActive(true);
    }

    [PunRPC]
    void syncHitByBomb()
    {
        duckHp--;
    }
}