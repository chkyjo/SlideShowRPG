﻿using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour {

    List<Character> completeListOfCharacters = new List<Character>();

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
    public enum likes{
        gettingDrunk,
        brothels,
        dogs,
        cats,
        animals,
        cheese,
        running,
        fighting,
        killing,
        torturing,
        eating,
        cardGames,
        horseRiding,
        sparring,
        wrestling,
        flirting,
        work,
        job,
    }
    public enum dislikes {
        gettingDrunk,
        brothels,
        dogs,
        cats,
        animals,
        cheese,
        running,
        fighting,
        killing,
        cardGames,
        horseRiding,
        sparring,
        wrestling,
        flirting,
        work,
        job,
    }
    public enum okays {
        job,
        tomatos,
        potatos,
        salad,
        fish,
        fishing,
        court,
        government,
        order,
        socialNorms,
        society,
        work,
        farming,
        cleaning,
        talking,
        singing,
        dancing,
        smoking,
        sweets,
        vegetables,
        meat,
        bartering,
        courting,
        running,
        fighting,
        combatTraining,
        cooking,
        gettingDrunk,
        silliness,
        newTechnology,
        repetition,
        creativity,
        tobacco,

    }
    public enum hates {
        job,
        tomatos,
        potatos,
        salad,
        fish,
        fishing,
        court,
        government,
        order,
        socialNorms,
        society,
        work,
        farming,
        cleaning,
        talking,
        singing,
        dancing,
        smoking,
        sweets,
        vegetables,
        meat,
        bars,
        bartering,
        courting,
        running,
        fighting,
        combatTraining,
        cooking,
        gettingDrunk,
        silliness,
        newTechnology,
        repetition,
        creativity,
        children,
        animals,
        tobacco,
    }
    public enum loves {
        job,
        tomatos,
        potatos,
        salad,
        fish,
        fishing,
        court,
        government,
        order,
        society,
        work,
        farming,
        cleaning,
        talking,
        singing,
        dancing,
        sweets,
        vegetables,
        meat,
        bars,
        bartering,
        courting,
        running,
        fighting,
        combatTraining,
        cooking,
        gettingDrunk,
        silliness,
        newTechnology,
        repetition,
        creativity,
        children,
        animals,
        tobacco,
    }

    private void Awake(){
        InitCompleteLists();
        CreateCharacters();
        for(int i = 0; i < 6; i++){
            completeListOfCharacters[UnityEngine.Random.Range(0, 500)].SetLocation(1);
        }
        CreateScriptedCharacters();
    }

    public Character[] GetCharactersInRoom(int roomIndex){
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

    public int GetNumCharactersInRoom(int roomIndex) {
        int numCharacters = 0;
        for (int i = 0; i < completeListOfCharacters.Count; i++) {
            if (completeListOfCharacters[i].GetLocation() == roomIndex) {
                numCharacters++;
            }
        }
        return numCharacters;
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

        Array loves = Enum.GetValues(typeof(loves));
        Array likes = Enum.GetValues(typeof(likes));
        Array okays = Enum.GetValues(typeof(okays));
        Array dislikes = Enum.GetValues(typeof(dislikes));
        Array hates = Enum.GetValues(typeof(hates));
        System.Random rand = new System.Random();
        loves[] randomLoves;
        likes[] randomLikes;
        okays[] randomOkays;
        dislikes[] randomDislikes;
        hates[] randomHates;

        for (int i = 0; i < 1000; i++){
            Character tempChar = new Character();
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

            randomLoves = new loves[5];
            randomLikes = new likes[5];
            randomOkays = new okays[5];
            randomDislikes = new dislikes[5];
            randomHates = new hates[5];
            for(int j = 0; j < 5; j++) {
                randomLoves[j] = (loves)loves.GetValue(rand.Next(loves.Length));
                randomLikes[j] = (likes)likes.GetValue(rand.Next(likes.Length));
                randomOkays[j] = (okays)okays.GetValue(rand.Next(okays.Length));
                randomDislikes[j] = (dislikes)dislikes.GetValue(rand.Next(dislikes.Length));
                randomHates[j] = (hates)hates.GetValue(rand.Next(hates.Length));
            }
            tempChar.SetPreferences(randomLoves);
            tempChar.SetPreferences(randomLikes);
            tempChar.SetPreferences(randomOkays);
            tempChar.SetPreferences(randomDislikes);
            tempChar.SetPreferences(randomHates);




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

        Array loves = Enum.GetValues(typeof(loves));
        Array likes = Enum.GetValues(typeof(likes));
        Array okays = Enum.GetValues(typeof(okays));
        Array dislikes = Enum.GetValues(typeof(dislikes));
        Array hates = Enum.GetValues(typeof(hates));
        System.Random rand = new System.Random();
        loves[] randomLoves;
        likes[] randomLikes;
        okays[] randomOkays;
        dislikes[] randomDislikes;
        hates[] randomHates;

        for (int i = 0; i < 10; i++) {
            ID = 1000 + i;
            firstAndLast = names[i].Split(' ');
            gender = UnityEngine.Random.Range(0, 2);
            listOfTraits = listsOfTraits[i].Split(' ');
            traitsToInt = new int[5];
            for (int j = 0; j < 5; j++){
                traitsToInt[j] = Convert.ToInt16(listOfTraits[j]);
            }
            Character tempChar = new Character(ID, firstAndLast[0], firstAndLast[1], gender, 20, 100, traitsToInt, goalsToInt, goalsToInt);

            randomLoves = new loves[5];
            randomLikes = new likes[5];
            randomOkays = new okays[5];
            randomDislikes = new dislikes[5];
            randomHates = new hates[5];
            for (int j = 0; j < 5; j++) {
                randomLoves[j] = (loves)loves.GetValue(rand.Next(loves.Length));
                randomLikes[j] = (likes)likes.GetValue(rand.Next(likes.Length));
                randomOkays[j] = (okays)okays.GetValue(rand.Next(okays.Length));
                randomDislikes[j] = (dislikes)dislikes.GetValue(rand.Next(dislikes.Length));
                randomHates[j] = (hates)hates.GetValue(rand.Next(hates.Length));
            }
            tempChar.SetPreferences(randomLoves);
            tempChar.SetPreferences(randomLikes);
            tempChar.SetPreferences(randomOkays);
            tempChar.SetPreferences(randomDislikes);
            tempChar.SetPreferences(randomHates);

            if (ID == 1000) {
                tempChar.AddBehavior(1, 0);
                tempChar.AddBehavior(2, 1);
                tempChar.SetLocation(6003);
                int[] trainings = new int[] { 1, 0, 0 };
                tempChar.SetTrainings(trainings);
                int[][] trainingHours = new int[3][];
                trainingHours[0] = new int[] { 7, 11 };
                tempChar.SetTrainingHours(trainingHours);
                int[] missions = new int[] { 1, 0, 0 };
                int[][] missionTimes = new int[3][];
                missionTimes[0] = new int[] { 12, 13 };
                tempChar.SetMissions(missions);
                tempChar.SetMissionTimes(missionTimes);
                
            }
            if(ID == 1001) {
                tempChar.SetLocation(6003);
            }
            if(ID == 1002) {
                tempChar.SetLocation(6003);
                tempChar.AddConvo(2, 1);
            }
            if(ID == 1003) {
                tempChar.SetLocation(6003);
            }
            if (ID < 1004){
                //tempChar.SetLocation(6003);
                tempChar.SetImportance(1);
            }
            if(ID == 1005 || ID == 1006) {
                tempChar.SetGender(0);
            }
            if(ID == 1007 || ID == 1008) {
                tempChar.SetLocation(7);
                tempChar.SetImportance(1);
            }
            if(ID == 1009) {
                tempChar.SetImportance(1);
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
		
        if (behaviorID == 1) {//character doesn not like when player stops during training
            for(int i = 0; i < 3; i++) {
                Character tempChar = completeListOfCharacters[characterID];
                int[][] trainingHours = tempChar.GetTrainingHours();
                int hour = GameObject.Find("SettingManager").GetComponent<SettingManager>().GetTime()[0];
				//for each training slot
                if(tempChar.GetTrainings()[i] != 0) {
					//if the hour is between the training times for a training
                    if (hour >= trainingHours[i][0] && hour < trainingHours[i][1]) {
                        string greeting = "Hey! Did I say you could take a break?";
                        completeListOfCharacters[characterID].SetRelationshipLvl(completeListOfCharacters[characterID].GetRelationshipLvl() - 5);
                        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().TalkTo(characterID, greeting);
                    }
                }
            }
            
            return true;
        }else if(behaviorID == 2) {//character does not like when player leaves during training
            Character tempChar = completeListOfCharacters[characterID];
            int[][] trainingHours = tempChar.GetTrainingHours();
            int hour = GameObject.Find("SettingManager").GetComponent<SettingManager>().GetTime()[0];
            for(int i = 0; i < 3; i++) {
                int[] trainings = tempChar.GetTrainings();
                if(trainings[i] != 0) {
                    if (hour >= trainingHours[i][0] && hour < trainingHours[i][1] && tempChar.GetWarned() == 0) {
                        tempChar.SetWarned(1);//set that the character warned the player about leaving during training
                        string greeting = "What are you doing? Get back to training! If you leave I will be forced to have you executed.";
                        completeListOfCharacters[characterID].SetRelationshipLvl(completeListOfCharacters[characterID].GetRelationshipLvl() - 5);
                        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().TalkTo(characterID, greeting);
                        return true;
                    }
                    else if (hour >= trainingHours[i][0] && hour < trainingHours[i][1] && tempChar.GetWarned() == 1) {
						//if player was already warned
                        StartCoroutine(GameObject.Find("DecisionManager").GetComponent<DecisionManager>().SendGuardsForPlayer());
                    }
                }
            }
            
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
        PlayerManager pM = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        
        int[,] prefKnowledge;
        int[] traitsKnowledge = new int[] { 0,0,0,0,0};
        int[] goalsKnowledge = new int[] { 0, 0, 0, 0, 0 };
        int[] skillsKnowledge = new int[] { 0, 0, 0, 0, 0 };

        //update character info panel with all characters info
        characterInfoPanel.GetComponentInChildren<Text>().text = talkingTo.GetFirstName() + " " + talkingTo.GetLastName();

        //get what the player knows
        for(int i = 0; i < pM.knowledgeOfCharacters.Count; i++) {
            if(characterID == pM.knowledgeOfCharacters[i].characterID) {
                prefKnowledge = pM.knowledgeOfCharacters[i]._preferences;
                traitsKnowledge = pM.knowledgeOfCharacters[i]._traits;
            }
        }

        for (int i = 0; i < 5; i++) {
            if (traitsKnowledge[i] == 1) {
                characterInfoPanel.transform.GetChild(1).GetComponentsInChildren<Text>()[i+1].text = GetTrait(talkingTo.GetTraits()[i]);
            }
            else {
                characterInfoPanel.transform.GetChild(1).GetComponentsInChildren<Text>()[i + 1].text = "???";
            }

            if (skillsKnowledge[i] == 1) {
                characterInfoPanel.transform.GetChild(2).GetComponentsInChildren<Text>()[i+1].text = GetSkill(talkingTo.GetSkills()[i]);
            }
            else {
                characterInfoPanel.transform.GetChild(2).GetComponentsInChildren<Text>()[i + 1].text = "???";
            }

            if (goalsKnowledge[i] == 1) {
                characterInfoPanel.transform.GetChild(5).GetComponentsInChildren<Text>()[i+1].text = GetGoal(talkingTo.GetSkills()[i]);
            }
            else {
                characterInfoPanel.transform.GetChild(5).GetComponentsInChildren<Text>()[i + 1].text = "???";
            }
        }

        relationshipSlider.value = talkingTo.GetRelationshipLvl();
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