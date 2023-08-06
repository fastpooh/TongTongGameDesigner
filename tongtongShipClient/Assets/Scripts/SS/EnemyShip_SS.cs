using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip_SS : MonoBehaviour
{
    public GameObject ball;
    public float health = 200f;
    public float coolTime = 3f;
    public int power = 15;
    private new Transform transform;
    private IEnumerator keepShooting; 
    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {  
        transform = GetComponent<Transform>();
        keepShooting = Fire();
        StartCoroutine(keepShooting);
    }
    void Update() 
    {
        CheckHealth();
        if(GameManager_SS.Instance.isPaused) {
            StopCoroutine(keepShooting);
        } 
    }
    IEnumerator Fire() {
        while(true) {
            GameObject projectile = Instantiate(ball, transform.position + new Vector3(0, 0, -3), transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(transform.forward*power, ForceMode.Impulse);
            yield return new WaitForSeconds(coolTime);
        }
    }
    void CheckHealth() {
        if (health <= 0 && !isDead) {
            isDead = true;
            GameManager_SS.Instance.StageClear();
        }
    }
}
