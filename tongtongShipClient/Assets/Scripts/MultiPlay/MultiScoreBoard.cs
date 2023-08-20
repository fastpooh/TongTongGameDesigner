using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MultiScoreBoard : MonoBehaviourPunCallbacks
{
    public MultiDuckCtrl player1;
    public MultiDuckCtrl player2;

    public GameObject exitUI;
    public TextMeshProUGUI winnerLog;

    public TextMeshProUGUI hp1;
    public TextMeshProUGUI hp2;


    void Start()
    {
        player1 = GameObject.FindWithTag("Player1").GetComponent<MultiDuckCtrl>();
        player2 = GameObject.FindWithTag("Player2").GetComponent<MultiDuckCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        hp1.text = "HP : " + player1.duckHp;
        hp2.text = "HP : " + player2.duckHp;

        if(player1.duckHp == 0)
        {
            exitUI.SetActive(true);
            winnerLog.text = "Player2 Wins";
        }
        else if(player2.duckHp == 0)
        {
            exitUI.SetActive(true);
            winnerLog.text = "Player1 Wins";
        }
        
    }

    public void OnExitClick()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("StartUI");
    }
}
