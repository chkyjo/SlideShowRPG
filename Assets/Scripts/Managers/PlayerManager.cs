using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public Text gameNameTextObject;
    public string gameName;
    public int gender;
    public Text sliderValueStatus;
    public int age;
    int[] factions;
    

    int[] _followers;

    public struct CharacterInfo {
        public int characterID;
        //-1 if not known
        public int[,] _preferences; //lists of things they hate, dislike, are okay with, like and love
        public int[] _traits; //things that make them unique
        public int[] _goals; //things they want to accomplish
        public int[] _skills; //things they are good at
        public int _relationship; //how they feel about you
    }

    public List<CharacterInfo> knowledgeOfCharacters = new List<CharacterInfo>();

    Slider healthBar;
    Slider caloriesBar;

    int _health;
    int _calories;
    string playerName;
    int _swordSkill = 0;
    int _perception = 30;
    int _deserter = 0;
    int _agility = 30;

    public int[] position = {0,0};

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
        _health = 76;
        _calories = 284;
        _followers = new int[10] {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
    }
    


    public void GrabGameName() {
        gameName = gameNameTextObject.text;
    }

    public void GrabPlayerInfo() {
        gender = GameObject.Find("GenderDropdown").GetComponent<Dropdown>().value;
        age = (int)GameObject.Find("AgeSlider").GetComponent<Slider>().value;
        playerName = GameObject.Find("PlayerNameInputField").transform.GetChild(2).GetComponent<Text>().text;
    }

    public void DisplayAgeSelection() {
        sliderValueStatus.text = ((int)GameObject.Find("AgeSlider").GetComponent<Slider>().value).ToString();
    }

    public CharacterInfo GetInfoOn(int characterID) {
        for(int i = 0; i < knowledgeOfCharacters.Count; i++) {
            if(characterID == knowledgeOfCharacters[i].characterID) {
                return knowledgeOfCharacters[i];
            }
        }

        CharacterInfo charInfo = new CharacterInfo();
        charInfo.characterID = characterID;

        charInfo._preferences = new int[5,5];
        charInfo._traits = new int[5];
        charInfo._goals = new int[5];
        charInfo._skills = new int[5];

        return charInfo;
    }

    public void AddFollowers(int follower) {
        for(int i = 0; i < 10; i++) {
            if(_followers[i] == -1) {
                _followers[i] = follower;
                return;
            }
        }
    }
    public void RemoveFollower(int follower) {
        for (int i = 0; i < 10; i++) {
            if (_followers[i] == follower) {
                _followers[i] = -1;
                return;
            }
        }
    }
    public int[] GetFollowers() {
        return _followers;
    }

    public int GetHealth() {
        return _health;
    }
    public int GetCalories() {
        return _calories;
    }
    public void AddHealth(int addition) {
        _health += addition;
        if (_health > 100) {
            _health = 100;
        }
        
        GameObject.FindWithTag("HealthBar").GetComponent<Slider>().value = _health;
        
    }
    public void SubtractHealth(int subtract) {
        _health -= subtract;
        if(_health < 1) {
            GameObject.Find("GameCanvas").transform.GetChild(GameObject.Find("GameCanvas").transform.childCount -1).gameObject.SetActive(true);
        }
        else {
            GameObject.FindWithTag("HealthBar").GetComponent<Slider>().value = _health;
        }
    }
    public void SetCalories(int calories) {
        _calories = calories;
    }
    public void AddCalories(int cals) {
        _calories += cals;
        if(_calories > 1000) {
            _calories = 1000;
        }
        GameObject.FindWithTag("CaloriesBar").GetComponent<Slider>().value = _calories;
    }
    public void SubtractCalories(int cals) {
        _calories -= cals;
        if(_calories < 1) {
            _calories = 0;
        }
        GameObject.FindWithTag("CaloriesBar").GetComponent<Slider>().value = _calories;
    }

    public void UpdateCalories() {
        if (_calories < 0) {
            _calories = 0;
        }
        GameObject.FindWithTag("CaloriesBar").GetComponent<Slider>().value = _calories;
    }
    public void UpdateSwordSkill() {
        GameObject.FindWithTag("SwordSkillBar").GetComponent<Slider>().value = _swordSkill;
    }
    public void UpdateHealth() {
        if (_health < 0) {
            _health = 0;
        }
        GameObject.FindWithTag("HealthBar").GetComponent<Slider>().value = _health;
    }

    public string GetName() {
        return playerName;
    }

    public int GetSwordSkill() {
        return _swordSkill;
    }
    public void AddToSwordSkill(int add) {
        _swordSkill += add;
    }
    public void SetPerception(int perception) {
        _perception = perception;
    }
    public int GetPerception() {
        return _perception;
    }

    public void SetDeserterStatus(int status) {
        _deserter = status;
    }
    public int GetDeserterStatus() {
        return _deserter;
    }

    public void SetAgility(int agility) {
        _agility = agility;
    }
    public int GetAgility() {
        return _agility;
    }


    public void Restart() {
        _health = 76;
        _calories = 284;
    }

}
