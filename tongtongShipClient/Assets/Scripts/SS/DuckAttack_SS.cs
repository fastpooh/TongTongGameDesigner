using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckAttack_SS : MonoBehaviour
{
    public GameObject ball;
    public Transform shootL;
    public Transform shootR;
    public int power = 8;
    public float coolTime = 3f;
    private bool canShoot = true;
   
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0)) {
            if(!canShoot) {
            
            } else {
                GameObject projectile1 = Instantiate(ball, shootL.position, transform.rotation);
                GameObject projectile2 = Instantiate(ball, shootR.position, transform.rotation);
                Rigidbody rBody1 = projectile1.GetComponent<Rigidbody>();
                Rigidbody rBody2 = projectile2.GetComponent<Rigidbody>();

                rBody1.velocity = transform.parent.GetComponent<Rigidbody>().velocity;
                rBody2.velocity = transform.parent.GetComponent<Rigidbody>().velocity;
                
                rBody1.AddForce(shootL.up*power, ForceMode.Impulse);
                rBody2.AddForce(shootR.up*power, ForceMode.Impulse);
                
                StartCoroutine(Reload(coolTime));
            }
        }
    
    }

    IEnumerator Reload(float cool) 
    {
        Timer_SS.Instance.isCool = true;
        Timer_SS.Instance.countDownText.enabled = true;
        canShoot = false;
        yield return new WaitForSeconds(cool);
        Timer_SS.Instance.isCool = false;
        canShoot = true;
    }
}
