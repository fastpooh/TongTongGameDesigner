using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCtrl : MonoBehaviour
{
    private SphereCollider sphereColl;
    WaitForSeconds waitBeforeDestroy = new WaitForSeconds(3f);
    WaitForSeconds explosion = new WaitForSeconds(0.1f);

    void Start()
    {
        sphereColl = GetComponent<SphereCollider>();
        StartCoroutine(DestroyItself());
    }

    // Destroyed When it hits walls or enemy ship
    void OnTriggerEnter(Collider coll)
    {
        if(coll.CompareTag("ENEMYSHIP") || coll.CompareTag("WALL"))
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
}
