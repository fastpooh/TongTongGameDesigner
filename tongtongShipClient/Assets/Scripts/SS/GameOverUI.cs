using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI healthLeftText;
    private float healthLeft;
    // Start is called before the first frame update
    void Start()
    {
        healthLeft = GameObject.FindWithTag("ENEMYSHIP").GetComponent<EnemyShip_SS>().health;
        healthLeftText.text += healthLeft.ToString("N0");;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
