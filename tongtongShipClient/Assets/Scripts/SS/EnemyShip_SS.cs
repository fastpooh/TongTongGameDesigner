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

    [SerializeField] private InGameUI inGameUI;
    [SerializeField] private GameManager_SS gameManager;
    public Image enemyHealthBar;
    public GameObject enemyBomb;
    public int enemyHP = 10;
    public float enemySpeed = 3.5f;
    public State state = State.IDLE;
    public float shootRange = 25.0f;
    public float coolTime = 3.0f;

    private float enemyCoolTime;

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager_SS>();
        tr = GetComponent<Transform>();
        playerDuck = GameObject.FindWithTag("SHIP");
        playerTransform = playerDuck.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = playerTransform.position;
        maxEnemyHP = enemyHP;
        enemyCoolTime = coolTime;
        agent.speed = enemySpeed;
    }

    void Update()
    {
        UpdateEnemyHealthBar((float) enemyHP/ (float) maxEnemyHP);
        enemyCoolTime = enemyCoolTime - Time.deltaTime;
        if(enemyCoolTime <= 0)
            enemyCoolTime = 0;
            
        if(enemyHP <= 0 && !gameManager.isOver)
        {
            state = State.DIE;
            gameManager.StageClear();
            Debug.Log("enemy died");
            agent.isStopped = true;
        }

        if(!gameManager.isPaused)
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

    void UpdateEnemyHealthBar(float fractionHP) {
        enemyHealthBar.fillAmount = fractionHP; 
    }
}
