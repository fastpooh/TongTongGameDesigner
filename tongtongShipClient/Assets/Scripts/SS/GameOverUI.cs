using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthLeftText;
    private float healthLeft;
    private GameManager_SS gameManager;
    public void SetUp() {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager_SS>();
        gameObject.SetActive(true);
        healthLeft = GameObject.FindWithTag("ENEMYSHIP").GetComponent<EnemyShip_SS>().enemyHP;
        healthLeftText.text += healthLeft.ToString("N0");;
    }
}
