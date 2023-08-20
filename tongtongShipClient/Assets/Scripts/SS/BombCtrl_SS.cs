using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCtrl_SS : MonoBehaviour
{
    public int damage = 10;
    public float power = 15f;
    [SerializeField] private float existTime = 3f;
    private Rigidbody rBody;
    
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        rBody.AddForce(transform.forward * power, ForceMode.Impulse);
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
