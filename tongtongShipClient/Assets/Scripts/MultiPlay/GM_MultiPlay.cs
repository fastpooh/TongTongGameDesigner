using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GM_MultiPlay : MonoBehaviourPunCallbacks
{
    void Awake()
    {
        CreatePlayer();
    }

    void CreatePlayer()
    {
        foreach(var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{player.Value.NickName}, {player.Value.ActorNumber}");
        }
        Transform[] points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
            PhotonNetwork.Instantiate("MultiRubberDuck1", points[1].position, points[1].rotation, 0);
        else if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
            PhotonNetwork.Instantiate("MultiRubberDuck2", points[2].position, points[2].rotation, 0);
        else
            Debug.LogError("Error!");
    }

    public float time = 0;

    void Update() 
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2) 
        {
            time += Time.deltaTime;
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