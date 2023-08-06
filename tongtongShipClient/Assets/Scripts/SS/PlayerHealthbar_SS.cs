using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar_SS : MonoBehaviour
{
    private float maxHP;
    private GameObject player;
    private float HP;
    private Image healthbar;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("SHIP");
        maxHP = player.GetComponent<DuckCtrl_SS>().health;
        HP = maxHP;
        healthbar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        HP = player.GetComponent<DuckCtrl_SS>().health;
        healthbar.fillAmount = HP / maxHP; 
    }
}
