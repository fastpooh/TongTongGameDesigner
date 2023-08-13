using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayManager : MonoBehaviour
{
    public GameObject peopleSpawnPoint;
    public GameObject person;

    public TextMeshProUGUI timer;
    private float time = 0;
    private int timeMin = 0;
    private int timeSec = 0;

    public float[] peopleAppearTime = {10, 20, 30, 40, 9999999};
    int idx = 0;

    void Start()
    {
        
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

    void GeneratePerson()
    {
        Transform[] points = peopleSpawnPoint.GetComponentsInChildren<Transform>();
        int randomInt = Random.Range(1, 4);
        Debug.Log("randomNum : " + randomInt);
        Instantiate(person, points[randomInt].position, points[randomInt].rotation);
    }

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
