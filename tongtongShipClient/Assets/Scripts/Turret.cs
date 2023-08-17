using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject enemyBomb;
    public float coolTime = 1.0f;
    private float enemyCoolTime;
    void Start()
    {
        enemyCoolTime = coolTime;
        
    }

    void Update()
    {
        enemyCoolTime = enemyCoolTime - Time.deltaTime;
        if (enemyCoolTime <= 0)
            enemyCoolTime = 0;
        if (enemyCoolTime == 0)
        {
            ShootBomb();
        }
    }
    void ShootBomb()
    {
        enemyCoolTime = coolTime;
        Instantiate(enemyBomb, transform.position, transform.rotation * Quaternion.Euler(0, 0, 0));
    }
}
