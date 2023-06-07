using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputManager : MonoBehaviour
{
    public TextMeshProUGUI inputKeyboardShower;
    public TextMeshProUGUI inputMouseShower;
    WaitForSeconds mouseDisappear = new WaitForSeconds(2f);

    void Start()
    {
        inputKeyboardShower.text = " ";
        inputMouseShower.text = " ";
    }
    
    void Update()
    {
        if(Input.GetKey(KeyCode.S))
        {
            inputKeyboardShower.text = "S";
        }
        else if(Input.GetKey(KeyCode.A))
        {
            inputKeyboardShower.text = "A";
        }
        else if(Input.GetKey(KeyCode.D))
        {
            inputKeyboardShower.text = "D";
        }
        else
        {
            inputKeyboardShower.text = " ";
        }

        if(Input.GetMouseButtonUp(0))
        {

            StartCoroutine(ShowKeyBoard("Left Mouse"));
        }
        else if(Input.GetMouseButtonDown(1))
        {
            StartCoroutine(ShowKeyBoard("Right Mouse"));
        }
    }

    IEnumerator ShowKeyBoard(string s)
    {
        inputMouseShower.text = s;
        yield return mouseDisappear;
        inputMouseShower.text = " ";
    }
}
