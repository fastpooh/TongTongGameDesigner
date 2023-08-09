using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageClearUI : MonoBehaviour
{
    public TextMeshProUGUI healthLeftText;
    private float healthLeft;
    public void SetUp()
    {
        gameObject.SetActive(true);
        healthLeft = GameObject.FindWithTag("SHIP").GetComponent<DuckCtrl_SS>().duckHP;
        healthLeftText.text += healthLeft.ToString("N0");;
    }
}
