using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageClearUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthLeftText;
    private float healthLeft;
    private GameManager_SS gameManager;
    public void SetUp()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager_SS>();
        gameObject.SetActive(true);
        healthLeft = GameObject.FindWithTag("SHIP").GetComponent<DuckCtrl_SS>().duckHP;
        healthLeftText.text += healthLeft.ToString("N0");
    }
}
