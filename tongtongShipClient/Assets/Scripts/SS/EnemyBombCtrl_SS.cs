using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBombCtrl_SS : MonoBehaviour
{
    public int damage = 10;
    public float existTime = 2f;
    void Start()
    {
        StartCoroutine(DestroyDelay(existTime));
        GetComponent<Rigidbody>().AddForce(transform.forward * 500f);
    }
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "SHIP") {
            Destroy(gameObject);
            col.gameObject.GetComponent<DuckCtrl_SS>().duckHP -= damage;
        }
    }
    IEnumerator DestroyDelay(float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
