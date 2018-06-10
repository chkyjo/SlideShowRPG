using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    public GameObject settingManager;

    Room[] rooms = new Room[10];

    int[] publicLocations = new int[10];

    void Awake() {
        

    }

    // Use this for initialization
    void Start () {
        int[] options = new int[1] { 5 };
        string observationText = "You look around your room. Your only possessions were already on your person. " +
            "In the corner lay a bed of fur. Stone bricks covered every other surface. There was no door " +
            "to the winding stairway that lead to the top.";
        string[] exitTexts = new string[1];
        exitTexts[0] = "Continue up the stairs";
        string[] leaveTexts = new string[1];
        leaveTexts[0] = "You walk up the stairs and find yourself in a large courtyard.";
        int[] connectedRooms = new int[1];
        connectedRooms[0] = 1;
        rooms[0] = new Room(1, observationText, 1, options, exitTexts, leaveTexts, connectedRooms);

        options = new int[1] { 6 };
        observationText = "The courtyard was a perfect square surrounded by pillars holding up a stone ceiling. Inside the " +
            "pillars was a square field of green grass with flowers and benches. On the other side of the courtyard was a " +
            "large double doorway.";
        exitTexts = new string[2];
        exitTexts[0] = "Enter through the double doors";
        exitTexts[1] = "Down stairs to bed chamber";
        leaveTexts = new string[2];
        leaveTexts[0] = "You lean into the double doors with your small frame and gradually it opens.";
        leaveTexts[1] = "You head down the stairs back to your bed chamber.";
        connectedRooms = new int[2];
        connectedRooms[0] = 2;
        connectedRooms[1] = 0;
        rooms[1] = new Room(1, observationText, 2, options, exitTexts, leaveTexts, connectedRooms);

        publicLocations[0] = 1;
        publicLocations[1] = 2;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Room GetRoom(int index){
        return rooms[index];
    }

    public Room GetCurrentRoom(){
        return rooms[settingManager.GetComponent<SettingManager>().currentRoom];
    }
}
