using UnityEngine;

//[System.Serializable]
public class Weapon{

    public int _ID;
    Texture2D _icon;
    public string _name;
    public int _damage;
    public int _quantity;
    public int _weight;

    public Weapon(int ID, Texture2D icon, string name, int damage, int quantity, int weight){
        _ID = 0;
        _icon = icon;
        _name = name;
        _damage = damage;
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
