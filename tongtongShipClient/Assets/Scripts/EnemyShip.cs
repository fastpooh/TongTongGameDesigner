using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class EnemyShip : MonoBehaviour
{
    // State of enemy AI ship
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }

    // Variables about enemy ship
    private Transform tr;
    private NavMeshAgent agent;
    public Image enemyHPbar;
    public GameObject enemyBomb;
    private float enemyCountDown;
    public State state = State.IDLE;
    public Transform cannon;

    // Spec of enemy ship
    public int enemyMaxHp = 3;
    public int enemyHP;
    public float enemySpeed = 3.5f;
    public float coolTime = 3.0f;
    public float shootRange = 25.0f;
    public float power = 300f;
    public bool isDie = false;

    // Variables about player(Duck)
    private bool foundBoat;
    private GameObject playerDuck;
    private Transform playerTransform;
    public int duckHP;

    // is this boat a submarine?
    public bool isSubmarine = false;

    void Start()
    {
        foundBoat = false;
        StartCoroutine(FindDuckBoat());
    }

    void Update()
    {
        if(foundBoat)
        {
            UpdateDuckHp();
            MoveAttack();
            EnemyCoolTimeUpdate();
            UpdateEnemyHealthBar();
            CheckGameEnds();
        }
    }
    
    void UpdateDuckHp()
    {
        duckHP = playerDuck.GetComponent<DuckCtrl>().duckHp;
    }

    // Move and attack when game is going on
    void MoveAttack()
    {
        if(!isDie && duckHP > 0)
        {
            float distance = Vector3.Distance(playerTransform.position, tr.position);
            if(distance < shootRange && enemyCountDown == 0)
            {
                state = State.ATTACK;
                ShootBomb();
                agent.isStopped = true;
            }
            else
            {
                state = State.TRACE;
                agent.isStopped = false;
                agent.SetDestination(playerTransform.position);
            }
        }
    }

    // Update fire coll time for enemies
    void EnemyCoolTimeUpdate()
    {
        enemyCountDown = enemyCountDown - Time.deltaTime;
        if(enemyCountDown <= 0)
            enemyCountDown = 0;
    }

    // Fire Bomb
    void ShootBomb()
    {
        GameObject bomb1 = Instantiate(enemyBomb, cannon.position, cannon.rotation*Quaternion.Euler(0, 0, 0));
        Rigidbody rBody1 = bomb1.GetComponent<Rigidbody>();
        if(!isSubmarine)
            rBody1.AddForce(transform.forward * power);
        enemyCountDown = coolTime;
    }

    // Fill HP bar on the UI
    void UpdateEnemyHealthBar()
    {
        enemyHPbar.fillAmount = (float)enemyHP / (float)enemyMaxHp;
    }

    // Stops enemy ship when duck or enemy ship is dead
    void CheckGameEnds()
    {
        if(enemyHP <= 0)
        {
            state = State.DIE;
            isDie = true;
            agent.isStopped = true;
        }
        else if (duckHP <= 0)
        {
            state = State.IDLE;
            agent.isStopped = true;
        }
    }

    // Decrease HP if hit by duck bomb
    void OnTriggerEnter(Collider coll)
    {
        if(coll.CompareTag("BOMB") && enemyHP > 0)
            enemyHP--;
    }

    IEnumerator FindDuckBoat()
    {
        yield return new WaitForSeconds(0.01f);
        // Initialize player duck ship
        playerDuck = GameObject.FindWithTag("SHIP");
        playerTransform = playerDuck.transform;

        // Initialize enemy ship
        tr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = playerTransform.position;
        agent.speed = enemySpeed;
        enemyHP = enemyMaxHp;
        enemyCountDown = 0;
        foundBoat = true;
    }
}
