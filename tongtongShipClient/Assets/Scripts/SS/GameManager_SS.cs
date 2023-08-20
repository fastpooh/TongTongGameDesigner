using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
public class GameManager_SS : MonoBehaviour
{
    [SerializeField] GameOverUI gameOverUI;
    [SerializeField] StageClearUI stageClearUI;
    public int level = 1;
    public bool isOver { get; private set; }
    public bool isPaused { get; private set; }

    public GameObject peopleSpawnPoint;
    public GameObject person;
    public GameObject shipHarbors;
    public GameObject harbor;

    // Timer related variables
    public TextMeshProUGUI timer;
    private float time = 0;
    private int timeMin = 0;
    private int timeSec = 0;

    // People come out at this point of time (seconds)
    public float[] peopleAppearTime = {10, 20, 30, 40, 9999999};
    int idx = 0;

    // Start is called before the first frame update
    void Start()
    {
        isOver = false;
        isPaused = false;
    }

    void Update()
    {
        GameTimer();
        if(peopleAppearTime[idx] <= time)
        {
            GeneratePerson();
            idx++;
        }
    }

    public void GameOver() {
        if(!isOver) {
            PauseGame();
            isOver = true;
            gameOverUI.SetUp();
        } else {
            Debug.Log("game is already over");
        }
    }
    public void StageClear() {
        if(!isOver) {
            PauseGame();
            isOver = true;
            stageClearUI.SetUp();
        } else {
            Debug.Log("stage is already cleared");
        }
    }
    public void PauseGame() {
        isPaused = true;
    }
    private void LoadLevel(int level) {
        this.level = level;
        SceneManager.LoadScene($"Stage{level}");
    }
    public void NextLevel() {
        LoadLevel(level+1);
    }
    public void RestartLevel() {
        LoadLevel(level);
    }


    // People generated at random point (among 3 points)
    void GeneratePerson()
    {
        Transform[] points = peopleSpawnPoint.GetComponentsInChildren<Transform>();
        Transform[] harbors = shipHarbors.GetComponentsInChildren<Transform>();
        int randomInt = Random.Range(1, 4);
        Debug.Log("randomNum : " + randomInt);
        GameObject personStanding = Instantiate(person, points[randomInt].position, points[randomInt].rotation);
        GameObject parkingSpot = Instantiate(harbor, harbors[randomInt].position, Quaternion.Euler(0, 0, 0));
    }
    // Show time on UI
    void GameTimer()
    {
        time += Time.deltaTime;
        timeMin = (int)time / 60;
        timeSec = (int)time % 60;

        string minute;
        string sec;
        if (timeMin < 10)
            minute = "0" + timeMin.ToString() + ":";
        else
            minute = timeMin.ToString() + ":";
        
        if(timeSec < 10)
            sec = "0" + timeSec.ToString();
        else
            sec = timeSec.ToString();

        timer.text = minute + sec;
    }
}


