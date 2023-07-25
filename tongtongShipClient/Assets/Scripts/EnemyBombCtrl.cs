using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBombCtrl : MonoBehaviour
{
    private Rigidbody rb;
    public float force = 500f;
    
    WaitForSeconds waitBeforeDestroy = new WaitForSeconds(3f);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force);
        StartCoroutine(DestroyItself());
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
