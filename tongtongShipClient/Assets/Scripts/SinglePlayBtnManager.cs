using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SinglePlayBtnManager : MonoBehaviour
{
    private int stageNum = 6;
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
    
    public void StageBtn()
    {
        string ButtonName = EventSystem.current.currentSelectedGameObject.name;
        ButtonName = ButtonName.Substring(5, 1);
        Debug.Log(ButtonName);
        SceneManager.LoadScene("Stage"+ButtonName);
    }
    /*
    public void Stage2Btn()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void Stage3Btn()
    {
        SceneManager.LoadScene("Stage3");
    }
    */
}
