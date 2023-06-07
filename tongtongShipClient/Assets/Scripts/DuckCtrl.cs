using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckCtrl : MonoBehaviour
{
    private Vector3 oldMoveVec;
    private Vector3 moveVec;
    public float inertia = 0.99f;
    public float moveSpeed = 0.7f;
    public int rotateSpeed = 100;
    private float multSpeed = 1f;

    private CharacterController controller;
    private new Transform transform;
    private int k;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        Move();
    }

    float hAxis => Input.GetAxis("Horizontal");
    float vAxis => Input.GetAxis("Vertical");

    void Move()
    {
        transform.Rotate(Vector3.up*rotateSpeed*Time.deltaTime*hAxis);

        moveVec = transform.forward;
        if(vAxis < 0)
        {
            multSpeed = vAxis + 1;
        }
        else
            multSpeed = 1;
        
        transform.position += moveVec*moveSpeed*Time.deltaTime*multSpeed;
    }
}
