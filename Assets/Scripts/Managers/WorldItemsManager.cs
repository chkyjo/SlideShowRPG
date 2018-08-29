using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldItemsManager : MonoBehaviour {

    public TextAsset weaponListText;
    public TextAsset armorListText;
    public TextAsset foodListText;
    public TextAsset plantListText;

    List<Weapon> completeWeaponList;
    List<Armor> completeArmorList;
    List<Food> completeFoodList;
    List<Plant> completePlantList;

    int[] _guaranteedItemList;

    struct item {
        public int categoryId;
        public string name;
    }

    item[] _allItems;

    void Awake() {
        completeWeaponList = new List<Weapon>();
        completeArmorList = new List<Armor>();
        completeFoodList = new List<Food>();
        completePlantList = new List<Plant>();

        _allItems = new item[200];
        _allItems[100].name = "Pivra";
        _allItems[100].categoryId = 0;
        _allItems[101].name = "Alacia";
        _allItems[101].categoryId = 1;
        _allItems[102].name = "Povacis";
        _allItems[102].categoryId = 2;
        _guaranteedItemList = new int[3] { 100, 101, 102 };
        InitItemLists();
    }

    void Start() {

    }

    public string GetItemString(int index) {
        return _allItems[index].name;
    }

    private void InitItemLists() {

        string itemListString = weaponListText.text;
        string[] arrayOfLines = itemListString.Split('\n');
        string[] splitLine;
        int ID;
        Texture2D icon;
        int damage;
        int quantity;
        int weight;
        Weapon tempWeapon;

        for (int i = 0; i < arrayOfLines.Length; i++) {
            splitLine = arrayOfLines[i].Split(' ');
            ID = Convert.ToInt16(splitLine[0]);
            damage = Convert.ToInt16(splitLine[2]);
            quantity = Convert.ToInt16(splitLine[3]);
            weight = Convert.ToInt16(splitLine[4]);
            icon = Resources.Load(splitLine[1]) as Texture2D;
            tempWeapon = new Weapon(ID, icon, splitLine[1], damage, quantity, weight);
            Debug.Log("Weapon added");
            completeWeaponList.Add(tempWeapon);
        }

        itemListString = armorListText.text;
        arrayOfLines = itemListString.Split('\n');
        int defense;
        Armor tempArmor;

        for (int i = 0; i < arrayOfLines.Length; i++) {

            splitLine = arrayOfLines[i].Split(' ');
            ID = Convert.ToInt16(splitLine[0]);
            defense = Convert.ToInt16(splitLine[2]);
            quantity = Convert.ToInt16(splitLine[3]);
            weight = Convert.ToInt16(splitLine[4]);
            icon = Resources.Load(splitLine[1]) as Texture2D;
            tempArmor = new Armor(ID, icon, splitLine[1], defense, quantity, weight);
            completeArmorList.Add(tempArmor);
        }

        itemListString = foodListText.text;
        arrayOfLines = itemListString.Split('\n');
        int calories;
        Food tempFood;

        for (int i = 0; i < arrayOfLines.Length; i++) {

            splitLine = arrayOfLines[i].Split(' ');
            ID = Convert.ToInt16(splitLine[0]);
            calories = Convert.ToInt16(splitLine[2]);
            quantity = Convert.ToInt16(splitLine[3]);
            weight = Convert.ToInt16(splitLine[4]);
            icon = Resources.Load(splitLine[1]) as Texture2D;
            tempFood = new Food(ID, icon, splitLine[1], calories, quantity, weight);
            completeFoodList.Add(tempFood);
        }

        itemListString = plantListText.text;
        arrayOfLines = itemListString.Split('\n');
        Plant tempPlant;

        for (int i = 0; i < arrayOfLines.Length; i++) {

            splitLine = arrayOfLines[i].Split(' ');
            ID = Convert.ToInt16(splitLine[0]);
            icon = Resources.Load(splitLine[1]) as Texture2D;
            weight = Convert.ToInt16(splitLine[2]);
            tempPlant = new Plant(ID, icon, splitLine[1], weight);
            completePlantList.Add(tempPlant);
        }
    }

    public void AddItemToInventory(int worldID) {
        int categoryID = _allItems[worldID].categoryId;

        //if the worldId is greater than 99 then it is in the plant category
        if(worldID > 99) {
            Debug.Log("Plant added");
            GameObject.Find("InventoryManager").GetComponent<InventoryManager>().AddPlant(completePlantList[categoryID]);
        }
    }

    //returns the list of items where if the player finds resources in a room they are
    //guaranteed to at least get one of these
    public int[] GetGuaranteedItems() {
        return _guaranteedItemList;
    }

    public Weapon GetWeapon(int index) {
        return completeWeaponList[index];
    }
    public Armor GetArmor(int index) {
        return completeArmorList[index];
    }
    public Food GetFood(int index) { 
        return completeFoodList[index];
    }
    public Plant GetPlant(int index) {
        return completePlantList[index];
    }
}
