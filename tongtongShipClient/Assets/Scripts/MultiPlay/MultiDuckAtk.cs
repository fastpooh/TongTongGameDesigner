using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class MultiDuckAtk : MonoBehaviourPunCallbacks
{
    // Photon related variables
    private PhotonView pv;

    // DuckCtrl.cs
    private MultiDuckCtrl duckInfo;

    // Variables related to shooting bombs
    public Transform cannonTransform;
    public GameObject bomb1;               // Player 1 bomb
    public GameObject bomb2;               // Player 2 bomb
    public float power = 300f;

    // Variables related to cooltime circle
    private GameObject coolTimePanel;
    private TextMeshProUGUI timer;
    private Image coolTimeCircle;
    public float coolTime = 3f;
    private float countDown;

    // Variables related to cooltime warning message
    private GameObject cooltimeWarning;
    WaitForSeconds warningDisappear = new WaitForSeconds(1f);

    void Start()
    {
        // Get DuckCtrl.cs
        duckInfo = GetComponent<MultiDuckCtrl>();

        // Get photon view component
        pv = GetComponent<PhotonView>();

        // Initialize cooltime circle variables
        coolTimePanel = GameObject.FindWithTag("COOLTIME");
        timer = coolTimePanel.transform.Find("CoolTimer").GetComponent<TextMeshProUGUI>();
        coolTimeCircle = coolTimePanel.transform.Find("CoolTimeCircle").GetComponent<Image>();
        cooltimeWarning= coolTimePanel.transform.Find("NotYetReady").gameObject;
        
        // Initialize cooltime warning variables
        cooltimeWarning.SetActive(false);
        countDown = 0;
    }

    void Update()
    {
        if(pv.IsMine)
        {
            CoolTimeUpdate();
            if(!duckInfo.isDead)
                Attack();
        }
    }

    void Attack()
    {
        // Left click when cooltime did not pass
        if(Input.GetMouseButtonDown(0) && !isShootPossible())
        {
            StartCoroutine(CoolTimeWarning());
        }

        // Shoot one bomb when you left click 
        if(Input.GetMouseButtonDown(0) && isShootPossible())
        {
            pv.RPC("Shoot", RpcTarget.Others);

            if(this.CompareTag("Player1"))
            {
                GameObject bom1= Instantiate(bomb1, cannonTransform.position, cannonTransform.rotation*Quaternion.Euler(0, 0, 0));
                Rigidbody rBod1 = bom1.GetComponent<Rigidbody>();
                rBod1.AddForce(cannonTransform.right * power);
                countDown = coolTime;
            }
            else
            {
                GameObject bom2= Instantiate(bomb2, cannonTransform.position, cannonTransform.rotation*Quaternion.Euler(0, 0, 0));
                Rigidbody rBod2 = bom2.GetComponent<Rigidbody>();
                rBod2.AddForce(cannonTransform.right * power);
                countDown = coolTime;
            }
        }
    }

    void CoolTimeUpdate()
    {
        if(countDown > 0)
        {
            countDown = countDown - 1*Time.deltaTime;
            timer.text = ((int)countDown + 1).ToString();
            coolTimeCircle.fillAmount = 1 - countDown/3;
        }
        else if (countDown <= 0)
        {
            countDown = 0;
            coolTimeCircle.fillAmount = 1;
            timer.text = "Ready!";
        }
    }

    bool isShootPossible()
    {
        if(countDown <= 0)
            return true;
        else
            return false;
    }

    IEnumerator CoolTimeWarning()
    {
        cooltimeWarning.SetActive(true);
        yield return warningDisappear;
        cooltimeWarning.SetActive(false);
    }

    [PunRPC]
    void Shoot()
    {
        if(this.CompareTag("Player1"))
        {
            GameObject bom1 = Instantiate(bomb1, cannonTransform.position, cannonTransform.rotation*Quaternion.Euler(0, 0, 0));
            Rigidbody rBody1 = bom1.GetComponent<Rigidbody>();
            rBody1.AddForce(cannonTransform.right * power);
        }
        else
        {
            GameObject bom1 = Instantiate(bomb2, cannonTransform.position, cannonTransform.rotation*Quaternion.Euler(0, 0, 0));
            Rigidbody rBody1 = bom1.GetComponent<Rigidbody>();
            rBody1.AddForce(cannonTransform.right * power);
        }
    }
}
