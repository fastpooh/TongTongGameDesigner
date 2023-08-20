using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DuckCtrl_SS : MonoBehaviour
{
    private new Transform transform;
    private Rigidbody rBody;
    [SerializeField] private GameManager_SS gameManager;
    [SerializeField] private InGameUI inGameUI;
    [SerializeField] private int maxHP = 200;
    [SerializeField] private float employDelay = 1.5f;
    private float employTimer = 0f;
    public bool employNew = false;
    
    public int paddlers = 3;
    public int gunners = 2;

    public int duckHP;
    public float controlOverBoat = 2f;
    public float duckSpeed = 7f;
    public float duckRotateSpeed = 2f;

    void Start() {
        transform = GetComponent<Transform>();
        rBody = GetComponent<Rigidbody>();
        rBody.angularDrag = 1f;
        duckHP = maxHP;
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager_SS>();
    }

    void Update() {
        if(gameManager.isPaused) {
            GetComponent<DuckAttack_SS>().enabled = false;
        } else {
            GetComponent<DuckAttack_SS>().enabled = true;
            inGameUI.UpdateDuckHealthBar(duckHP, maxHP);
        }
        if (duckHP <= 0 && !gameManager.isOver) {
            gameManager.GameOver();
        }
    }
    void FixedUpdate() {
        if(!gameManager.isPaused) {
            rBody.velocity = transform.forward * duckSpeed;
            rBody.AddForce(transform.forward * controlOverBoat * 0.2f);
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
                rBody.AddTorque(Vector3.up * Input.GetAxis("Horizontal") * duckRotateSpeed);
            }
            if (Input.GetKey(KeyCode.S)) {
                rBody.velocity = Vector3.zero;
            }
        } else {
            rBody.velocity = Vector3.zero;
        }
    }

    void changePad(int change) {
        paddlers += change;
        duckSpeed += change/2;
        duckRotateSpeed += change/2;
        controlOverBoat += change/2;
        inGameUI.UpdateNumPad(paddlers);
    }

    void changeGun(int change) {
        gunners += change;
        GetComponent<DuckAttack_SS>().coolTime += change/2;
        inGameUI.UpdateNumGun(gunners);
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "HARBOR") {
            employTimer = 0;
            Debug.Log("collision");
            inGameUI.DisplayNewCrewLoadingBar(true);
        } else if (col.gameObject.tag == "WALL" || col.gameObject.tag == "ENEMYSHIP") {
            rBody.AddForce(-transform.forward * 100);
        }
    }

    void OnTriggerStay(Collider col) {
        if(col.gameObject.tag == "HARBOR") {
            employTimer += Time.deltaTime;
            inGameUI.UpdateNewCrewLoadingBar((float)employTimer/employDelay);
            if(employTimer >= employDelay) {
                changePad(1);
                Destroy(col.gameObject);
                inGameUI.DisplayNewCrewLoadingBar(false);
            }
        } else if (col.gameObject.tag == "WALL" || col.gameObject.tag == "ENEMYSHIP") {
            Debug.Log("Stuck!");
        }
    }

    void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "HARBOR") {
            inGameUI.DisplayNewCrewLoadingBar(false);
        }
    }

    public void increasePad() {
        Debug.Log("Increase num paddlers");
        if(gunners > 0) {
            changePad(1);
            changeGun(-1);
        }
    }
    public void increaseGun() {
        if(paddlers > 0) {
            changePad(-1);
            changeGun(1);
        }
    }
}
