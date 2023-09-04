using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleFire : MonoBehaviour
{
    public CapsuleCollider coll;
    private bool isFireOn;

    public float coolTime = 0.5f;
    private float curTime;

    void Start()
    {
        coll.enabled = true;
        isFireOn = true;
        curTime = coolTime;
    }

    void Update()
    {
        curTime = curTime - Time.deltaTime;

        if(curTime <= 0 && isFireOn)
        {
            coll.enabled = false;
            isFireOn = false;
            curTime = coolTime;
        }
        else if(curTime <= 0 && !isFireOn)
        {
            coll.enabled = true;
            isFireOn = true;
            curTime = coolTime;
        }
    }
}
