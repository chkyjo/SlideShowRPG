using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour{

    public GameObject characterManager;

    public List<Character> charactersInSetting = new List<Character>();
    public Text weatherStatus;
    public Text toneStatus;
    public Text tempStatus;
    public Text timeStatus;
    public int numCharactersInSetting = 0;
    public int weather;
    public float startTime;
    public float timeUntilWeatherChange;
    public int temperature;
    public int combat;
    public int roomObserved;
    Coroutine weatherCheck;

    public Slider slider;
    public Text sliderValueStatus;

    int second;
    int minute;
    int hour;

    public int currentRoom;

    private void Awake(){
        weather = 0;//5 different weathers: rain, snow, sunny, misty, windy
        combat = 0;
        temperature = 0;
        numCharactersInSetting = 0;
        startTime = -10;
        timeUntilWeatherChange = 0;
        currentRoom = 0;
        minute = 44;
        hour = 6;
        roomObserved = 0;
        StartCoroutine(TimeUpdate());
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(WeatherTime());

        SetTone(1);

        SetTemp();
	}

    // Update is called once per frame
    void Update() {

        
	}

    public int GetRoom(){
        return currentRoom;
    }

    public void NewWeather(){
        //StopCoroutine(weatherCheck);
        weather = UnityEngine.Random.Range(0, 5);
        
        switch (weather){
            case 0:
                weatherStatus.text = "Weather: Rainy";
                temperature = 50 + UnityEngine.Random.Range(-5, 6);
                break;

            case 1:
                weatherStatus.text = "Weather: Snowy";
                temperature = 20 + UnityEngine.Random.Range(-5, 6);
                break;

            case 2:
                weatherStatus.text = "Weather: Sunny";
                temperature = 60 + UnityEngine.Random.Range(-5, 6);
                break;

            case 3:
                weatherStatus.text = "Weather: Misty";
                temperature = 40 + UnityEngine.Random.Range(-5, 6);
                break;

            case 4:
                weatherStatus.text = "Weather: Windy";
                temperature = 30 + UnityEngine.Random.Range(-5, 6);
                break;
        }

        timeUntilWeatherChange = UnityEngine.Random.Range(100, 500);
        startTime = Time.time;
    }

    IEnumerator TimeUpdate(){
        while(true){
            second = (int)Time.time;
            second %= 60;
            if (second == 0){
                minute++;
            }

            if(minute >= 60){
                minute = 0;
                hour++;
            }

            if(hour >= 24){
                hour = 0;
            }

            if(hour < 10){
                timeStatus.text = "0" + hour.ToString();
            }
            else{
                timeStatus.text = hour.ToString();
            }

            if(minute < 10){
                timeStatus.text += ":0" + minute.ToString();
            }
            else{
                timeStatus.text += ":" + minute.ToString();
            }

            if(second < 10){
                timeStatus.text += ":0" + second.ToString();
            }
            else{
                timeStatus.text += ":" + second.ToString();
            }

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator WeatherTime(){

        while (true){

            if (Time.time - startTime > timeUntilWeatherChange){
                NewWeather();
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void AddCharactersToSetting(Character[] characters){
        numCharactersInSetting = characters.Length;
        for(int i = 0; i < numCharactersInSetting; i++){

            charactersInSetting.Add(characters[i]);

        }
    }

    public void SetTone(int tone){

        switch (tone)
        {
            case 0:
                toneStatus.text = "Tone: In Combat";
                break;
            case 1:
                toneStatus.text = "Tone: Exploring";
                break;
            case 2:
                toneStatus.text = "Tone: Conversing";
                break;

        }
    }

    public void SetTemp(){
        tempStatus.text = temperature.ToString() + " degrees";
    }

    public void AddTime(int mins){
        minute += mins;

        if(minute >= 60){
            minute -= 60;
            hour++;
            if(hour >= 24){
                hour = 0;
            }
        }

        slider.value = 0;
    }

    public void UpdateSliderUI(){

        sliderValueStatus.text = ((int)slider.value).ToString() + " minutes";

    }
}
