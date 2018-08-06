using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour {

    public GameObject settingManager;

    Room[] rooms = new Room[20];

    int[] publicLocations = new int[10];

    void Awake() {

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
        connectedRooms[0] = 1;
        rooms[0] = new Room("Bedchamber", 1, observationText, 1, options, exitTexts, leaveTexts, connectedRooms);
        rooms[0].SetImage(rawImg);
        rooms[0].SetCoord(0, -200, -50);

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
        connectedRooms[0] = 2;
        connectedRooms[1] = 0;
        connectedRooms[2] = 9;
        rooms[1] = new Room("Courtyard", 1, observationText, 3, options, exitTexts, leaveTexts, connectedRooms);
        rooms[1].SetImage(rawImg);
        rooms[1].SetCoord(0, -131, 203);
        rooms[1].SetCoord(1, 31, -185);
        rooms[1].SetCoord(2, 289, 68);

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
        connectedRooms[0] = 3;
        connectedRooms[1] = 1;
        rooms[2] = new Room("Main Hall", 2, observationText, 2, options, exitTexts, leaveTexts, connectedRooms);
        rooms[2].SetImage(rawImg);
        rooms[2].SetCoord(0, 225, 178);
        rooms[2].SetCoord(1, -128, -193);

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
        connectedRooms[0] = 4;
        connectedRooms[1] = 2;
        rooms[3] = new Room("Training Grounds", 0, observationText, 2, options, exitTexts, leaveTexts, connectedRooms);
        rooms[3].SetImage(rawImg);
        rooms[3].SetCoord(0, 202, 188);
        rooms[3].SetCoord(1, -222, -57);

        rawImg = Resources.Load("Bridge") as Texture2D;
        options = new int[0] { };
        observationText = "The Their spears angle out toward the open field which disappeares behind the cover of trees.";
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
        rooms[4].SetImage(rawImg);
        rooms[4].SetCoord(0, 240, 106);
        rooms[4].SetCoord(1, -257, 158);

        rawImg = Resources.Load("OpenField") as Texture2D;
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
        rooms[5].SetImage(rawImg);
        rooms[5].SetCoord(0, 240, 106);
        rooms[5].SetCoord(1, -257, 158);

        rawImg = Resources.Load("Forest") as Texture2D;
        options = new int[0] { };
        observationText = "Thick bundles of trees quickly hide the far edges of the forest. Only a faint trickle of light pierces the trees from the open field at the Protectors castle.";
        exitTexts = new string[4];
        exitTexts[0] = "Open field";
        exitTexts[1] = "North";
        exitTexts[2] = "East";
        exitTexts[3] = "South";
        leaveTexts = new string[4];
        leaveTexts[0] = "You step out of the woods into an open field.";
        leaveTexts[1] = "You travel north.";
        leaveTexts[2] = "You travel east.";
        leaveTexts[3] = "You travel south.";
        connectedRooms = new int[4];
        connectedRooms[0] = 5;
        connectedRooms[1] = 10;
        connectedRooms[2] = 11;
        connectedRooms[3] = 12;
        rooms[6] = new Room("Woods", 0, observationText, 4, options, exitTexts, leaveTexts, connectedRooms);
        rooms[6].SetImage(rawImg);
        rooms[6].SetCoord(0, -284, 42);
        rooms[6].SetCoord(1, 7, 205);
        rooms[6].SetCoord(2, 281, 42);
        rooms[6].SetCoord(3, 16, -195);

        options = new int[0] { };
        observationText = "It was deathly quiet. You were the only one in your cell. A couple of travelers were in the cell two cells down. Your cell was nothing but a box. Two of the walls were made of rock, the other two were iron bars.";
        exitTexts = new string[0];
        leaveTexts = new string[0];
        connectedRooms = new int[0];
        rooms[7] = new Room("Prison", 0, observationText, 0, options, exitTexts, leaveTexts, connectedRooms);

        rawImg = Resources.Load("Forest") as Texture2D;
        options = new int[0] { };
        observationText = "Very little light made it through the trees.";
        exitTexts = new string[0];
        leaveTexts = new string[0];
        connectedRooms = new int[0];
        rooms[8] = new Room("Deep Woods", 0, observationText, 0, options, exitTexts, leaveTexts, connectedRooms);
        

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
        connectedRooms[0] = 1;
        connectedRooms[1] = 9;
        connectedRooms[2] = 9;
        rooms[9] = new Room("Entrance Hall", 0, observationText, 3, options, exitTexts, leaveTexts, connectedRooms);
        rooms[9].SetImage(rawImg);
        rooms[9].SetCoord(0, -282, -15);
        rooms[9].SetCoord(1, -15, -147);
        rooms[9].SetCoord(2, 286, 10);

        rawImg = Resources.Load("Forest") as Texture2D;
        options = new int[0] { };
        observationText = "Thick bundles of trees quickly hide the far edges of the forest.";
        exitTexts = new string[4];
        exitTexts[0] = "Open field";
        exitTexts[1] = "North";
        exitTexts[2] = "East";
        exitTexts[3] = "South";
        leaveTexts = new string[4];
        leaveTexts[0] = "You travel west.";
        leaveTexts[1] = "You travel north.";
        leaveTexts[2] = "You travel east.";
        leaveTexts[3] = "You travel south.";
        connectedRooms = new int[4];
        connectedRooms[0] = 5;
        connectedRooms[1] = 10;
        connectedRooms[2] = 11;
        connectedRooms[3] = 6;
        rooms[10] = new Room("Woods", 0, observationText, 4, options, exitTexts, leaveTexts, connectedRooms);
        rooms[10].SetImage(rawImg);
        rooms[10].SetCoord(0, -284, 42);
        rooms[10].SetCoord(1, 7, 205);
        rooms[10].SetCoord(2, 281, 42);
        rooms[10].SetCoord(3, 16, -195);

        rawImg = Resources.Load("Forest") as Texture2D;
        options = new int[0] { };
        observationText = "Thick bundles of trees quickly hide the far edges of the forest. Only a faint trickle of light pierces the trees from the open field at the Protectors castle.";
        exitTexts = new string[4];
        exitTexts[0] = "Open field";
        exitTexts[1] = "North";
        exitTexts[2] = "East";
        exitTexts[3] = "South";
        leaveTexts = new string[4];
        leaveTexts[0] = "You travel west.";
        leaveTexts[1] = "You travel north.";
        leaveTexts[2] = "You travel east.";
        leaveTexts[3] = "You travel south.";
        connectedRooms = new int[4];
        connectedRooms[0] = 6;
        connectedRooms[1] = 10;
        connectedRooms[2] = 11;
        connectedRooms[3] = 12;
        rooms[11] = new Room("Woods", 0, observationText, 4, options, exitTexts, leaveTexts, connectedRooms);
        rooms[11].SetImage(rawImg);
        rooms[11].SetCoord(0, -284, 42);
        rooms[11].SetCoord(1, 7, 205);
        rooms[11].SetCoord(2, 281, 42);
        rooms[11].SetCoord(3, 16, -195);

        rawImg = Resources.Load("Forest") as Texture2D;
        options = new int[0] { };
        observationText = "Thick bundles of trees quickly hide the far edges of the forest. Only a faint trickle of light pierces the trees from the open field at the Protectors castle.";
        exitTexts = new string[4];
        exitTexts[0] = "Open field";
        exitTexts[1] = "North";
        exitTexts[2] = "East";
        exitTexts[3] = "South";
        leaveTexts = new string[4];
        leaveTexts[0] = "You travel west.";
        leaveTexts[1] = "You travel north.";
        leaveTexts[2] = "You travel east.";
        leaveTexts[3] = "You travel south.";
        connectedRooms = new int[4];
        connectedRooms[0] = 5;
        connectedRooms[1] = 6;
        connectedRooms[2] = 11;
        connectedRooms[3] = 12;
        rooms[12] = new Room("Woods", 0, observationText, 4, options, exitTexts, leaveTexts, connectedRooms);
        rooms[12].SetImage(rawImg);
        rooms[12].SetCoord(0, -284, 42);
        rooms[12].SetCoord(1, 7, 205);
        rooms[12].SetCoord(2, 281, 42);
        rooms[12].SetCoord(3, 16, -195);


        publicLocations[0] = 1;
        publicLocations[1] = 2;

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
