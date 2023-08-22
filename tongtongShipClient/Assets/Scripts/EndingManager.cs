using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    // Sets of UI
    public GameObject success;
    public GameObject fail;
    public GameObject endingModal;

    // Game time related variables
    public TextMeshProUGUI timer;
    public Text successTime;
    public Text successHP;
    public Text successCoin;
    public Text failTime;
    public Text failHP;
    public Text failCoin;

    // My duck ship related variables
    private bool foundPlayer;
    private DuckCtrl player;
    private bool isPlaying;
    private bool isSuccess;


    // Enemy ship related variables       -> later change to list
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public EnemyShip enemyScript1;
    public EnemyShip enemyScript2;
    public EnemyShip enemyScript3;

    // Reward coin related variables
    private int coin;

    void Start()
    {
        // Find my Boat
        StartCoroutine(FindMyBoat());
        foundPlayer = false;

        // Initialize UI settings
        endingModal.SetActive(false);
        success.SetActive(false);
        fail.SetActive(false);

        // Success or Fail variables
        isPlaying = true;
        isSuccess = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(foundPlayer)
        {
            // When player is Dead
            if(player.isDead && isPlaying)
            {
                fail.SetActive(true);
                failTime.text = "Play Time : " + timer.text;
                failHP.text = "Current HP : " + player.duckHp.ToString()+"/"+player.maxHP.ToString();
                failCoin.text = "Reward : 5";
                AddCoins(5);
                endingModal.SetActive(true);
                isPlaying = false;
            }

            // When enemies are all dead -> change using for loop
            if (enemy1.activeSelf && enemyScript1.isDie || !enemy1.activeSelf)
                if (enemy2.activeSelf && enemyScript2.isDie || !enemy2.activeSelf)
                    if (enemy3.activeSelf && enemyScript3.isDie || !enemy3.activeSelf)
                        isSuccess = true;

            // Stage clear!
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
                successTime.text = "Play Time : " + timer.text;
                successHP.text = "Current HP : " + player.duckHp.ToString() + "/" + player.maxHP.ToString();
                successCoin.text = "Reward : " + (player.duckHp + 300 - int.Parse(timer.text.Substring(0, 2)) * 60 - int.Parse(timer.text.Substring(3, 2))).ToString();
                AddCoins(player.duckHp + 300 - int.Parse(timer.text.Substring(0, 2)) * 60 - int.Parse(timer.text.Substring(3, 2)));
                endingModal.SetActive(true);
                isPlaying = false;
            }
        }
    }

    // Button
    // Restart when game is over
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Exit game, leave to lobby
    public void Out()
    {
        SceneManager.LoadScene("StartUI");
    }

    // Move on to next level
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

    // Get coin as reward
    void AddCoins(int reward)
    {
        if (!PlayerPrefs.HasKey("Coin"))
        {
            PlayerPrefs.SetInt("Coin", 0);
        }
        coin = PlayerPrefs.GetInt("Coin");
        PlayerPrefs.SetInt("Coin", coin + reward);
    }

    // Find my boat
    IEnumerator FindMyBoat()
    {
        yield return new WaitForSeconds(0.4f);
        player = GameObject.FindWithTag("SHIP").GetComponent<DuckCtrl>();
        foundPlayer = true;
    }
}
