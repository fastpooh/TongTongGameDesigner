using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCtrl : MonoBehaviour
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

    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag == "SHIP")
            Destroy(gameObject);
    }

    IEnumerator DestroyItself()
    {
        yield return waitBeforeDestroy;
        Destroy(gameObject);
    }
}
