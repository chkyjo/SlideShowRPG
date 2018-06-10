using UnityEngine;
using UnityEngine.UI;

public class TalkToOption : MonoBehaviour {

    public int characterID;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TalkTo(){
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().TalkTo(characterID);
    }
}
