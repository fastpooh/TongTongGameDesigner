using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DuckAttack_SS : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform cannon;
    [SerializeField] private InGameUI inGameUI;

    public float coolTime = 3f;
    private bool canShoot;

   
    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        bool pointerOverScreen = (EventSystem.current.currentSelectedGameObject == null);
        if(pointerOverScreen&&Input.GetMouseButtonDown(0)) {
            if(!canShoot) {
                StartCoroutine(inGameUI.ShowCoolWarning());
            } else {
                GameObject cannonball1 = Instantiate(bombPrefab, cannon.position, transform.rotation*Quaternion.Euler(0, 75f, 0));
                Rigidbody rBody1 = cannonball1.GetComponent<Rigidbody>();
                rBody1.velocity = GetComponent<Rigidbody>().velocity;

                GameObject cannonball2 = Instantiate(bombPrefab, cannon.position, transform.rotation*Quaternion.Euler(0, 105f, 0));
                Rigidbody rBody2 = cannonball2.GetComponent<Rigidbody>();
                rBody2.velocity = GetComponent<Rigidbody>().velocity;
                
                canShoot = false;
                StartCoroutine(Reload());
            }
        }
    }

    private IEnumerator Reload() 
    {
        StartCoroutine(inGameUI.StartCountDown(coolTime));
        yield return new WaitForSeconds(3);
        canShoot = true;
    }
}
