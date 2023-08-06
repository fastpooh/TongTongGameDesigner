using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar_SS : MonoBehaviour
{
    // Start is called before the first frame update
    private float maxEnemyHP;
    private GameObject enemy;
    private float HP;
    private Image healthbar;

    void Start()
    {
        enemy = GameObject.FindWithTag("ENEMYSHIP");
        maxEnemyHP = enemy.GetComponent<EnemyShip_SS>().health;
        HP = maxEnemyHP;
        healthbar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        HP = enemy.GetComponent<EnemyShip_SS>().health;
        healthbar.fillAmount = HP / maxEnemyHP; 
    }
}
