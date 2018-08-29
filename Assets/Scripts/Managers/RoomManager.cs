using System.Collections;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    public GameObject settingManager;

    public Room[] rooms = new Room[7000];

    int[] publicLocations = new int[10];

    int[] castleRooms;
    int[] plains;

    void Awake() {

        //castleRooms = new int[2] { 845, 846 };
        plains = new int[23] { 643, 644, 645, 646, 647, 743, 744, 745, 746, 747, 843, 844, 847, 943, 944, 945, 946, 947, 1043, 1044, 1045, 1046, 1047 };
        Room room;
        int[,] buttonLocations;
        int[] mapLayout = new int[6000];

        Debug.Log("Creating layout...");
        for (int i = 0; i < 6000; i++) {
            mapLayout[i] = 0;
        }

        for(int i = 0; i < 23; i++) {
            mapLayout[plains[i]] = 1;
        }

        mapLayout[845] = 2;
        mapLayout[846] = 3;

        Texture2D rawImg = Resources.Load("BedChamber") as Texture2D;
        int[] options = new int[1] { 5 };
        string observationText = "You look around your room. Your only possessions were already on your person. " +
            "In the corner lay a bed of fur. Stone bricks covered every other surface. There was no door " +
            "to the winding stairway that lead to the top.";
        string[] exitTexts = new string[1];
        exitTexts[0] = "Continue up the stairs";
        string[] leaveTexts = new string[1];
        leaveTexts[0] = "You walk up the stairs and find yourself in a large courtyard.";
        int[] connectedRooms = new int[1];
        connectedRooms[0] = 6001;
        room = new Room("Bedchamber", 1, observationText, 1, options, exitTexts, leaveTexts, connectedRooms);
        room.SetRoomCoordinates(1137, -212);
        room.SetImage(rawImg);
        buttonLocations = new int[1, 2] { { 57, -88 } };
        room.SetButtonLocation(buttonLocations);
        rooms[6000] = room;

        rawImg = Resources.Load("Courtyard") as Texture2D;
        options = new int[1] { 6 };
        observationText = "The courtyard was a perfect square surrounded by pillars holding up a stone ceiling. Inside the " +
            "pillars was a square field of green grass with flowers and benches. On the other side of the courtyard was a " +
            "large double doorway.";
        exitTexts = new string[3];
        exitTexts[0] = "Enter through the double doors";
        exitTexts[1] = "Down stairs to bed chamber";
        exitTexts[2] = "Entrance hall";
        leaveTexts = new string[3];
        leaveTexts[0] = "You lean into the double doors with your small frame and gradually it opens. The great hall appears before you.";
        leaveTexts[1] = "You head down the stairs back to your bed chamber.";
        leaveTexts[2] = "You push through the door and find yourself looking down a sloped hallway.";
        connectedRooms = new int[3];
        connectedRooms[0] = 6002;
        connectedRooms[1] = 6000;
        connectedRooms[2] = 6004;
        room = new Room("Courtyard", 1, observationText, 3, options, exitTexts, leaveTexts, connectedRooms);
        room.SetRoomCoordinates(1137, -212);
        room.SetImage(rawImg);
        buttonLocations = new int[,] { { 213, 195 }, { 115, -114 }, { 387, 64 } };
        room.SetButtonLocation(buttonLocations);
        rooms[6001] = room;

        rawImg = Resources.Load("GreatHall") as Texture2D;
        options = new int[2] { 6, 7 };
        observationText = "Candles flickered and lit up every inch of the giant room. Large beams of sunlight shot diagonally down " +
            "from the high windows. Food was laid out on all five of the long tables that extended from you down to the lords " +
            "table at the far end. Many were still in the middle of breakfast. The main entrance to the castle was an opened gate to the right of the lords table.";
        exitTexts = new string[2];
        exitTexts[0] = "Training Grounds";
        exitTexts[1] = "Courtyard";
        leaveTexts = new string[2];
        leaveTexts[0] = "You walk over to the main entrance and pass two guards on your way out. The sun is blinding as you step out into the open air.";
        leaveTexts[1] = "You push your way through the double doors to the courtyard.";
        connectedRooms = new int[2];
        connectedRooms[0] = 6003;
        connectedRooms[1] = 6001;
        room = new Room("Main Hall", 2, observationText, 2, options, exitTexts, leaveTexts, connectedRooms);
        room.SetRoomCoordinates(1137, -212);
        room.SetImage(rawImg);
        buttonLocations = new int[,] { { 372, 130 }, { 229, -134 } };
        room.SetButtonLocation(buttonLocations);
        rooms[6002] = room;
        
        rawImg = Resources.Load("TrainingGrounds") as Texture2D;
        options = new int[0] { };
        observationText = "A sloped, curvy walkway led down from the castle to a lower open field. Men, women, and children were running and parrying " +
            "with each other. Distant yells and shouts echoed through the sky. In the distance you could see Gregory barking commands at the other " +
            "recruits you had become familiar with.";
        exitTexts = new string[2];
        exitTexts[0] = "Archway";
        exitTexts[1] = "Main Hall";
        leaveTexts = new string[2];
        leaveTexts[0] = "You cross the training grounds to an archway under the castle walls and walk under it.";
        leaveTexts[1] = "You walk over to the main entrance and pass two guards on your way in. You are hit with the soft candle light and the smell of food.";
        connectedRooms = new int[2];
        connectedRooms[0] = 846;
        connectedRooms[1] = 6002;
        room = new Room("Training Grounds", 0, observationText, 2, options, exitTexts, leaveTexts, connectedRooms);
        room.SetRoomCoordinates(1137, -212);
        room.SetImage(rawImg);
        buttonLocations = new int[,] { { 356, 189 }, { 43, -38 } };
        room.SetButtonLocation(buttonLocations);
        rooms[6003] = room;

        rawImg = Resources.Load("EntranceHall") as Texture2D;
        options = new int[0] { };
        observationText = "A large, closed gate stood at the main entrance at the other end of the curved room. Multiple guards were standing watch on both the inside and out. The floor was decorated with shined marble and blue and gold rugs. The walls were reinforced with cobbled stone and hung large tapestries depicting battles against ferocious beasts.";
        exitTexts = new string[3];
        exitTexts[0] = "Courtyard";
        exitTexts[1] = "Protectors bed chambers";
        exitTexts[2] = "Leave castle";
        leaveTexts = new string[3];
        leaveTexts[0] = "You step through the door into the courtyard.";
        leaveTexts[1] = "You attempt to enter the door to the bed chambers but it is locked.";
        leaveTexts[2] = "You approach the gate but the guards turn you away.";
        connectedRooms = new int[3];
        connectedRooms[0] = 6001;
        connectedRooms[1] = 6004;
        connectedRooms[2] = 6004;
        room = new Room("Entrance Hall", 0, observationText, 3, options, exitTexts, leaveTexts, connectedRooms);
        room.SetRoomCoordinates(1137, -212);
        room.SetImage(rawImg);
        buttonLocations = new int[,] { { 54, 17 }, { 162, -92 }, { 340, 63 } };
        room.SetButtonLocation(buttonLocations);
        rooms[6004] = room;

        options = new int[0] { };
        observationText = "It was deathly quiet. You were the only one in your cell. A couple of travelers were in the cell two cells down. Your cell was nothing but a box. Two of the walls were made of rock, the other two were iron bars.";
        exitTexts = new string[0];
        leaveTexts = new string[0];
        connectedRooms = new int[0];
        rooms[6005] = new Room("Prison", 0, observationText, 0, options, exitTexts, leaveTexts, connectedRooms);
        room.SetRoomCoordinates(1137, -212);

        Debug.Log("Setting up rooms...");
        //set up rooms
        int roomIndex = 0;
        for (int j = 0; j < 60; j++) {
            for (int i = 0; i < 100; i++) {
                
                room = new Room();
                if(mapLayout[roomIndex] == 0) {
                    room.SetName("Forest");
                    room.SetType("Forest");
                    rawImg = Resources.Load("Forest") as Texture2D;
                    room.SetImage(rawImg);
                    observationText = "Densely packed trees quickly hid the surrounding forest. Only a faint trickle of light made it through the leaves above.";
                    room.SetObservation(observationText);
                }
                else if(mapLayout[roomIndex] == 1) {
                    room.SetName("Open Field");
                    room.SetType("Open Field");
                    rawImg = Resources.Load("OpenField") as Texture2D;
                    room.SetImage(rawImg);
                    observationText = "Wide open fields spread out in all directions. The massive castle of the Protectors stood towering over the trees.";
                    room.SetObservation(observationText);
                }
                else if (mapLayout[roomIndex] == 2) {
                    room.SetName("Castle");
                    room.SetType("Building");
                    room.SetRoomCoordinates(1137, -212);
                    room.SetTeleport(6003);
                    room.SetEntrance(2);
                }
                else if (mapLayout[roomIndex] == 3) {
                    room.SetName("Bridge");
                    room.SetType("Bridge");
                    rawImg = Resources.Load("Bridge") as Texture2D;
                    room.SetImage(rawImg);
                    observationText = "Guards stand at the other end of the bridge. Their spears angle out toward the open field which disappeares behind the cover of trees.";
                    room.SetObservation(observationText);
                }

                room.SetRoomCoordinates((i * 25) + 12, -(j * 25) - 12);
                rooms[roomIndex] = room;
                
                roomIndex++;
            }
        }

        Debug.Log("Connecting rooms...");
        //connect the rooms with exits
        roomIndex = 0;
        for (int i = 0; i < 6000; i++) {
            if(mapLayout[i] != 2) {
                GetConnectedRooms(i, rooms[i]);
            }
        }

        publicLocations[0] = 1;
        publicLocations[1] = 2;

    }

    public void GetConnectedRooms(int ID, Room room) {

        int j = ID / 100;
        int i = ID % 100;

        int numExits = 0;

        string[] exitTexts = new string[4];
        string[] leaveTexts = new string[4];
        int[] connectedRooms = new int[4];
        int[,] theButtonLocations = new int[4, 2] { { 0, 36 }, { 175, 191 }, { 350, 36 }, { 175, -125 } };
        int[,] buttonLocations = new int[4, 2];

        if (CheckIfExit(i, j, 0)) {
            //can go west
            exitTexts[numExits] = "West: " + GetRoomType(ID - 1);
            leaveTexts[numExits] = "You travel west.";
            connectedRooms[numExits] = ID - 1;
            buttonLocations[numExits, 0] = 0;
            buttonLocations[numExits, 1] = 36;
            numExits++;
        }
        if (CheckIfExit(i, j, 1)) {
            //can go north
            exitTexts[numExits] = "North: " + GetRoomType(ID - 100);
            leaveTexts[numExits] = "You travel north.";
            connectedRooms[numExits] = ID - 100;
            buttonLocations[numExits, 0] = 175;
            buttonLocations[numExits, 1] = 191;
            numExits++;
        }
        if (CheckIfExit(i, j, 2)) {
            //can go east
            exitTexts[numExits] = "East: " + GetRoomType(ID + 1);
            leaveTexts[numExits] = "You travel east.";
            connectedRooms[numExits] = ID + 1;
            buttonLocations[numExits, 0] = 350;
            buttonLocations[numExits, 1] = 36;
            numExits++;
        }
        if (CheckIfExit(i, j, 3)) {
            //can go south
            exitTexts[numExits] = "South: " + GetRoomType(ID + 100);
            leaveTexts[numExits] = "You travel south.";
            connectedRooms[numExits] = ID + 100;
            buttonLocations[numExits, 0] = 175;
            buttonLocations[numExits, 1] = -125;
            numExits++;
        }

        room.SetNumExits(numExits);
        room.SetButtonLocation(buttonLocations);
        room.SetExitTexts(exitTexts);
        room.SetLeaveTexts(leaveTexts);
        room.SetConnectedRooms(connectedRooms);

    }

    public bool CheckIfExit(int i, int j, int direction) {

        int ID = (j * 100) + i;

        //check for an exit going west
        if(direction == 0) {
            if (i != 0) {
                if(GetRoomType(ID - 1) == "Building") {
                    if(GetRoom(ID - 1).GetEntrance() == 2) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    return true;
                }
            }
            else {
                return false;
            }
        }
        else if(direction == 1) {//check if there's an exit going north
            if (j != 0) {
                if (GetRoomType(ID - 100) == "Building") {
                    if (GetRoom(ID - 100).GetEntrance() == 3) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    return true;
                }
            }
            else {
                return false;
            }
        }
        else if (direction == 2) {//check if there's an exit going east
            if (i != 99) {
                if (GetRoomType(ID + 1) == "Building") {
                    if (GetRoom(ID + 1).GetEntrance() == 0) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    return true;
                }
            }
            else {
                return false;
            }
        }
        else {//if there's an exit going south
            if (j != 59) {
                if (GetRoomType(ID + 100) == "Building") {
                    if (GetRoom(ID + 100).GetEntrance() == 1) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    return true;
                }
            }
            else {
                return false;
            }
        }

    }

    public bool CheckIfForestRoom(int index) {
        for(int i = 0; i < 2; i++) {
            if(index == castleRooms[i]) {
                return false;
            }
        }
        for(int i = 0; i < 23; i++) {
            if(index == plains[i]) {
                return false;
            }
        }

        return true;
    }

    public string GetRoomType(int ID) {
        return rooms[ID].GetRoomType();
    }

    public int[] GetRoomCoordinates(int ID) {
        return rooms[ID].GetRoomCoordinates();
    }

    public Room GetRoom(int index){
        return rooms[index];
    }

    public Room GetCurrentRoom(){
        return rooms[settingManager.GetComponent<SettingManager>().currentRoom];
    }

    public void SetRoomObserved(int roomID, int observed) {
        GetRoom(roomID).SetObserved(observed);
        if (observed == 1) {
            StartCoroutine(TimerUntilNotObserved(roomID));
        }
    }

    public void AddItemToRoom(int itemID, int roomID) {
        int[] resource = new int[2] { itemID, 1 };
        rooms[roomID].AddResource(resource);
    }

    public void RemoveItemFromRoom(int itemID, int roomID) {
        rooms[roomID].RemoveResource(itemID);
    }

    IEnumerator TimerUntilNotObserved(int roomID) {
        SettingManager sM = GameObject.Find("SettingManager").GetComponent<SettingManager>();
        int startMinute = sM.GetTotalMinutes();
        int endMinute = sM.GetTotalMinutes();
        while (endMinute < startMinute + 60) {
            yield return new WaitForSeconds(1f);
            endMinute = sM.GetTotalMinutes();
        }

        //after not observed remove all room options previously discovered
        Room room = GetRoom(roomID);
        room.ClearTempOptions();
        room.RemoveAllResources();
        room.SetObserved(0);
    }

    public void AddOptionToRoom(int roomID, int optionID) {
        Room tempRoom = GetRoom(roomID);

        //add the options
        if (tempRoom._options == null) {
            tempRoom._options = new int[5] {-1, -1, -1, -1, -1 };
            tempRoom._options[0] = optionID;
        }
        else {
            for (int i = 0; i < tempRoom._options.Length; i++) {
                if (tempRoom._options[i] == -1) {
                    tempRoom._options[i] = optionID;
                }
            }
        }
    }

    //called when tracking a deer which adds the deer trail to the next room
    public void AddTimedOptionToRoom(int roomID, int optionID, int time) {
        Room tempRoom = GetRoom(roomID);

        //add the options
        if(tempRoom._options == null) {
            tempRoom._options = new int[5] { -1, -1, -1, -1, -1 };
            tempRoom._options[0] = optionID;
        }
        else {
            for(int i = 0; i < tempRoom._options.Length; i++) {
                if(tempRoom._options[i] == -1) {
                    tempRoom._options[i] = optionID;
                    return;
                }
            }
        }

        StartCoroutine(RemoveOptionAfterTime(optionID, time, roomID));

    }
    //called by AddTimedOptionToRoom to remove the option after a certain time
    IEnumerator RemoveOptionAfterTime(int optionID, int minutes, int roomID) {

        SettingManager sM = settingManager.GetComponent<SettingManager>();
        Room room = GetRoom(roomID);

        //update total seconds passes until it reaches 60
        int startMinute = sM.GetTotalMinutes();
        int endMinute = sM.GetTotalMinutes();
        while (endMinute < (startMinute + minutes) && RoomContainsOption(roomID, optionID)) {
            yield return new WaitForSeconds(1);
            endMinute = sM.GetTotalMinutes();
        }

        RemoveOptionFromRoom(roomID, optionID);

    }
    //called from RemoveOptionAfterTime
    public void RemoveOptionFromRoom(int roomID, int optionID) {
         
        for (int i = 0; i < rooms[roomID]._options.Length; i++) {
            if (rooms[roomID]._options[i] == optionID) {
                rooms[roomID]._options[i] = -1;
            }
        }
    }

    public void RemoveTempOptionFromRoom(int roomID, int optionID) {
        rooms[roomID].RemoveTempOption(optionID);
    }

    public bool RoomContainsOption(int roomID, int optionID) {

        Room room = GetRoom(roomID);
        if(room._options == null) {
            return false;
        }
        for (int i = 0; i < room._options.Length; i++) {
            if (room._options[i] == optionID) {
                return true;
            }
        }

        return false;
    }

    public string GetRoomObservation(int roomID) {

        string observation;

        if (roomID == 1) {
            observation = "The courtyard was a perfect square surrounded by pillars holding up a stone ceiling. Inside the " +
            "pillars, a square setting of green grass with flowers and benches sat under the sky. ";
            string weather = settingManager.GetComponent<SettingManager>().GetWeather();
            if (weather == "sunny") {
                observation += "Everything radiated with sun light. ";
            }
            else if (weather == "rainy") {
                observation += "Rain poured through the opening and soaked into the ground. ";
            }
            else if (weather == "snowy") {
                observation += "Snow fell through the opening, covering everything in a thin blanket. ";
            }
            else if (weather == "windy") {
                observation += "Gusts of wind could be heard rushing past the opening in the ceiling. ";
            }

            observation += "On the other side of the courtyard was a large double doorway.";

            return observation;
        }
        else if(roomID == 2) {
            string weather = settingManager.GetComponent<SettingManager>().GetWeather();
            
            observation = "Candles flickered and lit up every inch of the giant room. ";
            if(weather == "sunny") {
                observation += "Large beams of sun light shot diagonally down from the high windows. ";
            }else if(weather == "rainy") {
                observation += "Rain drops slid down the high windows along the walls. ";
            }else if(weather == "misty") {
                observation += "Gray light from the misty sky bloomed from the high windows. ";
            }else if(weather == "snowy") {
                observation += "Large flakes of bright snow fell slowly outsde the high windows. ";
            }else if(weather == "windy") {
                observation += "Light beamed down through the high windows. ";
            }
            
            observation += "Food was laid out on all five of the long tables that extended from you down to the lords " +
            "table at the far end. Many were still in the middle of breakfast. The main entrance to the castle was an opened gate to the right of the lords table.";

            return observation;
        }
        else if(roomID == 4) {
            string weather = settingManager.GetComponent<SettingManager>().GetWeather();
            string text = "The castle wall, which seemed to be several paces thick, looms above you. A bridge extends across a wide river. The currents " +
            "are calm under the " + weather + " sky. " +
            "Two guards stand at the other side of the bridge. Their spears angle out toward the open field which disappeares behind the cover of trees.";
            return text;
        }
        else {
            return rooms[roomID].GetObservationText();
        }
    }
}
