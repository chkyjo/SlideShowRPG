using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkOption : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Talk(){
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().Talk();
    }
    public void TalkTo(){
        string firstAndLast = this.gameObject.GetComponentsInChildren<Text>()[1].text;
        string[] bothNames = firstAndLast.Split(' ');
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().TalkTo(bothNames[0], bothNames[1]);
    }
}
