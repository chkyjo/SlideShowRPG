using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour{

    public GameObject characterManager;
    public GameObject deathPanel;

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
    Coroutine weatherCheck;

    int[] position = { 0, 0 };

    public Text roomStatusText;
    public int training = 0;

    int second;
    int minute;
    int hour;
    int totalMinutes;
    float timeScale;
    bool timePause = false;

    public int opponentID;

    int _currentRoom;

    private void Awake(){
        weather = 0;//5 different weathers: rain, snow, sunny, misty, windy
        combat = 0;
        temperature = 0;
        numCharactersInSetting = 0;
        startTime = -10;
        timeUntilWeatherChange = 0;
        _currentRoom = 6000;
        minute = 45;
        hour = 6;
        totalMinutes = 0;
        timeScale = 1;
        StartCoroutine(TimeUpdate());
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(WeatherTime());

        DisplayRoom();

        SetTone(1);

        SetTemp();
	}

    public void SetRoom(int roomID) {
        _currentRoom = roomID;
    }

    public int GetRoom(){
        return _currentRoom;
    }

    public int[] GetCoordinates() {
        return position;
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

    public string GetWeather(){
        if(weather == 0){
            return "rainy";
        }
        else if(weather == 1){
            return "snowy";
        }
        else if(weather == 2){
            return "sunny";
        }
        else if(weather == 3){
            return "misty";
        }
        else if(weather == 4){
            return "windy";
        }
        else{
            return "bleak";
        }
    }

    IEnumerator TimeUpdate(){
        PlayerManager pM = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();

        while(true){
            if (timePause == false){
                if(training == 0) {
                    ++second;
                }

                if (second >= 60){
                    second = 0;
                    minute++;
                    totalMinutes++;
                    if (minute >= 60) {
                        minute = 0;
                        hour++;
                        if (hour >= 24) {
                            hour = 0;
                        }
                    }
                    if (minute % 5 == 0) {
                        if (pM.GetCalories() == 0) {
                            pM.SubtractHealth(1);
                        }
                        else {
                            pM.AddHealth(1);
                        }
                    }
                    pM.SubtractCalories(1);
                }

                if (hour < 10){
                    timeStatus.text = "0" + hour.ToString();
                }
                else{
                    timeStatus.text = hour.ToString();
                }

                if (minute < 10){
                    timeStatus.text += ":0" + minute.ToString();
                }
                else{
                    timeStatus.text += ":" + minute.ToString();
                }

                if (second < 10){
                    timeStatus.text += ":0" + second.ToString();
                }
                else{
                    timeStatus.text += ":" + second.ToString();
                }
            }

            yield return new WaitForSeconds(timeScale);
        }
    }

    public int[] GetTime(){
        int[] time = new int[3] { hour, minute, second };

        return time;
    }

    IEnumerator WeatherTime(){

        while (true){

            if (Time.time - startTime > timeUntilWeatherChange){
                NewWeather();
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator SecondsTimer(int seconds) {
        for(int i = 0; i < seconds; i++) {
            GameObject.Find("MissionManager").GetComponent<MissionManager>().currentTime = i;
            yield return new WaitForSeconds(1f);
        }
        GameObject.Find("MissionManager").GetComponent<MissionManager>().currentTime = 10;
    }

    public void SetCharactersInSetting(Character[] characters){
        charactersInSetting.RemoveRange(0, charactersInSetting.Count);
        numCharactersInSetting = characters.Length;
        for (int i = 0; i < numCharactersInSetting; i++){
            charactersInSetting.Add(characters[i]);
        }
    }

    public void AddCharactersToSetting(Character[] characters){
        numCharactersInSetting = characters.Length;
        for(int i = 0; i < numCharactersInSetting; i++){
            charactersInSetting.Add(characters[i]);
        }
    }

    public void DisplayRoom() {
        Room room = GameObject.FindWithTag("RoomManager").GetComponent<RoomManager>().GetRoom(GetRoom());
        roomStatusText.text = "Room: " + room.GetName();
        position = room.GetRoomCoordinates();
    }

    public void UpdatePosition(int x, int y) {
        position[0] = x;
        position[1] = y;
    }

    public void SetTone(int tone){
        switch (tone){
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

    public int GetTotalMinutes() {
        return totalMinutes;
    }

    public void AddTime(int mins, int seconds){
        second += seconds;
        //minute += mins;
        PlayerManager pM = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();

        for(int i = 0; i < mins; i++) {
            minute++;
            totalMinutes++;
            if (minute % 5 == 0) {
                if (pM.GetCalories() > 0) {
                    if (pM.GetHealth() < 100) {
                        pM.AddHealth(1);
                    }
                }
                else {
                    if (pM.GetHealth() > 0) {
                        pM.SubtractHealth(1);
                    }
                }

                pM.UpdateHealth();
            }
        }

        if (pM.GetCalories() >= mins) {
            pM.SubtractCalories(mins);
            pM.UpdateCalories();
        }
        else {
            pM.SetCalories(0);
        }
        
        if(second >= 60) {
            second = 0;
            minute++;
            totalMinutes++;
        }

        if (minute >= 60) {
            minute -= 60;
            hour++;
            
            if (hour >= 24){
                hour = 0;
            }
        }

        if(minute % 5 == 0) {
            if (pM.GetCalories() > 0) {
                if(pM.GetHealth() < 100) {
                    pM.AddHealth(1);
                }
            }
            else {
                if(pM.GetHealth() > 0) {
                    pM.SubtractHealth(1);
                }
            }

            pM.UpdateHealth();
        }
    }

    public void TrainingTime() {
        training = 1;
    }

    public void SetTimeScale(float scale){
        timeScale = scale;
    }

}
