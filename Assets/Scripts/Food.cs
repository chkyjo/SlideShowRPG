using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food{

    int _ID;
    Texture2D _icon;
    public string _name;
    public int _calories;
    public int _quantity;
    public int _weight;
    
    public Food(int ID, Texture2D icon, string name, int calories, int quantity, int weight){
        _ID = ID;
        _icon = icon;
        _name = name;
        _calories = calories;
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
