using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InGameUI : MonoBehaviour
{
    // [SerializeField] private GameObject duck;
    // [SerializeField] private GameObject enemy;
    [SerializeField] private Image duckHealthBar;
    [SerializeField] private TextMeshProUGUI duckHealthText;
    [SerializeField] private Image enemyHealthBar;
    [SerializeField] private Image newCrewLoadingBar;
    [SerializeField] private TextMeshProUGUI coolTimer;
    [SerializeField] private TextMeshProUGUI coolTimeWarning;
    [SerializeField] private Image coolTimeImage;
    [SerializeField] private TextMeshProUGUI numPaddler;
    [SerializeField] private TextMeshProUGUI numGunner;
    private float displayTime;
    
    void Start()
    {
        newCrewLoadingBar.transform.parent.gameObject.SetActive(false);
        coolTimeImage.enabled = false;
        coolTimer.enabled = false;
        coolTimeWarning.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayNewCrewLoadingBar(bool display) {
        if(display) {
            newCrewLoadingBar.transform.parent.gameObject.SetActive(true);
        } else {
            newCrewLoadingBar.transform.parent.gameObject.SetActive(false);
        }
    }
    public void UpdateDuckHealthBar(int duckHP, int maxHP) {
        float fractionHP = (float) duckHP / (float) maxHP;
        duckHealthText.text = duckHP.ToString() + " / " + maxHP.ToString();
        duckHealthBar.fillAmount = fractionHP;
    }
    public void UpdateNewCrewLoadingBar(float fractionTime) {
        newCrewLoadingBar.fillAmount = fractionTime;
    }
    public void UpdateNumPad(int paddlers) {
        numPaddler.text = paddlers.ToString();
    }
    public void UpdateNumGun(int gunners) {
        numGunner.text = gunners.ToString();
    }

    public IEnumerator StartCountDown(float cool) {
        coolTimer.enabled = true;
        coolTimeImage.enabled = true;
        displayTime = cool;
        while( displayTime > 0 ) {
            coolTimer.text = displayTime.ToString("N0");
            coolTimeImage.fillAmount = 1 - displayTime/cool;
            yield return new WaitForSeconds(1);
            displayTime -= 1;
        }
        coolTimeImage.fillAmount = 1;
        coolTimer.text = "Ready!";
        yield return new WaitForSeconds(1);
        coolTimer.enabled = false;
        coolTimeImage.enabled = false;
    }

    public IEnumerator ShowCoolWarning()
    {
        coolTimeWarning.enabled = true;
        yield return new WaitForSeconds(1);
        coolTimeWarning.enabled = false;
    }

}
