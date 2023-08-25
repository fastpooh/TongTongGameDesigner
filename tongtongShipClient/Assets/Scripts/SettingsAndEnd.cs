using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsAndEnd : MonoBehaviour
{
    public GameObject settingModal;
    
    // Start is called before the first frame update
    void Start()
    {
        settingModal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingModal.SetActive(!settingModal.activeSelf);
        }
    }

    public void SettingOn()
    {
        settingModal.SetActive(true);
    }

    public void SettingOff()
    {
        settingModal.SetActive(false);
    }
    
    public void GoHome()
    {
        SceneManager.LoadScene("StartUI");
    }

    public void GoStage()
    {
        SceneManager.LoadScene("SingleScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
