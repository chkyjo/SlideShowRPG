using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character{

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

    public Character(string firstName, string lastName, int gender, int age, int height, int weight, int[] traits, int[] goals, int[] skills, int[][] relationships){
        _firstName = firstName;
        _lastName = lastName;
        _gender = gender;
        _age = age;
        _height = height;
        _weight = weight;
        _traits = traits;
        _goals = goals;
        _skills = skills;
        _relationships = relationships;
    }

    public Character(){

    }

    public string GetFirstName()
    {
        return _firstName;
    }
    public string GetLastName()
    {
        return _lastName;
    }
    public int GetGender()
    {
        return _gender;
    }
    public int GetAge()
    {
        return _age;
    }
    public int GetHeight()
    {
        return _height;
    }
    public int GetWeight()
    {
        return _weight;
    }

    public int[] GetTraits()
    {
        return _traits;
    }
    public int[] GetGoals()
    {
        return _goals;
    }
    public int[] GetSkills()
    {
        return _skills;
    }
    public int[][] GetRelationships()
    {
        return _relationships;
    }

    public void SetFirstName(string firstName)
    {
        _firstName = firstName;
    }
    public void SetLastName(string lastName)
    {
        _lastName = lastName;
    }
    public void SetGender(int gender)
    {
        _gender = gender;
    }
    public void SetAge(int age)
    {
        _age = age;
    }
    public void SetHeight(int height)
    {
        _height = height;
    }
    public void SetWeight(int weight)
    {
        _weight = weight;
    }

    public void SetTraits(int[] traits)
    {
        _traits = traits;
    }
    public void SetGoals(int[] goals)
    {
        _goals = goals;
    }
    public void SetSkills(int[] skills)
    {
        _skills = skills;
    }
    public void SetRelationships(int[][] relationships)
    {
        _relationships = relationships;
    }
}
