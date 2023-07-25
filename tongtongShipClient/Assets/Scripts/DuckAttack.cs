using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DuckAttack : MonoBehaviour
{
    private new Transform transform;
    public GameObject duckBomb;

    public float shootCooltime = 3;
    public TextMeshProUGUI timer;
    public GameObject cooltimeWarning;
    public Image coolTimeCircle;

    public int hp = 10;
    public TextMeshProUGUI hpBoard;

    WaitForSeconds warningDisappear = new WaitForSeconds(1f);
    

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        cooltimeWarning.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CoolTimeUpdate();
        Attack();
        ShowScoreBoard();
    }

    void Attack()
    {
        if((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && !isShootPossible())
        {
            StartCoroutine(CoolTimeWarning());
        }

        if(Input.GetMouseButtonDown(0) && isShootPossible())        // left click
        {
            Instantiate(duckBomb, transform.position, transform.rotation*Quaternion.Euler(0, -90f, 0));
            shootCooltime = 3f;
        }
        else if(Input.GetMouseButtonDown(1) && isShootPossible())        // right click
        {
            Instantiate(duckBomb, transform.position, transform.rotation*Quaternion.Euler(0, 90f, 0));
            shootCooltime = 3f;
        }
    }

    void CoolTimeUpdate()
    {
        if(shootCooltime > 0)
        {
            shootCooltime = shootCooltime - 1*Time.deltaTime;
            timer.text = ((int)shootCooltime + 1).ToString();
            coolTimeCircle.fillAmount = 1 - shootCooltime/3;
        }
        else if (shootCooltime <= 0)
        {
            shootCooltime = 0;
            coolTimeCircle.fillAmount = 1;
            timer.text = "Ready!";
        }
    }

    void ShowScoreBoard()
    {
        hpBoard.text = "HP : " + hp.ToString();
    }

    bool isShootPossible()
    {   
        if(shootCooltime == 0)
            return true;
        else
            return false;
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.CompareTag("ENEMYBOMB") && hp > 0)
        {
            hp--;
        }
    }

    IEnumerator CoolTimeWarning()
    {
        cooltimeWarning.SetActive(true);
        yield return warningDisappear;
        cooltimeWarning.SetActive(false);
    }
}
