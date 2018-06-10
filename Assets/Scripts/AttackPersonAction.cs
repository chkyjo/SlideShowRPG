using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPersonAction : MonoBehaviour {

    public int characterID;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AttackPerson(){
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().AttackPerson(characterID);
    }
}
