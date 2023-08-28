using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using System.Data.Common;

public class BoatStore : MonoBehaviour
{
    // Coin related variables
    public int coin;
    public TextMeshProUGUI coinUI;

    // Owning Boat variables
    public List<bool> haveBoat = new List<bool> {true, false, false, false, false, false, false};
    private int[] boatPrice = {1, 10, 25, 40, 50, 75, 100};
    public int selectedBoat = 0;

    // UI lists
    public GameObject boatBtnContainer;

    // Selected Boat UI
    public Sprite selectedBoatUI;
    public Sprite notSelectedBoatUI;

    // Sound when you buy the boat
    public AudioClip boatBuySound;
    private AudioSource audioSource;

    void Awake()
    {
        // To start fresh
        // ClearSettings();
        // PlayerPrefs.SetInt("Coin", 1000);

        // Get coin values
        if (!PlayerPrefs.HasKey("Coin"))
        {
            PlayerPrefs.SetInt("Coin", 10);
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
    }

    void Start()
    {
        // Initialize sound
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = boatBuySound;
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }
    
    void Update()
    {
        // Show owing coins
        coinUI.text = coin.ToString() + " Coin";
    }

    // Save haveBoat list to database
    void SaveOwningBoat()
    {
        // Convert each bool to an int (true = 1, false = 0) and join them into a single comma-delimited string
        string delimitedString = string.Join(",", haveBoat.ConvertAll(b => b ? 1 : 0));
        PlayerPrefs.SetString("HaveBoat", delimitedString);
        PlayerPrefs.Save();
    }

    // Get haveBoat list from database
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

    // Express owning boat, selected boat and want-to-buy boats on UI
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
                {
                    boatBtn.text = "Selected";
                    Image buttonImage = boatBtn.transform.parent.gameObject.GetComponent<Image>();
                    buttonImage.sprite = selectedBoatUI;
                }
                else
                {
                    boatBtn.text = "Select";
                    Image buttonImage = boatBtn.transform.parent.gameObject.GetComponent<Image>();
                    buttonImage.sprite = notSelectedBoatUI;
                }
            }
            else
                boatBtn.text = boatPrice[index].ToString() + " Coins";

            index++;
            // Debug.Log("index : " + index.ToString());
        }
    }

    // Buy or select boat by clicking the button
    public void BuyOrSelectBoat()
    {
        // Parsing button number
        string ButtonName = EventSystem.current.currentSelectedGameObject.name;
        ButtonName = ButtonName.Substring(8, 1);
        int number = int.Parse(ButtonName) - 1;

        // Select boat if you have the boat
        if(haveBoat[number])
        {
            selectedBoat = number;
            PlayerPrefs.SetInt("SelectedBoat", selectedBoat);
            SetBoatButtons();
            // PlaySound();
        }

        // Buy boat if you don't have the boat
        if (coin >= boatPrice[number] && !haveBoat[number])
        {
            coin = coin - boatPrice[number];
            PlayerPrefs.SetInt("Coin", coin);

            haveBoat[number] = true;
            SaveOwningBoat();
            SetBoatButtons();
            PlaySound();
        }
    }

    // Play sound
    public void PlaySound()
    {
        audioSource.Play();
    }

    // Clear all settings
    public void ClearSettings()
    {
        PlayerPrefs.DeleteKey("Coin");
        PlayerPrefs.DeleteKey("HaveBoat");
        PlayerPrefs.DeleteKey("SelectedBoat");

        for(int i=0; i<boatPrice.Length; i++)
        {
            if(i == 0)
                haveBoat[i] = true;
            else
                haveBoat[i] = false;
        }

        SaveOwningBoat();
        LoadOwningBoat();
        SetBoatButtons();
    }
}
