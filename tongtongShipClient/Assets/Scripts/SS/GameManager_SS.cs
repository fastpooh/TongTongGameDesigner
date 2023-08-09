using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_SS : MonoBehaviour
{
    public GameOverUI gameOverUI;
    public StageClearUI stageClearUI;
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOver() {
        PauseGame();
        gameOverUI.SetUp();
    }
    public void StageClear() {
        PauseGame();
        stageClearUI.SetUp();
    }
    public void PauseGame() {
        isPaused = true;
    }
    
}

