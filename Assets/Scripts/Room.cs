using UnityEngine;


public class Room{

    string _name;
    public int _numOptions;
    public string _observationText;
    public int _numExits;
    public int[] _options;
    public string[] _exitTexts;
    public string[] _leaveTexts;
    public int[] _connectedRooms;
    Texture2D roomImage;
    int[][] coords;

    public Room(string name, int numOptions, string observationText, int numExits, int[] options, string[] exitTexts, string[] leaveTexts, int[] connectedRooms){
        _name = name;
        _numOptions = numOptions;
        _observationText = observationText;
        _numExits = numExits;
        _options = options;
        _exitTexts = exitTexts;
        _leaveTexts = leaveTexts;
        _connectedRooms = connectedRooms;
        coords = new int[numExits][];
    }

    public Room(){

    }

    public string GetName() {
        return _name;
    }

    public void SetImage(Texture2D image) {
        roomImage = image;
    }

    public Texture2D GetRoomImage() {
        return roomImage;
    }

    public void SetCoord(int index, int x, int y) {
        coords[index] = new int[2];
        coords[index][0] = x;
        coords[index][1] = y;

    }

    public int[] GetCoord(int index) {
        return coords[index];
    }
}
