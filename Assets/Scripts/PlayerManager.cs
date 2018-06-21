using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public Text gameNameTextObject;
    public string gameName;
    public int gender;
    public Text sliderValueStatus;
    public int age;
    public int swordSkill = 0;

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
        gender = GameObject.Find("GenderDropdown").GetComponent<Dropdown>().value;
        age = (int)GameObject.Find("AgeSlider").GetComponent<Slider>().value;
        name = GameObject.Find("PlayerNameInputField").transform.GetChild(2).GetComponent<Text>().text;
    }

    public void DisplayAgeSelection() {
        sliderValueStatus.text = ((int)GameObject.Find("AgeSlider").GetComponent<Slider>().value).ToString();
    }

    public void UpdateCalories(){
        if(calories < 0) {
            calories = 0;
        }
        GameObject.FindWithTag("CaloriesBar").GetComponent<Slider>().value = calories;
    }
    public void UpdateSwordSkill() {
        GameObject.FindWithTag("SwordSkillBar").GetComponent<Slider>().value = swordSkill;
    }
    public void UpdateHealth(){
        if(health < 0) {
            health = 0;
        }
        GameObject.FindWithTag("HealthBar").GetComponent<Slider>().value = health;
    }

    public string GetName() {
        return name;
    }

    public void Restart() {
        health = 76;
        calories = 284;
    }

}
