using UnityEngine;

public class RoomManager : MonoBehaviour {

    public GameObject settingManager;

    Room[] rooms = new Room[10];

    int[] publicLocations = new int[10];

    void Awake() {
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
        rooms[0] = new Room("Bedchamber", 1, observationText, 1, options, exitTexts, leaveTexts, connectedRooms);

        options = new int[1] { 6 };
        observationText = "The courtyard was a perfect square surrounded by pillars holding up a stone ceiling. Inside the " +
            "pillars was a square field of green grass with flowers and benches. On the other side of the courtyard was a " +
            "large double doorway.";
        exitTexts = new string[2];
        exitTexts[0] = "Enter through the double doors";
        exitTexts[1] = "Down stairs to bed chamber";
        leaveTexts = new string[2];
        leaveTexts[0] = "You lean into the double doors with your small frame and gradually it opens. The great hall appears before you.";
        leaveTexts[1] = "You head down the stairs back to your bed chamber.";
        connectedRooms = new int[2];
        connectedRooms[0] = 2;
        connectedRooms[1] = 0;
        rooms[1] = new Room("Courtyard", 1, observationText, 2, options, exitTexts, leaveTexts, connectedRooms);

        options = new int[2] { 6, 7 };
        observationText = "Candles flickered and lit up every inch of the giant room. Large beams of sunlight shot diagonally down " +
            "from the high windows. Food was laid out on all five of the long tables that extended from you down to the lords " +
            "table at the far end. Many were still in the middle of breakfast. The main entrance to the castle was an opened gate to the right of the lords table.";
        exitTexts = new string[2];
        exitTexts[0] = "Main entrance";
        exitTexts[1] = "Double doors to courtyard";
        leaveTexts = new string[2];
        leaveTexts[0] = "You walk over to the main entrance and pass two guards on your way out. The sun is blinding as you step out into the open air.";
        leaveTexts[1] = "You push your way through the double doors to the courtyard.";
        connectedRooms = new int[2];
        connectedRooms[0] = 3;
        connectedRooms[1] = 1;
        rooms[2] = new Room("Main Hall", 2, observationText, 2, options, exitTexts, leaveTexts, connectedRooms);

        options = new int[0] { };
        observationText = "A sloped, curvy walkway led down from the castle to a lower open field. Men, women, and children were running and parrying " +
            "with each other. Distant yells and shouts echoed through the sky. In the distance you could see Gregory barking commands at the other " +
            "recruits you had become familiar with.";
        exitTexts = new string[2];
        exitTexts[0] = "Archway";
        exitTexts[1] = "Into the castle";
        leaveTexts = new string[2];
        leaveTexts[0] = "You cross the training grounds to an archway under the castle walls and walk under it.";
        leaveTexts[1] = "You walk over to the main entrance and pass two guards on your way in. You are hit with the soft candle light and the smell of food.";
        connectedRooms = new int[2];
        connectedRooms[0] = 4;
        connectedRooms[1] = 2;

        rooms[3] = new Room("Training Grounds", 0, observationText, 2, options, exitTexts, leaveTexts, connectedRooms);

        string weather = settingManager.GetComponent<SettingManager>().GetWeather();
        options = new int[0] { };
        observationText = "The castle wall, which seemed to be several paces thick, looms above you. A bridge extends across a wide river. The currents " +
            "are calm under the " + weather + " sky. " +
            "Two guards stand at the other side of the bridge. Their spears angle out toward the open field which disappeares behind the cover of trees.";
        exitTexts = new string[2];
        exitTexts[0] = "Open field";
        exitTexts[1] = "Training grounds";
        leaveTexts = new string[2];
        leaveTexts[0] = "You cross over the narrow, stone, bridge.";
        leaveTexts[1] = "You walk under the arch way through the castle walls and stumble into the training grounds.";
        connectedRooms = new int[2];
        connectedRooms[0] = 5;
        connectedRooms[1] = 3;
        rooms[4] = new Room("Bridge", 0, observationText, 2, options, exitTexts, leaveTexts, connectedRooms);

        options = new int[0] { };
        observationText = "Wide open fields spread out in all directions. The massive castle of the Protectors stood towering over the trees behind the river traveling north and south.";
        exitTexts = new string[2];
        exitTexts[0] = "Woods";
        exitTexts[1] = "Bridge";
        leaveTexts = new string[2];
        leaveTexts[0] = "You approach the woods. The darkness under the leaves doesn't seem to pull away as you get closer.";
        leaveTexts[1] = "You cross the field to the stone bridge.";
        connectedRooms = new int[2];
        connectedRooms[0] = 6;
        connectedRooms[1] = 4;
        rooms[5] = new Room("Open field", 0, observationText, 2, options, exitTexts, leaveTexts, connectedRooms);

        options = new int[0] { };
        observationText = "Thick bundles of trees quickly hide the far edges of the forest. Only a faint trickle of light pierces the trees from the open field at the Protectors castle.";
        exitTexts = new string[1];
        exitTexts[0] = "Open field";
        leaveTexts = new string[1];
        leaveTexts[0] = "You step out of the woods into an open field.";
        connectedRooms = new int[1];
        connectedRooms[0] = 5;
        rooms[6] = new Room("Woods", 0, observationText, 1, options, exitTexts, leaveTexts, connectedRooms);

        options = new int[0] { };
        observationText = "It was deathly quiet. You were the only one in your cell. A couple of travelers were in the cell two cells down. Your cell was nothing but a box. Two of the walls were made of rock, the other two were iron bars.";
        exitTexts = new string[0];
        leaveTexts = new string[0];
        connectedRooms = new int[0];
        rooms[7] = new Room("Prison", 0, observationText, 0, options, exitTexts, leaveTexts, connectedRooms);




        publicLocations[0] = 1;
        publicLocations[1] = 2;

    }

    // Use this for initialization
    void Start () {
        
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

    public string GetRoomObservation(int roomID) {

        string observation;

        if (roomID == 1) {
            observation = "The courtyard was a perfect square surrounded by pillars holding up a stone ceiling. Inside the " +
            "pillars, a square setting of green grass with flowers and benches sat under the sky. ";
            string weather = settingManager.GetComponent<SettingManager>().GetWeather();
            Debug.Log(weather);
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
            return rooms[roomID]._observationText;
        }
    }
}
