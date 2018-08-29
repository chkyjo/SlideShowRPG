using UnityEngine;

public class Plant : MonoBehaviour {

    private int _ID;
    private Texture2D _icon;
    private string _name;
    private int _weight;

    public Plant(int ID, Texture2D icon, string name, int weight) {
        _ID = ID;
        _icon = icon;
        _name = name;
        _weight = weight;
    }
    public Plant() {

    }

    public int ID {
        get {
            return _ID;
        }

        set {
            _ID = value;
        }
    }

    public Texture2D Icon {
        get {
            return _icon;
        }

        set {
            _icon = value;
        }
    }

    public string Name {
        get {
            return _name;
        }

        set {
            _name = value;
        }
    }

    public int Weight {
        get {
            return _weight;
        }

        set {
            _weight = value;
        }
    }
}
