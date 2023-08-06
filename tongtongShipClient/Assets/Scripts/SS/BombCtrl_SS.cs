using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCtrl_SS : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 10f;
    public float existTime = 2f;
    void Start()
    {
        StartCoroutine(DestroyDelay(existTime));
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "ENEMYSHIP") {
            Destroy(gameObject);
            col.gameObject.GetComponent<EnemyShip_SS>().health -= damage;
        } 
    }
    IEnumerator DestroyDelay(float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
