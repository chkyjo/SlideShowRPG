using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character {

    int _ID;
    string _firstName;
    string _lastName;
    string _status;
    int _gender; //0 male, 1 female
    int _age;
    int _health;
    string _greeting;
    int _importance;

    int[] _traits; //things that make them unique
    int[] _goals; //things they want to accomplish
    int[] _skills; //things they are good at
    int[][] _relationships; //how they feel about others
    int _relationship; //how they feel about you
    int[] _playerKnowledge; //what the player knows about the character

    int[] _convoList;

    int _roomLocation;
    int[] _missions;
    int[][] _missionTimes;

    int[] _trainings;
    int[][] _trainingHours;
    int _warned;
    int[] _behaviors;

    public Character(int ID, string firstName, string lastName, int gender, int age, int health, int[] traits, int[] goals, int[] skills) {
        _ID = ID;
        _firstName = firstName;
        _lastName = lastName;
        _status = "Alive";
        _gender = gender;
        _age = age;
        _health = health;
        _greeting = "";
        _importance = 0;

        _traits = traits;
        _goals = goals;
        _skills = skills;
        _relationship = 50;
        _relationships = new int[1][];
        _playerKnowledge = new int[15];

        _convoList = new int[3] { 0, -1, -1 };

        _roomLocation = -1;
        _missions = new int[3] {0,0,0 };
        _missionTimes = new int[3][];

        _trainings = new int[3] { 0, 0, 0 };
        _trainingHours = new int[2][];
        _warned = 0;
        _behaviors = new int[2]{0,0};

    }

    public Character() {
        _status = "Alive";
        _roomLocation = -1;
        _health = 100;
        _greeting = "";
        _importance = 0;

        _relationships = new int[1][];
        _relationship = 50;
        _playerKnowledge = new int[15] { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

        _convoList = new int[3] { 0, -1, -1 };

        _missions = new int[3] { 0, 0, 0 };
        _trainings = new int[3] { 0, 0, 0 };
        _behaviors = new int[2] { 0, 0 };
    }

    public int GetID() {
        return _ID;
    }
    public string GetFirstName() {
        return _firstName;
    }
    public string GetLastName() {
        return _lastName;
    }
    public string GetStatus() {
        return _status;
    }
    public int GetGender() {
        return _gender;
    }
    public int GetAge() {
        return _age;
    }
    public int GetHealth() {
        return _health;
    }
    public string GetGreeting() {
        return _greeting;
    }
    public int GetImportance() {
        return _importance;
    }
    

    public int[] GetTraits() {
        return _traits;
    }
    public int[] GetGoals() {
        return _goals;
    }
    public int[] GetSkills() {
        return _skills;
    }
    public int[][] GetRelationships() {
        return _relationships;
    }

    public int[] GetConvoList() {
        return _convoList;
    }
    

    public int GetRelationship() {
        return _relationship;
    }
    public int[] GetPlayerKnowledge(){
        return _playerKnowledge;
    }
    public int GetLocation() {
        return _roomLocation;
    }
    public int[] GetMissions() {
        return _missions;
    }
    public int[][] GetMissionTimes() {
        return _missionTimes;
    }

    public int[] GetTrainings() {
        return _trainings;
    }
    public int[][] GetTrainingHours() {
        return _trainingHours;
    }
    public int GetWarned() {
        return _warned;
    }
    public int GetBehavior(int index) {
        return _behaviors[index];
    }
    

    public void SetID(int ID) {
        _ID = ID;
    }
    public void SetFirstName(string firstName) {
        _firstName = firstName;
    }
    public void SetLastName(string lastName) {
        _lastName = lastName;
    }
    public void SetStatus(string status) {
        _status = status;
    }
    public void SetGender(int gender) {
        _gender = gender;
    }
    public void SetAge(int age) {
        _age = age;
    }
    public void SubtractHealth(int health) {
        _health -= health;
        if (_health < 0) {
            _health = 0;
            _status = "Dead";
        }
    }
    public void SetGreeting(string greeting) {
        _greeting = greeting;
    }
    public void SetImportance(int importance) {
        _importance = importance;
    }

    public void SetTraits(int[] traits) {
        _traits = traits;
    }
    public void SetGoals(int[] goals) {
        _goals = goals;
    }
    public void SetSkills(int[] skills) {
        _skills = skills;
    }
    public void SetRelationships(int[][] relationships) {
        _relationships = relationships;
    }
    public void SetPlayerKnowledge(int index){
        _playerKnowledge[index] = 1;
    }

    public void AddConvo(int convoID, int index) {
        _convoList[index] = convoID;
    }

    public void SetLocation(int location){
        _roomLocation = location;
    }
    public void SetMissions(int[] missions) {
        _missions = missions;
    }
    public void SetMissionTimes(int[][] missionTimes) {
        _missionTimes = missionTimes;
    }


    public void SetRelationship(int relationship)
    {
        _relationship = relationship;
    }
    public void AddRelationship(int rel) {
        _relationship += rel;
    }
    public void SubtractRelationship(int rel) {
        _relationship -= rel;
    }

    public void SetTrainings(int[] trainings) {
        _trainings = trainings;
    }
    public void SetTrainingHours(int[][] hours) {
        _trainingHours = hours;
    }
    public void SetWarned(int warned) {
        _warned = warned;
    }
    public void AddBehavior(int behavior, int index) {
        _behaviors[index] = behavior;
    }
}
