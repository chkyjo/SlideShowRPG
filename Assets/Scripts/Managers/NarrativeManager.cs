using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeManager : MonoBehaviour {

    public GameObject settingManager;
    public Text mainText;

    string[] observations = new string[10];

	// Use this for initialization
	void Start () {
        observations[0] = "You look around your room. Your only possessions were already on your person. " +
            "In the corner lay a bed of fur. Stone bricks covered every other surface. There was no door " +
            "to the winding stairway that lead to the top.";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetObservation()
    {
        mainText.text = observations[settingManager.GetComponent<SettingManager>().GetRoom()];
    }
}
