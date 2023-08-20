using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class BoatStore : MonoBehaviour
{
    // Coin related variables
    public int coin;
    public TextMeshProUGUI coinUI;

    // Owning Boat variables
    public List<bool> haveBoat = new List<bool> {true, false, false, false, false, false};
    public int[] boatPrice = {1, 3, 5, 8, 10, 12};
    public int selectedBoat = 0;

    // UI lists
    public GameObject boatBtnContainer;

    void Awake()
    {
        // Get coin values
        if (!PlayerPrefs.HasKey("Coin"))
        {
            PlayerPrefs.SetInt("Coin", 0);
        }
        coin = PlayerPrefs.GetInt("Coin");
        coinUI.text = coin.ToString() + " Coin";

        // Get list of owning ships
        if (!PlayerPrefs.HasKey("HaveBoat"))
            SaveOwningBoat();
        else
            LoadOwningBoat();

        // Set current Boat
        if (!PlayerPrefs.HasKey("SelectedBoat"))
        {
            PlayerPrefs.SetInt("SelectedBoat", 0);
        }
        selectedBoat = PlayerPrefs.GetInt("SelectedBoat");


        SetBoatButtons();

        /*
        for (int i = 1; i <= openStageNum; i++)
        {
            GameObject StageBtn = null;
            StageBtn = GameObject.Find("Stage" + i.ToString() + "Btn");
            StageBtn.GetComponent<Button>().interactable = true;
        }
        */
    }
    
    void Start()
    {
        coin = 100;
    }

    
    void Update()
    {
        coinUI.text = coin.ToString() + " Coin";
    }

    void SaveOwningBoat()
    {
        // Convert each bool to an int (true = 1, false = 0) and join them into a single comma-delimited string
        string delimitedString = string.Join(",", haveBoat.ConvertAll(b => b ? 1 : 0));
        PlayerPrefs.SetString("HaveBoat", delimitedString);
        PlayerPrefs.Save();
    }

    void LoadOwningBoat()
    {
        // Retrieve the comma-delimited string and split it into an array
        string delimitedString = PlayerPrefs.GetString("HaveBoat", "");
        string[] tokens = delimitedString.Split(',');
        haveBoat.Clear();

        // Convert each token back to a bool and add it to the list
        foreach (var token in tokens)
        {
            if (int.TryParse(token, out int result))
            {
                haveBoat.Add(result == 1);
            }
        }
    }

    void SetBoatButtons()
    {
        int index = 0;
        foreach (Transform boat in boatBtnContainer.transform)
        {
            TextMeshProUGUI boatBtn = boat.GetComponentInChildren<TextMeshProUGUI>();

            if (index >= haveBoat.Count)
                break;

            if(haveBoat[index])
            {
                if(index == selectedBoat)
                    boatBtn.text = "Selected";
                else
                    boatBtn.text = "Select";
            }
            else
                boatBtn.text = boatPrice[index].ToString() + " Coins";

            index++;
        }
    }

    public void BuyOrSelectBoat()
    {
        // Parsing button number
        string ButtonName = EventSystem.current.currentSelectedGameObject.name;
        ButtonName = ButtonName.Substring(8, 1);
        int number = int.Parse(ButtonName) - 1;
        // Debug.Log(number.ToString());

        // Select boat if you have the boat
        if(haveBoat[number])
        {
            selectedBoat = number;
            PlayerPrefs.SetInt("SelectedBoat", selectedBoat);
            SetBoatButtons();
        }

        // Buy boat if you don't have the boat
        if (coin >= boatPrice[number] && !haveBoat[number])
        {
            coin = coin - boatPrice[number];
            PlayerPrefs.SetInt("Coin", coin);

            haveBoat[number] = true;
            SaveOwningBoat();
            SetBoatButtons();
        }
    }
}
