using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Des_DuckCtrl : MonoBehaviour
{
    public float moveSpeed = 0.7f;
    public int rotateSpeed = 100;
    private CharacterController controller;
    private Rigidbody rb;
    private new Transform transform;
    private Vector3 moveVec;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
    }

    float hAxis => Input.GetAxis("Horizontal");
    float vAxis => Input.GetAxis("Vertical");

    void Update()
    {
        transform.Rotate(Vector3.up*rotateSpeed*Time.deltaTime*hAxis);
        moveVec = transform.forward;

        if(vAxis < -0.9)
            moveVec = -moveVec;

        rb.AddForce(moveVec * moveSpeed * 0.1f);
    }
}
