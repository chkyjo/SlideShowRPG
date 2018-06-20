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

    public Room(string name, int numOptions, string observationText, int numExits, int[] options, string[] exitTexts, string[] leaveTexts, int[] connectedRooms){
        _name = name;
        _numOptions = numOptions;
        _observationText = observationText;
        _numExits = numExits;
        _options = options;
        _exitTexts = exitTexts;
        _leaveTexts = leaveTexts;
        _connectedRooms = connectedRooms;
    }

    public Room(){

    }

    public string GetName() {
        return _name;
    }

}
