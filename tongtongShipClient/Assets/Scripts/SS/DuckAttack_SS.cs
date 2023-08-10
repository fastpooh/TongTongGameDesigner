using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DuckAttack_SS : MonoBehaviour
{
    public GameObject bombPrefab;
    public Transform shootL;
    public Transform shootR;
    public float power = 15f;
    public float coolTime = 3f;
    private float countDown;
    private bool canShoot;

    public TextMeshProUGUI timer;
    public GameObject coolTimeWarning;
    public Image coolTimeImage;
   
    // Start is called before the first frame update
    void Start()
    {
        countDown = 0;
        canShoot = true;
        coolTimeWarning.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && !canShoot) {
            StartCoroutine(CoolTimeWarning());
        }
        if(Input.GetMouseButtonDown(0) && canShoot) {
            GameObject cannonball1 = Instantiate(bombPrefab, shootL.position, transform.rotation*Quaternion.Euler(0, -90f, 0));
            Rigidbody rBody1 = cannonball1.GetComponent<Rigidbody>();
            rBody1.velocity = GetComponent<Rigidbody>().velocity;
            rBody1.AddForce(shootL.forward*power, ForceMode.Impulse);
            canShoot = false;
            StartCoroutine(Reload());
        }
        else if (Input.GetMouseButtonDown(1) && canShoot) {
            GameObject cannonball2 = Instantiate(bombPrefab, shootR.position, transform.rotation);
            Rigidbody rBody2 = cannonball2.GetComponent<Rigidbody>();
            rBody2.velocity = GetComponent<Rigidbody>().velocity;
            rBody2.AddForce(shootR.forward*power, ForceMode.Impulse);
            canShoot = false;
            StartCoroutine(Reload());
        } 
    }

    IEnumerator Reload() 
    {
        countDown = coolTime;
        while( countDown > 0 ) {
            timer.text = countDown.ToString("N0");
            coolTimeImage.fillAmount = 1 - countDown/coolTime;
            yield return new WaitForSeconds(1);
            countDown--;
        }
        countDown = 0;
        coolTimeImage.fillAmount = 1;
        timer.text = "Ready!";
        canShoot = true;
    }
    IEnumerator CoolTimeWarning()
    {
        coolTimeWarning.SetActive(true);
        yield return new WaitForSeconds(1);
        coolTimeWarning.SetActive(false);
    }
}
