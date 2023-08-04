using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SinglePlayBtnManager : MonoBehaviour
{
    private int stageNum = 3;
    private int openStageNum = 1;

    public GameObject Stages;


    void Awake()
    {
        if (!PlayerPrefs.HasKey("SingleStage"))
        {
            PlayerPrefs.SetInt("SingleStage", 1); 
        }
        openStageNum = PlayerPrefs.GetInt("SingleStage");
        for (int i = 1; i <= openStageNum; i++)
        {
            GameObject StageBtn = null;
            StageBtn = GameObject.Find("Stage" + i.ToString() + "Btn");
            StageBtn.GetComponent<Button>().interactable = true;
        }
    }
    
    public void Stage1Btn()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void Stage2Btn()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void Stage3Btn()
    {
        SceneManager.LoadScene("Stage3");
    }
}
