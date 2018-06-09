using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour{

    public GameObject characterManager;

    public List<Character> charactersInSetting = new List<Character>();
    public Text weatherStatus;
    public Text toneStatus;
    public int numCharactersInSetting;
    public int weather;
    public float startTime;
    public float timeUntilWeatherChange;
    public int temperature;
    public int combat;
    Coroutine weatherCheck;

    private void Awake(){
        weather = 0;//5 different weathers: rain, snow, sunny, misty, windy
        combat = 1;
        temperature = 40;
        numCharactersInSetting = 20;
        startTime = -10;
        timeUntilWeatherChange = 0;
    }

    // Use this for initialization
    void Start () {

        StartCoroutine(WeatherTime());

        AddCharactersToSetting();

        SetTone();

	}
	
	// Update is called once per frame
	void Update (){
		
	}

    public void NewWeather(){
        //StopCoroutine(weatherCheck);
        weather = UnityEngine.Random.Range(0, 5);
        
        switch (weather){
            case 0:
                weatherStatus.text = "Weather: Rainy";
                break;

            case 1:
                weatherStatus.text = "Weather: Snowy";
                break;

            case 2:
                weatherStatus.text = "Weather: Sunny";
                break;

            case 3:
                weatherStatus.text = "Weather: Misty";
                break;

            case 4:
                weatherStatus.text = "Weather: Windy";
                break;
        }

        timeUntilWeatherChange = UnityEngine.Random.Range(100, 500);
        startTime = Time.time;
    }

    IEnumerator WeatherTime(){

        while (true){
            Debug.Log("Time.time = " + Time.time + ", startTime = " + startTime + ", timeUntilWeatherChange = " + timeUntilWeatherChange);

            if (Time.time - startTime > timeUntilWeatherChange){
                NewWeather();
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void AddCharactersToSetting(){
        for(int i = 0; i < numCharactersInSetting; i++){

            charactersInSetting.Add(characterManager.GetComponent<CharactersManager>().GetRandomCharacter());

        }
    }

    public void SetTone(){
        toneStatus.text = "Tone: In Combat";
    }
}
