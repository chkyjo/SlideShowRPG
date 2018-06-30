using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAction : MonoBehaviour {

    public int characterID;
    public int itemID;

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
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().ThrowItem(itemID);
    }

    public void ThrowItemAt(){
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().ThrowItemAt(characterID, itemID);
    }
}
