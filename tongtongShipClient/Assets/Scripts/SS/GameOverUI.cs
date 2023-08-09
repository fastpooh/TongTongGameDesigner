using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI healthLeftText;
    private float healthLeft;
    public void SetUp() {
        gameObject.SetActive(true);
        healthLeft = GameObject.FindWithTag("ENEMYSHIP").GetComponent<EnemyShip_SS>().enemyHP;
        healthLeftText.text += healthLeft.ToString("N0");;
    }
}
