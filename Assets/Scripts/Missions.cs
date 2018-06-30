using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missions : MonoBehaviour {

    public int missionID;
    public GameObject conversationBackgroundPanel;
    public GameObject characterManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UseMissionManager() {
        GameObject.Find("MissionManager").GetComponent<Missions>().StartMission(missionID);
    }

    public void StartMission(int ID) {
        switch (missionID) {
            case 0:
                StartCoroutine(HuntingMission());
                break;
            case 1:
                break;
        }
    }

    IEnumerator HuntingMission() {
        yield return new WaitForSeconds(3f);

        conversationBackgroundPanel.SetActive(false);
        CharactersManager cM = characterManager.GetComponent<CharactersManager>();
        Character tempChar = cM.GetCharacter(1000);
        tempChar.SetLocation(6);
        tempChar = cM.GetCharacter(1001);
        tempChar.SetLocation(6);
        tempChar = cM.GetCharacter(1002);
        tempChar.SetLocation(6);
        tempChar = cM.GetCharacter(1003);
        tempChar.SetLocation(6);


        Debug.Log("Mission started");
    }
}
