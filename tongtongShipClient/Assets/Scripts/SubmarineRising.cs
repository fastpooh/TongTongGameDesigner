using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class SubmarineRising : MonoBehaviour
{
    private Transform tr;
    private GameObject playerDuck;
    public GameObject submarine;
    private Transform playerTransform;
    private Transform submarineTransform;
    
    private bool isDie = false;
    public float shootRange = 25.0f;
    public float coolTime = 10.0f;

    private float enemyCoolTime;
    private float speed=3.0f;
    private bool isRising = false;

    WaitForSeconds rest = new WaitForSeconds(0.3f);

    void Start()
    {
        tr = GetComponent<Transform>();
        playerDuck = GameObject.Find("RubberDuck");
        playerTransform = playerDuck.transform;
        submarineTransform = submarine.transform;
        enemyCoolTime = coolTime;
    }

    void Update()
    {

        enemyCoolTime = enemyCoolTime - Time.deltaTime;
        if (enemyCoolTime <= 0)
            enemyCoolTime = 0;
        
        float distance = Vector3.Distance(playerTransform.position, tr.position);
        if (distance < shootRange && enemyCoolTime == 0)
        {
            isRising = true;
            
            enemyCoolTime = coolTime;
        }
        else if (enemyCoolTime <= 5)
        {
            isRising = false;
            
            //tr.position = submarineTransform.position + new Vector3(0, -1f, 0);
            //tr.Translate(new Vector3(0, -1f, 0));
        }

        if (isRising)
        {
            tr.position = Vector3.Lerp(tr.transform.position, submarineTransform.position+new Vector3(0, 0, 0), 0.005f);
        }
        else 
        {
            tr.position = Vector3.Lerp(tr.transform.position, submarineTransform.position+new Vector3(0, -2f, 0), 0.005f);
        }

    }

}