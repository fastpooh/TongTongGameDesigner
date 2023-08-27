using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainButtonManager : MonoBehaviour
{
    public bool isUIOn;
    public GameObject blackBackGround;
    public GameObject multiplayUI;
    public GameObject statisticsUI;
    public GameObject creditsUI;

    private void Start()
    {
        creditsUI.SetActive(false);
        multiplayUI.SetActive(false);
        statisticsUI.SetActive(false);
        blackBackGround.SetActive(false);
        isUIOn = false;
    }

    // Move to another scene
    public void SinglePlayBtn()
    {
        if(!isUIOn)
            SceneManager.LoadScene("SingleScene");
    }
    
    public void StoreBtn()
    {
        if(!isUIOn)
            SceneManager.LoadScene("StoreScene");
    }

    // Show UI
    public void MultiPlayBtn()
    {
        if(!isUIOn)
        {
            multiplayUI.SetActive(true);
            blackBackGround.SetActive(true);
            isUIOn = true;
        }
        //SceneManager.LoadScene("MultiScene");
    }

    public void ExitMultiplay()
    {
        multiplayUI.SetActive(false);
        blackBackGround.SetActive(false);
        isUIOn = false;
    }

    public void StatisticsBtn()
    {
        if(!isUIOn)
        {
            statisticsUI.SetActive(true);
            blackBackGround.SetActive(true);
            isUIOn = true;
        }
    }

    public void ExitStatistice()
    {
        statisticsUI.SetActive(false);
        blackBackGround.SetActive(false);
        isUIOn = false;
    }

    public void CreditsBtn()
    {
        if(!isUIOn)
        {
            creditsUI.SetActive(true);
            blackBackGround.SetActive(true);
            isUIOn = true;
        }
    }

    public void ExitCredits()
    {
        creditsUI.SetActive(false);
        blackBackGround.SetActive(false);
        isUIOn = false;
    }

    public void EndBtn()
    {
        Application.Quit();
    }

    public void ResetBtn()
    {
        PlayerPrefs.DeleteAll();
    }
}
