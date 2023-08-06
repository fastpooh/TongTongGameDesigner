using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer_SS : MonoBehaviour
{
    public static Timer_SS Instance { set; get; }
    public bool isCool = false;
    private float cool;
    private float coolTime;
    public TextMeshProUGUI countDownText;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        coolTime = GetComponent<DuckAttack_SS>().coolTime;
        cool = coolTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager_SS.Instance.isPaused) {
            countDownText.enabled = false;
        } else {
            if(isCool == true) {
            cool -= Time.deltaTime;
            countDownText.text = cool.ToString("N0");
            } else {
                cool = coolTime;
                countDownText.enabled = false;
            }
        }
    }
}
