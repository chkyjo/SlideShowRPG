using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour{

    string _name;
    string _type;
    public int _numOptions;
    string _observationText;
    public int _numExits;
    public int[] _options;
    int[] _tempOptions;
    public string[] _exitTexts;
    public string[] _leaveTexts;
    public int[] _connectedRooms;
    Texture2D roomImage;
    int[,] _buttonPositions;
    int[] _roomCoordinates = { 0, 0 };
    int _teleportTo;
    int _entrance;
    int _observed;
    List<int[]> _resources = new List<int[]>();

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

    public void SetName(string name) {
        _name = name;
    }
    public void SetType(string type) {
        _type = type;
    }
    public void SetImage(Texture2D image) {
        roomImage = image;
    }
    public void SetObservationText(string text) {
        _observationText = text;
    }
    public void SetRoomCoordinates(int x, int y) {
        _roomCoordinates[0] = x;
        _roomCoordinates[1] = y;
    }
    public void SetButtonLocation(int[,] locations) {
        _buttonPositions = locations;

    }
    public void SetExitTexts(string[] exitTexts) {
        _exitTexts = exitTexts;
    }
    public void SetLeaveTexts(string[] leaveTexts) {
        _leaveTexts = leaveTexts;
    }
    public void SetNumExits(int num) {
        _numExits = num;
    }
    public void SetConnectedRooms(int[] rooms) {
        _connectedRooms = rooms;
    }
    public void SetObservation(string obs) {
        _observationText = obs;
    }
    public void SetTeleport(int ID) {
        _teleportTo = ID;
    }
    public void SetEntrance(int entrance) {
        _entrance = entrance;
    }
    public void SetObserved(int observed) {
        _observed = observed;
    }
    public void AddResource(int[] resource) {
        if(_resources.Count == 0) {
            _resources.Add(resource);
            return;
        }
        for(int i = 0; i < _resources.Count; i++) {
            if(_resources[i][0] == resource[0]) {
                _resources[i][1] += resource[1];
                return;
            }
        }
        _resources.Add(resource);

    }
    public void RemoveResource(int resourceID) {
        if (_resources.Count == 0) {
            Debug.Log("Attempted to remove resource from empty list of resources");
            return;
        }
        for (int i = 0; i < _resources.Count; i++) {
            if (_resources[i][0] == resourceID) {
                _resources.RemoveAt(i);
                return;
            }
        }
    }
    public List<int[]> GetResources() {
        return _resources;
    }
    public void RemoveAllResources() {
        _resources.Clear();
    }
    //options cleared after the observed stat expires
    public void AddTempOption(int optionID) {
        if(_tempOptions == null) {
            _tempOptions = new int[5] {-1, -1, -1, -1, -1 };
            _tempOptions[0] = optionID;
            return;
        }
        for(int i = 0; i < _tempOptions.Length; i++) {
            if(_tempOptions[i] == -1) {
                _tempOptions[i] = optionID;
                return;
            }
        }
    }
    public int[] GetTempOptions() {
        return _tempOptions;
    }
    public void RemoveTempOption(int optionID) {
        if (_tempOptions == null) {
            return;
        }
        for (int i = 0; i < _tempOptions.Length; i++) {
            if (_tempOptions[i] == optionID) {
                _tempOptions[i] = -1;
                return;
            }
        }
    }
    public void ClearTempOptions() {
        if(_tempOptions == null) {
            return;
        }
        for(int i = 0; i < _tempOptions.Length; i++) {
            _tempOptions[i] = -1;
        }
    }

    public string GetName() {
        return _name;
    }
    public string GetRoomType() {
        return _type;
    }
    public int GetNumExits() {
        return _numExits;
    }
    public string GetObservationText() {
        return _observationText;
    }
    public Texture2D GetRoomImage() {
        return roomImage;
    }
    public int[,] GetButtonLocation() {
        return _buttonPositions;
    }
    public int[] GetRoomCoordinates() {
        return _roomCoordinates;
    }
    public int GetTeleport() {
        return _teleportTo;
    }
    public int GetEntrance() {
        return _entrance;
    }
    public int GetObserved() {
        return _observed;
    }
}
