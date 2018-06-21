using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAction : MonoBehaviour {

    public int characterID;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Throw(){
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().Throw();
    }

    public void ThrowItem(){
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().ThrowItem();
    }

    public void ThrowItemAt(){
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().ThrowItemAt(characterID);
    }
}
