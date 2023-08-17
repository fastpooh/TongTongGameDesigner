using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TorpedoBombCtrl : MonoBehaviour
{
    private Rigidbody rb;
    //public float force = 300f;
    private NavMeshAgent agent;
    public float bombSpeed = 10.0f;
    private GameObject playerDuck;
    private Transform playerTransform;
    
    
    WaitForSeconds waitBeforeDestroy = new WaitForSeconds(5f);

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //rb.AddForce(transform.forward * force);
        StartCoroutine(DestroyItself());
        playerDuck = GameObject.Find("RubberDuck");
        playerTransform = playerDuck.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = playerTransform.position;
        agent.isStopped = false;
        agent.speed = bombSpeed;
    }

    void Update()
    {
        
        agent.speed = bombSpeed;
        agent.SetDestination(playerTransform.position);
        //if (bombSpeed >= 20)
        //    bombSpeed = 20;
        //bombSpeed += 0.01f;
        //Debug.Log(bombSpeed);
    }

    

    void OnTriggerEnter(Collider coll)
    {
        if(coll.CompareTag("SHIP") || coll.CompareTag("WALL"))
            Destroy(gameObject);
    }

    IEnumerator DestroyItself()
    {
        yield return waitBeforeDestroy;
        Destroy(gameObject);
    }
}
