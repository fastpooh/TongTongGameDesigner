using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Torpedo : MonoBehaviour
{
    public float bombSpeed = 10.0f;

    private Transform tr;
    private NavMeshAgent agent;

    private bool foundBoat;
    private GameObject playerDuck;
    private Transform playerTransform;

    private SphereCollider sphereColl;
    WaitForSeconds waitBeforeDestroy = new WaitForSeconds(2.7f);
    WaitForSeconds explosion = new WaitForSeconds(0.1f);

    void Start()
    {
        sphereColl = GetComponent<SphereCollider>();
        foundBoat = false;
        StartCoroutine(FindDuckBoat());
        StartCoroutine(DestroyItself());
    }
    void Update()
    {
        if(foundBoat)
        {
            //Debug.Log("???");   
            agent.isStopped = false;
            agent.SetDestination(playerTransform.position);
        }
    }
            

    // Destroyed When it hits walls or enemy ship
    void OnTriggerEnter(Collider coll)
    {
        if(coll.CompareTag("SHIP") || coll.CompareTag("WALL"))
        {
            Explode();
            Destroy(gameObject);
        }
    }

    // Bomb explodes. Bigger area is affected by the explosion.
    void Explode()
    {
        // exploding effect
        sphereColl.radius = 2f;
    }

    // Bomb destroyed after 3 seconds with explosion
    IEnumerator DestroyItself()
    {
        yield return waitBeforeDestroy;
        Explode();
        yield return explosion;
        Destroy(gameObject);
    }

    IEnumerator FindDuckBoat()
    {
        yield return new WaitForSeconds(0.01f);
        // Initialize player duck ship
        playerDuck = GameObject.FindWithTag("SHIP");
        playerTransform = playerDuck.transform;

        // Initialize enemy ship
        tr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = playerTransform.position;
        agent.speed = bombSpeed;
        foundBoat = true;
    }
}
