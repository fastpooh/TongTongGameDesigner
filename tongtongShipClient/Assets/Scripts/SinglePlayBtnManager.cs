using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SinglePlayBtnManager : MonoBehaviour
{
    // public GameObject Stages;
    public int stageNum = 9;  // total number of stages
    private int openStageNum;

    // By clicking these buttons, you enter that stage
    public Button[] enterButtons;

    void Awake()
    {
        // Get stage values
        if (!PlayerPrefs.HasKey("SingleStage"))
        {
            PlayerPrefs.SetInt("SingleStage", 1); 
        }
        openStageNum = PlayerPrefs.GetInt("SingleStage");

        // Activate accessible stage start buttons
        for (int i = 1; i <= openStageNum; i++)
        {
            GameObject StageBtn = null;
            StageBtn = GameObject.Find("Stage" + i.ToString() + "Btn");
            StageBtn.GetComponent<Button>().interactable = true;
        }
    }

    void Start()
    {
        openStageNum = PlayerPrefs.GetInt("SingleStage");    // stage0 -> openStageNum = 1

        Text btnTxt;
        for(int i=0; i<stageNum; i++)
        {
            if(i < openStageNum)
            {
                btnTxt = enterButtons[i].GetComponentInChildren<Text>();
                btnTxt.text = "Clear!\n";
            }
            else
            {
                btnTxt = enterButtons[i].GetComponentInChildren<Text>();
                btnTxt.text = "Start!\n";
            }

        }
    }

    
    // Load Stage n scene by clicking button
    public void StageBtn()
    {
        string ButtonName = EventSystem.current.currentSelectedGameObject.name;
        ButtonName = ButtonName.Substring(5, 1);
        Debug.Log(ButtonName);
        SceneManager.LoadScene("Stage"+ButtonName);
    }
}
