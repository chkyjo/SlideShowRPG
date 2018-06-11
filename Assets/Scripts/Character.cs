using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character{

    int _ID;
    string _firstName;
    string _lastName;
    int _gender; //0 male, 1 female
    int _age;
    int _height;
    int _weight;

    int[] _traits; //things that make them unique
    int[] _goals; //things they want to accomplish
    int[] _skills; //things they are good at
    int[][] _relationships; //how they feel about others
    int _relationship; //how they feel about you

    int _roomLocation;

    public Character(int ID, string firstName, string lastName, int gender, int age, int height, int weight, int[] traits, int[] goals, int[] skills){
        _ID = ID;
        _firstName = firstName;
        _lastName = lastName;
        _gender = gender;
        _age = age;
        _height = height;
        _weight = weight;
        _traits = traits;
        _goals = goals;
        _skills = skills;
        _relationship = 50;
        _relationships = new int[1][];
        _roomLocation = -1;
    }

    public Character(){
        _roomLocation = -1;
        _ID = 0;
        _relationships = new int[1][];
        _relationship = 50;
    }

    public int GetID(){
        return _ID;
    }
    public string GetFirstName(){
        return _firstName;
    }
    public string GetLastName(){
        return _lastName;
    }
    public int GetGender(){
        return _gender;
    }
    public int GetAge(){
        return _age;
    }
    public int GetHeight(){
        return _height;
    }
    public int GetWeight(){
        return _weight;
    }
    public int GetLocation(){
        return _roomLocation;
    }

    public int[] GetTraits(){
        return _traits;
    }
    public int[] GetGoals(){
        return _goals;
    }
    public int[] GetSkills(){
        return _skills;
    }
    public int[][] GetRelationships(){
        return _relationships;
    }
    public int GetRelationship(){
        return _relationship;
    }

    public void SetID(int ID){
        _ID = ID;
    }
    public void SetFirstName(string firstName){
        _firstName = firstName;
    }
    public void SetLastName(string lastName){
        _lastName = lastName;
    }
    public void SetGender(int gender){
        _gender = gender;
    }
    public void SetAge(int age){
        _age = age;
    }
    public void SetHeight(int height){
        _height = height;
    }
    public void SetWeight(int weight){
        _weight = weight;
    }

    public void SetTraits(int[] traits){
        _traits = traits;
    }
    public void SetGoals(int[] goals){
        _goals = goals;
    }
    public void SetSkills(int[] skills){
        _skills = skills;
    }
    public void SetRelationships(int[][] relationships){
        _relationships = relationships;
    }

    public void SetLocation(int location){
        _roomLocation = location;
    }
    public void SetRelationship(int relationship)
    {
        _relationship = relationship;
    }
}
