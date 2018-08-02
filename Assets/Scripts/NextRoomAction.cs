using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoomAction : MonoBehaviour{

    public int nextRoomID;
    public string leaveText;

    // Use this for initialization
    void Start(){

    }

    // Update is called once per frame
    void Update(){

    }

    public void NextRoom(){
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().NextRoom(nextRoomID, leaveText);
    }

    public void DisplayExitOptions() {
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().DisplayExitOptions();
    }

}