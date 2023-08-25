using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayManager : MonoBehaviour
{
    // People generating variables
    public GameObject peopleSpawnPoint;
    public GameObject person;

    // Timer related variables
    public TextMeshProUGUI timer;
    private float time = 0;
    private int timeMin = 0;
    private int timeSec = 0;

    // People come out at this point of time (seconds)
    public float[] peopleAppearTime = {10, 20, 30, 40, 9999999};
    int idx = 0;

    // Where duck appears when game starts
    public Transform spawnPoint;
    public GameObject[] boatList;

    void Start()
    {
        // Set current Boat
        if (!PlayerPrefs.HasKey("SelectedBoat"))
        {
            PlayerPrefs.SetInt("SelectedBoat", 0);
        }
        int selectedBoatIdx = PlayerPrefs.GetInt("SelectedBoat");

        // Instantiate the boat you have
        Instantiate(boatList[selectedBoatIdx], spawnPoint.position, spawnPoint.rotation);
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

    // People generated at random point (among 3 points)
    void GeneratePerson()
    {
        Transform[] points = peopleSpawnPoint.GetComponentsInChildren<Transform>();
        int randomInt = Random.Range(1, 4);
        Debug.Log("randomNum : " + randomInt);
        Instantiate(person, points[randomInt].position, points[randomInt].rotation);
    }

    // Show time on UI
    void GameTimer()
    {
        time += Time.deltaTime;
        timeMin = (int)time / 60;
        timeSec = (int)time % 60;

        string minuate;
        string sec;
        if (timeMin < 10)
            minuate = "0" + timeMin.ToString() + ":";
        else
            minuate = timeMin.ToString() + ":";
        
        if(timeSec < 10)
            sec = "0" + timeSec.ToString();
        else
            sec = timeSec.ToString();

        timer.text = minuate + sec;
    }
}
