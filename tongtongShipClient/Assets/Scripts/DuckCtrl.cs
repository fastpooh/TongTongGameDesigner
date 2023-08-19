using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckCtrl : MonoBehaviour
{
    // Move related variables
    private CharacterController controller;
    private Rigidbody rb;
    private new Transform transform;
    public Vector3 moveVec;

    // Control movement of boat
    public float controlOverBoat = 5f;  // With higher values, the boat follows your command better
    public int rotateSpeed = 50;        // With higher values, the boat turns faster     
    public float maxSpeed = 7f;         // The maximum speed of boat (boat accelerates from speed 0)

    // Health related variables
    public Image healthbar;
    public int maxHP = 3;
    public int duckHp;                      // might cause error because enemy ship is taking duck HP value in Start() function
    public bool isDead = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        duckHp = maxHP;
        healthbar = GameObject.Find("DuckHPbar").GetComponent<Image>();
    }

    // Input : wasd
    float hAxis => Input.GetAxis("Horizontal");
    float vAxis => Input.GetAxis("Vertical");

    void Update()
    {
        MoveTurn();
        UpdateHealthBar();
    }

    void MoveTurn()
    {
        if (!isDead) // Only move and turn when boat is alive
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

    void UpdateHealthBar()
    {
        healthbar.fillAmount = (float)duckHp / (float)maxHP;
    }

    // Decrease HP if hit by an enemy bomb
    void OnTriggerEnter(Collider coll)
    {
        if(coll.CompareTag("ENEMYBOMB") && duckHp > 0)
        {
            duckHp--;

            if(duckHp <= 0)
                isDead = true;
        }
    }
}
