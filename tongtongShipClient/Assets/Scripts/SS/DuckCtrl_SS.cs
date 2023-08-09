using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckCtrl_SS : MonoBehaviour
{
    private new Transform transform;
    private Rigidbody rBody;
    private float maxHP;

    public int duckHP = 10;
    public float duckSpeed = 5f;
    public Image healthbar;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start() {
        transform = GetComponent<Transform>();
        rBody = GetComponent<Rigidbody>();
        maxHP = duckHP;
    }
    // Update is called once per frame
    void Update() {
        UpdateHealthBar();
        if (duckHP <= 0 && !isDead) {
            isDead = true;
        }
    }
    void FixedUpdate() {
        if(!isDead) {
            rBody.velocity = transform.forward * duckSpeed;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
                rBody.AddTorque(Vector3.up * Input.GetAxis("Horizontal") * duckSpeed);
            }
            if (Input.GetKey(KeyCode.S)) {
                rBody.velocity = Vector3.zero;
            }
        } else {
            rBody.velocity = Vector3.zero;
        }
    }
    void UpdateHealthBar() {
        healthbar.fillAmount = duckHP / maxHP;
    }
}
