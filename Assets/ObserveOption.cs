using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserveOption : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Observe()
    {
        GameObject.Find("NarrativeManager").GetComponent<NarrativeManager>().GetObservation();
    }
}
