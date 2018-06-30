using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CharactersManager : MonoBehaviour {

    List<Character> completeListOfCharacters = new List<Character>();
    List<Character> scriptedCharacters = new List<Character>();

    public GameObject characterInfoPanel;
    public Slider relationshipSlider;

    public TextAsset maleFirstNameListTextFile;
    public TextAsset femaleFirstNameListTextFile;
    public TextAsset lastNameListTextFile;
    public TextAsset traitsListTextFile;
    public TextAsset skillsListTextFile;
    public TextAsset goalsListTextFile;
    public TextAsset relationshipsListTextFile;

    public TextAsset scriptedCharacterNamesText;
    public TextAsset scriptedCharacterTraits;

    string[] completeListOfMaleFirstNames = new string[1000];
    string[] completeListOfFemaleFirstNames = new string[1000];
    string[] completeListOfLastNames = new string[1000];
    string[] completeListOfTraits = new string[1000];
    string[] completeListOfGoals = new string[1000];
    string[] completeListOfSkills = new string[1000];
    int[][] completeListOfRelationships = new int[1000][];

    string firstName;
    string lastName;
    int gender;
    int age;
    int height;
    int weight;
    int[] traits;
    int[] goals;
    int[] skills;
    int[] relationships;

    private void Awake(){
        InitCompleteLists();
        CreateCharacters();
        for(int i = 0; i < 6; i++){
            completeListOfCharacters[UnityEngine.Random.Range(0, 500)].SetLocation(1);
        }
        CreateScriptedCharacters();
    }

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Character[] GetCharactersInRoom(int roomIndex){
        Debug.Log(completeListOfCharacters.Count);
        List<Character> characters = new List<Character>();
        for(int i = 0; i < completeListOfCharacters.Count; i++){
            if (completeListOfCharacters[i].GetLocation() == roomIndex){
                characters.Add(completeListOfCharacters[i]);
            }
        }
        
        Character[] arrayOfCharacters = new Character[characters.Count];
        for(int i = 0; i < characters.Count; i++){
            arrayOfCharacters[i] = characters[i];
        }
        return arrayOfCharacters;
    }

    private void InitCompleteLists(){
        string stringOfNames = maleFirstNameListTextFile.text;
        completeListOfMaleFirstNames = stringOfNames.Split('\n');

        stringOfNames = femaleFirstNameListTextFile.text;
        completeListOfFemaleFirstNames = stringOfNames.Split('\n');

        stringOfNames = lastNameListTextFile.text;
        completeListOfLastNames = stringOfNames.Split('\n');

        string listOfTraits = traitsListTextFile.text;
        completeListOfTraits = listOfTraits.Split('\n');

        string listOfSkills = skillsListTextFile.text;
        completeListOfSkills = listOfSkills.Split('\n');

        string listOfGoals = goalsListTextFile.text;
        completeListOfGoals = listOfGoals.Split('\n');

        //string listOfRelationships = relationshipsListTextFile.text;
        //string[] splitListOfRelationships = listOfGoals.Split('\n');

        //for (int i = 0; i < splitListOfRelationships.Length; i++){
        ////    completeListOfRelationships[i][0] = Convert.ToInt32(splitListOfRelationships[i]);
        //}
    }

    private void CreateCharacters(){
        for(int i = 0; i < 1000; i++){
            Character tempChar = new Character();
            tempChar.SetID(i);
            //assign gender; 0 male, 1 female
            tempChar.SetGender(UnityEngine.Random.Range(0, 2));
            //use gender to get a random name
            if(tempChar.GetGender() == 0){
                tempChar.SetFirstName(GetRandomMaleFirstName());
            }
            else{
                tempChar.SetFirstName(GetRandomFemaleFirstName());
            }
            //get random last name
            tempChar.SetLastName(GetRandomLastName());

            //add five random traits
            int[] tempTraits = new int[5];
            for(int j = 0; j < 5; j++){
                tempTraits[j] = GetRandomTraitIndex();
            }
            tempChar.SetTraits(tempTraits);
            //add five random skills
            int[] tempSkills = new int[5];
            for (int j = 0; j < 5; j++){
                tempSkills[j] = GetRandomSkillIndex();
            }
            tempChar.SetSkills(tempSkills);
            //add five random goals
            int[] tempGoals = new int[5];
            for (int j = 0; j < 5; j++){
                tempGoals[j] = GetRandomGoalIndex();
            }
            tempChar.SetGoals(tempGoals);

            if(UnityEngine.Random.Range(0, 1000) < 40) {
                tempChar.SetLocation(3);
            }

            //zero relationship list
            //int[][] relationships = new int[1000][];
            //tempChar.SetRelationships(relationships);

            completeListOfCharacters.Add(tempChar);
        }
    }

    private void CreateScriptedCharacters(){

        int ID;
        string[] firstAndLast = new string[2];
        int gender;
        string[] names = (scriptedCharacterNamesText.text).Split('\n');
        string[] listsOfTraits = (scriptedCharacterTraits.text).Split('\n');
        string[] listOfTraits;
        int[] traitsToInt = new int[5];
        int[] goalsToInt = new int[5] { 1, 2, 3, 4, 5 };
        string[] goals;

        for (int i = 0; i < 7; i++) {
            ID = 1000 + i;
            firstAndLast = names[i].Split(' ');
            gender = UnityEngine.Random.Range(0, 2);
            listOfTraits = listsOfTraits[i].Split(' ');
            traitsToInt = new int[5];
            for (int j = 0; j < 5; j++){
                traitsToInt[j] = Convert.ToInt16(listOfTraits[j]);
            }
            Character tempChar = new Character(ID, firstAndLast[0], firstAndLast[1], gender, 20, 100, traitsToInt, goalsToInt, goalsToInt);

            if(ID == 1000) {
                tempChar.AddBehavior(1, 0);
                tempChar.AddBehavior(2, 1);
                int[] trainingHours = { 7, 11 };
                tempChar.SetTrainingHours(trainingHours);
            }
            if (ID < 1004){
                tempChar.SetLocation(3);
                tempChar.SetImportance(1);
            }
            if(ID == 1005 || ID == 1006) {
                tempChar.SetGender(0);
            }
            completeListOfCharacters.Add(tempChar);
        }
    }

    public bool ProvokeCharacter(int characterID, int behaviorID) {
        for (int i = 0; i < 2; i++) {
            if(completeListOfCharacters[characterID].GetBehavior(i) == behaviorID) {
                return ActivateBehavior(behaviorID, characterID);
            }
        }
        return false;
    }

    public bool ActivateBehavior(int behaviorID, int characterID) {
        if (behaviorID == 1) {
            Character tempChar = completeListOfCharacters[characterID];
            int[] trainingHours = tempChar.GetTrainingHours();
            int hour = GameObject.Find("SettingManager").GetComponent<SettingManager>().GetTime()[0];
            if (hour >= trainingHours[0] && hour < trainingHours[1]) {
                string greeting = "Hey! Did I say you could take a break?";
                completeListOfCharacters[characterID].SetRelationship(completeListOfCharacters[characterID].GetRelationship() - 5);
                GameObject.Find("DecisionManager").GetComponent<DecisionManager>().TalkTo(characterID, greeting);
            }
            return true;
        }else if(behaviorID == 2) {
            Character tempChar = completeListOfCharacters[characterID];
            int[] trainingHours = tempChar.GetTrainingHours();
            int hour = GameObject.Find("SettingManager").GetComponent<SettingManager>().GetTime()[0];
            if (hour >= trainingHours[0] && hour < trainingHours[1] && tempChar.GetWarned() == 0) {
                tempChar.SetWarned(1);
                string greeting = "What are you doing? Get back to training! If you leave I will be forced to have you executed.";
                completeListOfCharacters[characterID].SetRelationship(completeListOfCharacters[characterID].GetRelationship() - 5);
                GameObject.Find("DecisionManager").GetComponent<DecisionManager>().TalkTo(characterID, greeting);
                return true;
            }
            StartCoroutine(GameObject.Find("DecisionManager").GetComponent<DecisionManager>().SendGuardsForPlayer());
            return false;
        }
        return false;
    }

    public Character GetCharacter(int index){
        return completeListOfCharacters[index];
    }

    public string GetTrait(int index){
        return completeListOfTraits[index];
    }
    public string GetSkill(int index){
        return completeListOfSkills[index];
    }
    public string GetGoal(int index){
        return completeListOfGoals[index];
    }

    

    public void UpdateInfoPanel(int characterID) {

        Character talkingTo = GetCharacter(characterID);

        //update character info panel with all characters info
        characterInfoPanel.GetComponentInChildren<Text>().text = talkingTo.GetFirstName() + " " + talkingTo.GetLastName();
        int[] playerKnowledge = talkingTo.GetPlayerKnowledge();
        for (int i = 1; i < 6; i++) {
            if (playerKnowledge[i - 1] == 1) {
                characterInfoPanel.GetComponentsInChildren<Text>()[i].text = GetTrait(talkingTo.GetTraits()[i - 1]);
            }
            else {
                characterInfoPanel.GetComponentsInChildren<Text>()[i].text = "???";
            }
        }
        for (int i = 6; i < 11; i++) {
            if (playerKnowledge[i - 1] == 1) {
                characterInfoPanel.GetComponentsInChildren<Text>()[i].text = GetSkill(talkingTo.GetSkills()[i - 6]);
            }
            else {
                characterInfoPanel.GetComponentsInChildren<Text>()[i].text = "???";
            }
        }
        for (int i = 11; i < 16; i++) {
            if (playerKnowledge[i - 1] == 1) {
                characterInfoPanel.GetComponentsInChildren<Text>()[i].text = GetGoal(talkingTo.GetSkills()[i - 11]);
            }
            else {
                characterInfoPanel.GetComponentsInChildren<Text>()[i].text = "???";
            }
        }
        relationshipSlider.value = talkingTo.GetRelationship();
    }

    public Character GetRandomCharacter(){
        return completeListOfCharacters[UnityEngine.Random.Range(0, completeListOfCharacters.Count)];
    }

    private string GetRandomMaleFirstName(){
        int randNum;
        randNum = UnityEngine.Random.Range(0, completeListOfMaleFirstNames.Length);
        return completeListOfMaleFirstNames[randNum];
    }
    private string GetRandomFemaleFirstName(){
        int randNum;
        randNum = UnityEngine.Random.Range(0, completeListOfFemaleFirstNames.Length);
        return completeListOfFemaleFirstNames[randNum];
    }

    private string GetRandomLastName(){
        int randNum;
        randNum = UnityEngine.Random.Range(0, completeListOfLastNames.Length);
        return completeListOfLastNames[randNum];
    }

    private int GetRandomTraitIndex(){
        return UnityEngine.Random.Range(0, completeListOfTraits.Length);
    }

    private int GetRandomGoalIndex(){
        return UnityEngine.Random.Range(0, completeListOfGoals.Length);
    }

    private int GetRandomSkillIndex(){
        return UnityEngine.Random.Range(0, completeListOfSkills.Length);
    }
}