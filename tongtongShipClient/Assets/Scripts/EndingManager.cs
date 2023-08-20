using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public GameObject success;
    public GameObject fail;

    public GameObject endingModal;

    public TextMeshProUGUI timer;

    public Text successTime;
    public Text successHP;
    public Text successCoin;

    public Text failTime;
    public Text failHP;
    public Text failCoin;

    public DuckCtrl player;
    // public GameObject playManager;

    private bool isPlaying;
    private bool isSuccess;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    public EnemyShip enemyScript1;
    public EnemyShip enemyScript2;
    public EnemyShip enemyScript3;

    // Start is called before the first frame update
    void Start()
    {
        endingModal.SetActive(false);
        success.SetActive(false);
        fail.SetActive(false);
        isPlaying = true;
        isSuccess = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isDead && isPlaying)
        {
            fail.SetActive(true);
            failTime.text = "격퇴 시간 - " + timer.text;
            failHP.text = "남은 체력 - " + player.duckHp.ToString()+"/"+player.maxHP.ToString();
            failCoin.text = "보상 : ";
            endingModal.SetActive(true);
            isPlaying = false;
        }

        if (enemy1.activeSelf && enemyScript1.isDie || !enemy1.activeSelf)
            if (enemy2.activeSelf && enemyScript2.isDie || !enemy2.activeSelf)
                if (enemy3.activeSelf && enemyScript3.isDie || !enemy3.activeSelf)
                    isSuccess = true;

        if (isSuccess && isPlaying)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            currentScene = currentScene.Substring(5);
            int n;
            if (!int.TryParse(currentScene, out n))
            {
                Debug.Log("Not Stage.");
            }
            PlayerPrefs.SetInt("SingleStage", n + 1);
            success.SetActive(true);
            successTime.text = "성공 시간 - " + timer.text;
            successHP.text = "남은 체력 - " + player.duckHp.ToString() + "/" + player.maxHP.ToString();
            successCoin.text = "보상 : " + (player.duckHp + 300 - int.Parse(timer.text.Substring(0, 2)) * 60 - int.Parse(timer.text.Substring(3, 2))).ToString();
            endingModal.SetActive(true);
            isPlaying = false;
        }

        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Out()
    {
        SceneManager.LoadScene("StartUI");
    }

    public void NextLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        currentScene = currentScene.Substring(5);
        int n;
        if(!int.TryParse(currentScene, out n))
        {
            Debug.Log("Not Stage.");
        }
        string nextScene = "Stage" + (n + 1).ToString();

        if (n < 6)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            SceneManager.LoadScene("StartUI");
        }
    }
}
