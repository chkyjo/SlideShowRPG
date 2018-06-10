using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public Slider healthBar;
    public Slider caloriesBar;

    public int health;
    public int calories;

	// Use this for initialization
	void Start () {
        health = 76;
        healthBar.value = health;
        calories = 284;
        caloriesBar.value = calories;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateCalories()
    {
        caloriesBar.value = calories;
    }

}
