using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiBomb : MonoBehaviourPunCallbacks
{
    // Photon related variables
    private PhotonView pv;

    // Bomb variables.
    private SphereCollider sphereColl;
    WaitForSeconds waitBeforeDestroy = new WaitForSeconds(3f);
    WaitForSeconds explosion = new WaitForSeconds(0.1f);

    void Start()
    {
        pv = GetComponent<PhotonView>();
        sphereColl = GetComponent<SphereCollider>();
        StartCoroutine(DestroyItself());
    }

    // Destroyed When it hits walls or enemy ship
    void OnTriggerEnter(Collider coll)
    {
        if(coll.CompareTag("Player1") || coll.CompareTag("Player2") || coll.CompareTag("WALL"))
        {
            Debug.Log(coll.tag);
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
        pv.RPC("SyncExplosion", RpcTarget.Others);
        Explode();
        yield return explosion;
        pv.RPC("SyncDestroy", RpcTarget.Others);
        Destroy(gameObject);
    }

    [PunRPC]
    void SyncExplosion()
    {
        // exploding effect
        sphereColl.radius = 2f;
    }

    [PunRPC]
    void SyncDestroy()
    {
        Destroy(this.gameObject);
    }
}
