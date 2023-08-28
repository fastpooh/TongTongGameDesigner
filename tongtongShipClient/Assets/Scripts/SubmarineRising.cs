using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class SubmarineRising : MonoBehaviour
{
    // Submarine transform
    private Transform tr;

    // Information about player
    private GameObject playerDuck;
    private Transform playerTransform;
    private bool foundBoat;
    
    // About the boats spec
    public float shootRange = 25.0f;
    public float coolTime = 10f;
    public float risingTime = 5.0f;
    private float enemyCountDown;



    public bool isRising = false;
    public GameObject submarine;
    private Transform submarineTransform;


    public BoxCollider hitBox;



    void Start()
    {
        tr = GetComponent<Transform>();
        foundBoat = false;
        StartCoroutine(FindDuckBoat());
        hitBox.enabled = true;
    }

    void Update()
    {
        if(foundBoat)
        {
            Rising();
        }
    }

    void Rising()
    {
        enemyCountDown = enemyCountDown - Time.deltaTime;
        if (enemyCountDown <= 0)
            enemyCountDown = 0;
        
        float distance = Vector3.Distance(playerTransform.position, tr.position);
        if (distance < shootRange && enemyCountDown == 0)
        {
            isRising = true;
            
            enemyCountDown = coolTime;
        }
        else if (enemyCountDown <= risingTime)
        {
            isRising = false;
        }

        if (isRising)
        {
            tr.position = Vector3.Lerp(tr.transform.position, submarineTransform.position+new Vector3(0, 0, 0), 0.05f);
            hitBox.enabled = true;
        }
        else 
        {
            tr.position = Vector3.Lerp(tr.transform.position, submarineTransform.position+new Vector3(0, -2f, 0), 0.01f);
            hitBox.enabled = false;
        }
    }

    IEnumerator FindDuckBoat()
    {
        yield return new WaitForSeconds(0.01f);
        // Initialize player duck ship
        playerDuck = GameObject.FindWithTag("SHIP");
        playerTransform = playerDuck.transform;


        // Initialize enemy ship
        submarineTransform = submarine.transform;
        enemyCountDown = coolTime;
        foundBoat = true; 
    }
}
