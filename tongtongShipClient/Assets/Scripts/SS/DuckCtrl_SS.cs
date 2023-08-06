using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckCtrl_SS : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float health = 100f;
    private new Transform transform;
    private Rigidbody rBody;

    public bool isDead;
    // Start is called before the first frame update
    void Start() {
        transform = GetComponent<Transform>();
        rBody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update() {
        CheckHealth();
    }
    void FixedUpdate() {
        if(!GameManager_SS.Instance.isPaused) {
            rBody.velocity = transform.forward*moveSpeed;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
                rBody.AddTorque(Vector3.up * Input.GetAxis("Horizontal") * moveSpeed);
            }
            if (Input.GetKey(KeyCode.S)) {
                rBody.velocity = Vector3.zero;
            }
        } else {
            rBody.velocity = Vector3.zero;
        }
    }
    void CheckHealth() {
        if (health <= 0 && !isDead) {
            isDead = true;
            GameManager_SS.Instance.GameOver();
        }
    }
}
