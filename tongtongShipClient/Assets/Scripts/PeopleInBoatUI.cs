using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PeopleInBoatUI : MonoBehaviour
{
    // Boat information
    private DuckCtrl duckInfo;
    public bool foundBoat;

    void Start()
    {
        foundBoat = false;
        StartCoroutine(FindMyBoat());
    }

    void Update()
    {
        /*
        if(foundBoat)
            Debug.Log("Paddler : " + duckInfo.paddlers.ToString());
        */
    }

    // Button events
    public void IncreasePaddler()
    {
        if(duckInfo.gunners > 0)
            duckInfo.IncreasePad();
    }

    public void IncreaseGunner()
    {
        if(duckInfo.paddlers > 0)
            duckInfo.IncreaseGun();
    }

    IEnumerator FindMyBoat()
    {
        yield return new WaitForSeconds(0.5f);
        duckInfo = GameObject.FindWithTag("SHIP").GetComponent<DuckCtrl>();
        foundBoat = true;
    }

}
