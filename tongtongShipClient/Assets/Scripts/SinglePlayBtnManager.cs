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
        // PlayerPrefs.SetInt("SingleStage", 1);
        // Get stage values
        if (!PlayerPrefs.HasKey("SingleStage"))
        {
            PlayerPrefs.SetInt("SingleStage", 1);
        }
        openStageNum = PlayerPrefs.GetInt("SingleStage");

        // Activate accessible stage start buttons
        for (int i = 0; i < openStageNum; i++)
        {
            enterButtons[i].interactable = true;
        }
    }

    void Start()
    {
        openStageNum = PlayerPrefs.GetInt("SingleStage");    // stage0 -> openStageNum = 1

        Text btnTxt;
        for(int i=0; i<stageNum; i++)
        {
            if(i < openStageNum - 1)
            {
                btnTxt = enterButtons[i].GetComponentInChildren<Text>();
                btnTxt.text = "Clear!\n";
            }
            else if (i == openStageNum - 1)
            {
                btnTxt = enterButtons[i].GetComponentInChildren<Text>();
                btnTxt.text = "Start!\n";
            }
            else
            {
                btnTxt = enterButtons[i].GetComponentInChildren<Text>();
                btnTxt.text = "Not Ready\n";
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
