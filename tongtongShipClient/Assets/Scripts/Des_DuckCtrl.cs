using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Des_DuckCtrl : MonoBehaviour
{
    public float controlOverBoat = 5f;
    public int rotateSpeed = 50;
    public float maxSpeed = 7f;
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
        transform.Rotate(Vector3.up*rotateSpeed*2*Time.deltaTime*hAxis);
        moveVec = transform.forward;

        if(vAxis < -0.9)
            moveVec = -moveVec;

        rb.AddForce(moveVec * controlOverBoat * 0.1f);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
