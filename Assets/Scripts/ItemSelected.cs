using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelected : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CallNextFunction() {
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().ThrowItemAt();
    }
}
