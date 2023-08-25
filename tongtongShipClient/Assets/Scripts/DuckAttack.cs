using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DuckAttack : MonoBehaviour
{
    // DuckCtrl.cs
    private DuckCtrl duckInfo;

    // Variables related to shooting bombs
    public Transform cannonTransform;
    public GameObject bombPrefab;
    public int numberOfShootingBomb = 1;
    public float power = 300f;

    // Variables related to cooltime circle
    private GameObject coolTimePanel;
    private TextMeshProUGUI timer;
    private Image coolTimeCircle;
    [SerializeField] private float coolTime = 3f;
    private int gunner;
    public float[] coolTimeList = {8f, 6f, 5f, 4.5f, 3.5f, 3f, 2f, 1f};
    private float countDown;

    // Variables related to cooltime warning message
    private GameObject cooltimeWarning;
    WaitForSeconds warningDisappear = new WaitForSeconds(1f);

    void Start()
    {
        // Get DuckCtrl.cs
        duckInfo = GetComponent<DuckCtrl>();

        // Initialize cooltime circle variables
        coolTimePanel = GameObject.FindWithTag("COOLTIME");
        timer = coolTimePanel.transform.Find("CoolTimer").GetComponent<TextMeshProUGUI>();
        coolTimeCircle = coolTimePanel.transform.Find("CoolTimeCircle").GetComponent<Image>();
        cooltimeWarning = coolTimePanel.transform.Find("NotYetReady").GetComponent<Transform>().gameObject;
        SetGunnerCooltime();

        // Initialize cooltime warning variables
        cooltimeWarning.SetActive(false);
        countDown = 0;
    }

    void Update()
    {
        CoolTimeUpdate();
        Attack();
    }

    void Attack()
    {
        // Left click when cooltime did not pass
        if(Input.GetMouseButtonDown(1) && !isShootPossible())
        {
            StartCoroutine(CoolTimeWarning());
        }

        // Shoot one bomb when you left click 
        if(Input.GetMouseButtonDown(1) && isShootPossible() && numberOfShootingBomb == 1)
        {
            GameObject bomb1 = Instantiate(bombPrefab, cannonTransform.position, cannonTransform.rotation*Quaternion.Euler(0, 0, 0));
            Rigidbody rBody1 = bomb1.GetComponent<Rigidbody>();
            rBody1.AddForce(cannonTransform.right * power);
            countDown = coolTime;
        }
    }

    // Update cool time indicator UI
    void CoolTimeUpdate()
    {
        if(countDown > 0)
        {
            countDown = countDown - 1*Time.deltaTime;
            timer.text = ((int)countDown + 1).ToString();
            coolTimeCircle.fillAmount = 1 - countDown/coolTime;
        }
        else if (countDown <= 0)
        {
            countDown = 0;
            coolTimeCircle.fillAmount = 1;
            timer.text = "Ready!";
        }
    }

    // Check if shooting bomb is possible
    bool isShootPossible()
    {
        if(countDown <= 0)
            return true;
        else
            return false;
    }

    // Set cooltime using number of gunners from DuckCtrl.cs script
    public void SetGunnerCooltime()
    {
        gunner = duckInfo.gunners;
        coolTime = coolTimeList[gunner];
    }

    // Cooltime did not pass
    IEnumerator CoolTimeWarning()
    {
        cooltimeWarning.SetActive(true);
        yield return warningDisappear;
        cooltimeWarning.SetActive(false);
    }
}
