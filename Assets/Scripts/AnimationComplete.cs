using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationComplete : MonoBehaviour {

    public GameObject nextText;
    public GameObject inventoryButton;
    public GameObject decisionPanel;
    public GameObject skipButton;
    public GameObject indoorPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartNextPassage(){
        gameObject.SetActive(false);
        nextText.SetActive(true);
    }

    public void Begin() {
        gameObject.SetActive(false);
        GameObject.Find("GameManager").GetComponent<GameManager>().SetUpGame();
    }
}
