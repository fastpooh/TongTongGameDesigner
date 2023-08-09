using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;

public class EnemyShip_SS : MonoBehaviour
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
    private int maxEnemyHP;
    private int duckHP;

    public GameObject enemyBomb;
    public int enemyHP = 10;
    public float enemySpeed = 3.5f;
    public Image healthbar;
    public State state = State.IDLE;
    public float shootRange = 25.0f;
    public float coolTime = 3.0f;

    private bool isDead = false;
    private float enemyCoolTime;

    WaitForSeconds rest = new WaitForSeconds(0.3f);

    void Start()
    {
        tr = GetComponent<Transform>();
        playerDuck = GameObject.FindWithTag("SHIP");
        playerTransform = playerDuck.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = playerTransform.position;
        duckHP = GameObject.FindWithTag("SHIP").GetComponent<DuckCtrl_SS>().duckHP;
        maxEnemyHP = enemyHP;
        enemyCoolTime = coolTime;
        agent.speed = enemySpeed;
    }

    void Update()
    {
        UpdateHealthBar();
        enemyCoolTime = enemyCoolTime - Time.deltaTime;
        if(enemyCoolTime <= 0)
            enemyCoolTime = 0;
            
        if(enemyHP <= 0 || duckHP <=0)
        {
            state = State.DIE;
            isDead = true;
            agent.isStopped = true;
        }

        if(!isDead && duckHP > 0)
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

    void UpdateHealthBar() {
        healthbar.fillAmount = enemyHP / maxEnemyHP; 
    }

}
