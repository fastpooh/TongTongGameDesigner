using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainButtonManager : MonoBehaviour
{
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
        
    }

    public void EndBtn()
    {
        Application.Quit();
    }
}
