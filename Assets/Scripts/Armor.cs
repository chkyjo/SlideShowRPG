using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor{

    public int _ID;
    Texture2D _icon;
    public string _name;
    public int _defense;
    public int _quantity;
    public int _weight;

    public Armor(int ID, Texture2D icon, string name, int defense, int quantity, int weight){
        _ID = ID;
        _icon = icon;
        _name = name;
        _defense = defense;
        _quantity = quantity;
        _weight = weight;
    }

    public Texture2D GetIcon()
    {
        return _icon;
    }

    public int GetID() {
        return _ID;
    }
}
