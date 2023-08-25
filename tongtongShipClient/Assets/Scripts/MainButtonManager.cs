using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainButtonManager : MonoBehaviour
{
    public GameObject creditsUI;

    private void Start()
    {
        creditsUI.SetActive(false);
    }

    public void SinglePlayBtn()
    {
        SceneManager.LoadScene("SingleScene");
    }

    public void MultiPlayBtn()
    {
        SceneManager.LoadScene("MultiScene");
    }
    
    public void StoreBtn()
    {
        SceneManager.LoadScene("StoreScene");
    }

    public void StatisticsBtn()
    {
        
    }

    public void CreditsBtn()
    {
        creditsUI.SetActive(true);
    }

    public void ExitCredits()
    {
        creditsUI.SetActive(false);
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
