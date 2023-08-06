using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_SS : MonoBehaviour
{
    public static GameManager_SS Instance;

    public GameObject gameOverUI;
    public GameObject stageClearUI;
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOver() {
        PauseGame();
        gameOverUI.SetActive(true);
    }
    public void StageClear() {
        PauseGame();
        stageClearUI.SetActive(true);
    }
    public void PauseGame() {
        isPaused = true;
    }
    
}

