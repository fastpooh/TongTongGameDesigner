using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class PirateShip : MonoBehaviour
{
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }
    private Transform tr;
    private GameObject playerDuck;
    private Transform playerTransform;
    private NavMeshAgent agent;
    public int enemyHP = 10;
    private int duckHP;
    public TextMeshProUGUI enemyHPUI;
    public GameObject enemyBomb;
    public float enemySpeed = 3.5f;

    public State state = State.IDLE;
    private bool isDie = false;
    public float shootRange = 25.0f;
    public float coolTime = 3.0f;
    private float enemyCoolTime;

    WaitForSeconds rest = new WaitForSeconds(0.3f);

    void Start()
    {
        tr = GetComponent<Transform>();
        playerDuck = GameObject.Find("RubberDuck");
        playerTransform = playerDuck.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = playerTransform.position;
        duckHP = GameObject.Find("RubberDuck").GetComponent<DuckAttack>().hp;

        enemyCoolTime = coolTime;
        agent.speed = enemySpeed;
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.CompareTag("BOMB") && enemyHP > 0)
            enemyHP--;
    }

    void Update()
    {
        ShowHpOnScoreBoard();
        enemyCoolTime = enemyCoolTime - Time.deltaTime;
        if(enemyCoolTime <= 0)
            enemyCoolTime = 0;
            
        if(enemyHP <= 0 || duckHP <=0)
        {
            state = State.DIE;
            isDie = true;
            agent.isStopped = true;
        }

        if(!isDie && duckHP > 0)
        {
            float distance = Vector3.Distance(playerTransform.position, tr.position);
            if(distance < shootRange && enemyCoolTime == 0)
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
    void ShootBomb()
    {
        enemyCoolTime = coolTime;
        Instantiate(enemyBomb, transform.position, transform.rotation*Quaternion.Euler(0, 0, 0));
    }

    void ShowHpOnScoreBoard()
    {
        enemyHPUI.text = "HP : " + enemyHP.ToString();
    }

}
