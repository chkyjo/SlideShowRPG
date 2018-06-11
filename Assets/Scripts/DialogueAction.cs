using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueAction : MonoBehaviour {

    public string dialogueMessage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MakeDialogue(){
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().MakeDialogueChoice(dialogueMessage);
    }
}
