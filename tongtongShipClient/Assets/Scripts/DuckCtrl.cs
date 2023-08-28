using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DuckCtrl : MonoBehaviour
{
    // DuckAttack script
    DuckAttack duckAtk;

    // Move related variables
    private CharacterController controller;
    private Rigidbody rb;
    private new Transform transform;
    public Vector3 moveVec;

    // Control movement of boat
    private float controlOverBoat = 18f;  // With higher values, the boat follows your command better
    private int rotateSpeed = 50;        // With higher values, the boat turns faster     
    private float maxSpeed = 4.5f;         // The maximum speed of boat (boat accelerates from speed 0)
    
    // Spec of boat depending on number of people
    private float[] controlOverBoatList = {10f, 15f, 18f, 22f, 26f, 30f, 33f, 37f, 40f, 41f, 42f};
    private int[] rotateSpeedList = {30, 40, 50, 55, 65, 70, 75, 80, 85, 90, 95};
    private float[] maxSpeedList = {3f, 4f, 4.5f, 5f, 6f, 7f, 8f, 9f, 9.1f, 9.2f, 9.5f};

    // Health related variables
    public Image healthbar;
    public int maxHP = 10;
    public int duckHp;                      // might cause error because enemy ship is taking duck HP value in Start() function
    public bool isDead = false;

    // People related variables
    public int paddlers = 3;
    public int gunners = 2;
    private TextMeshProUGUI paddlerNumUI;
    private TextMeshProUGUI gunnerNumUI;

    // People getting on boat variables
    public float employDelay = 1.5f;
    private float employTimer = 0f;
    private Image fillImg;

    void Start()
    {
        // Set duck initially
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        duckHp = maxHP;
        healthbar = GameObject.Find("DuckHPbar").GetComponent<Image>();

        // Set number of paddlers/gunners UI
        paddlerNumUI = GameObject.Find("paddleNum").GetComponent<TextMeshProUGUI>();
        gunnerNumUI = GameObject.Find("bombNum").GetComponent<TextMeshProUGUI>();
        fillImg = GameObject.Find("FillOnBoard").GetComponent<Image>();
        fillImg.gameObject.SetActive(false);

        // Set DuckAtk script
        duckAtk = GetComponent<DuckAttack>();
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

        // Enter new people area
        if(coll.CompareTag("HARBOR"))
        {
            employTimer = 0;
            fillImg.gameObject.SetActive(true);
            StartCoroutine(DestroyFill());
        }

        if (coll.CompareTag("FIRE") && duckHp > 0)
        {
            duckHp--;

            if (duckHp <= 0)
                isDead = true;
        }

    }

    // When enough time passes near additional people, he gets aboard
    void OnTriggerStay(Collider coll)
    {
        if(coll.CompareTag("HARBOR"))
        {
            if(!fillImg.gameObject.activeSelf)
                fillImg.gameObject.SetActive(true);
            
            employTimer += Time.deltaTime;
            fillImg.fillAmount = employTimer/employDelay;
            if(employTimer >= employDelay)
            {
                paddlers++;
                Destroy(coll.gameObject);
                fillImg.gameObject.SetActive(false);
            }
            SetBoatSpecAndUI();
        }
    }

    // Give up new people from coming aboard
    void OnTriggerExit(Collider coll)
    {
        if(coll.CompareTag("HARBOR"))
        {
            fillImg.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("PirateSmall") && duckHp > 0)
        {
            paddlers--;
            SetBoatSpecAndUI();
        }
        if (coll.collider.CompareTag("PirateLarge") && duckHp > 0)
        {
            duckHp--;
            paddlers--;
            SetBoatSpecAndUI();
        }
        if (coll.collider.CompareTag("TurtleShip") && duckHp > 0)
        {
            duckHp--;
            SetBoatSpecAndUI();
        }

        if (duckHp <= 0)
            isDead = true;
    }


    // Button click events

    // Increase paddlers
    public void IncreasePad()
    {
        paddlers++;
        gunners--;
        duckAtk.SetGunnerCooltime();
        SetBoatSpecAndUI();
    }

    // Decrease paddlers
    public void IncreaseGun()
    {
        paddlers--;
        gunners++;
        duckAtk.SetGunnerCooltime();
        SetBoatSpecAndUI();
    }

    // Set boat spec
    public void SetBoatSpecAndUI()
    {
        controlOverBoat = controlOverBoatList[paddlers];
        rotateSpeed = rotateSpeedList[paddlers];
        maxSpeed = maxSpeedList[paddlers];

        paddlerNumUI.text = paddlers.ToString();
        gunnerNumUI.text = gunners.ToString();
    }

    IEnumerator DestroyFill()
    {
        yield return new WaitForSeconds(1.51f);
        if(fillImg.gameObject.activeSelf)
            fillImg.gameObject.SetActive(false);
    }
}
