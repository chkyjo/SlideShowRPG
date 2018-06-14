using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public Text gameNameTextObject;
    public string gameName;
    public Dropdown genderDropDown;
    public int gender;
    public Slider ageSlider;
    public Text sliderValueStatus;
    public int age;
    public Text playerName;

    Slider healthBar;
    Slider caloriesBar;

    public int health;
    public int calories;
    public string name;

    public static PlayerManager Instance;

    void Awake() {
        
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }

        gender = 1;
        age = 9;
        health = 76;
        calories = 284;
    }
    

	// Use this for initialization
	void Start () {
        
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GrabGameName() {
        gameName = gameNameTextObject.text;
    }

    public void GrabPlayerInfo() {
        gender = genderDropDown.value;
        age = (int)ageSlider.value;
        name = playerName.text;
    }

    public void GrabHealthBars() {
        healthBar = GameObject.FindWithTag("HealthBar").GetComponent<Slider>();
        caloriesBar = GameObject.FindWithTag("CaloriesBar").GetComponent<Slider>();
    }

    public void DisplayAgeSelection() {
        sliderValueStatus.text = ((int)ageSlider.value).ToString();
    }

    public void UpdateCalories(){
        
        caloriesBar.value = calories;
    }
    public void UpdateHealth(){
        healthBar.value = health;
    }

    public string GetName() {
        return name;
    }

}
