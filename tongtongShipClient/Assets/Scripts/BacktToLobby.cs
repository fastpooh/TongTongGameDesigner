using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktToLobby : MonoBehaviour
{
    public void MoveToLobby()
    {
        SceneManager.LoadScene("StartUI");
    }
}
