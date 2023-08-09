using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCtrl_SS : MonoBehaviour
{
    public int damage = 10;
    public float existTime = 2f;
    private Rigidbody rBody;
    
    void Start()
    {
        StartCoroutine(DestroyDelay(existTime));
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "ENEMYSHIP") {
            Destroy(gameObject);
            col.gameObject.GetComponent<EnemyShip_SS>().enemyHP -= damage;
        } 
    }
    IEnumerator DestroyDelay(float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
