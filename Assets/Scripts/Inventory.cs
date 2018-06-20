using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Inventory : MonoBehaviour {

    public GameObject gameManager;

    public TextAsset weaponListText;
    public TextAsset armorListText;
    public TextAsset foodListText;

    private List<Weapon> completeWeaponList = new List<Weapon>();
    private List<Armor> completeArmorList = new List<Armor>();
    private List<Food> completeFoodList = new List<Food>();

    private List<Weapon> weaponInventory = new List<Weapon>();
    private List<Armor> armorInventory = new List<Armor>();
    private List<Food> foodInventory = new List<Food>();

    public GameObject itemsPanel;
    public GameObject itemScroll;

    public RectTransform scroll;
    public GameObject item;
    public GameObject foodInventoryItem;
    public Scrollbar scrollBar;
    public Text userFeedback;

    int itemsInItemPanel;

	// Use this for initialization
	void Start () {

        InitItemLists();

        AddWeaponToInventory(0);

        AddArmorToInventory(0);
        AddArmorToInventory(1);
        AddArmorToInventory(2);

        AddFoodToInventory(0);
        AddFoodToInventory(1);
        AddFoodToInventory(5);
        AddFoodToInventory(6);

        //zero items on display
        itemsInItemPanel = 0;

    }

    private void InitItemLists(){

        string itemListString = weaponListText.text;
        string[] arrayOfLines = itemListString.Split('\n');
        string[] splitLine;
        int ID;
        Texture2D icon;
        int damage;
        int quantity;
        int weight;

        for (int i = 0; i < arrayOfLines.Length; i++){

            splitLine = arrayOfLines[i].Split(' ');
            ID = Convert.ToInt16(splitLine[0]);
            damage = Convert.ToInt16(splitLine[2]);
            quantity = Convert.ToInt16(splitLine[3]);
            weight = Convert.ToInt16(splitLine[4]);
            icon = Resources.Load(splitLine[1]) as Texture2D;
            Weapon tempWeapon = new Weapon(ID, icon, splitLine[1], damage, quantity, weight);
            completeWeaponList.Add(tempWeapon);
        }

        itemListString = armorListText.text;
        arrayOfLines = itemListString.Split('\n');
        int defense;
        for (int i = 0; i < arrayOfLines.Length; i++){

            splitLine = arrayOfLines[i].Split(' ');
            ID = Convert.ToInt16(splitLine[0]);
            defense = Convert.ToInt16(splitLine[2]);
            quantity = Convert.ToInt16(splitLine[3]);
            weight = Convert.ToInt16(splitLine[4]);
            icon = Resources.Load(splitLine[1]) as Texture2D;
            Armor tempArmor = new Armor(ID, icon, splitLine[1], defense, quantity, weight);
            completeArmorList.Add(tempArmor);
        }

        itemListString = foodListText.text;
        arrayOfLines = itemListString.Split('\n');
        int calories;
        for (int i = 0; i < arrayOfLines.Length; i++){

            splitLine = arrayOfLines[i].Split(' ');
            ID = Convert.ToInt16(splitLine[0]);
            calories = Convert.ToInt16(splitLine[2]);
            quantity = Convert.ToInt16(splitLine[3]);
            weight = Convert.ToInt16(splitLine[4]);
            icon = Resources.Load(splitLine[1]) as Texture2D;
            Food tempFood = new Food(ID, icon, splitLine[1], calories, quantity, weight);
            completeFoodList.Add(tempFood);
        }
    }

    public int[] GetInventoryCounts(){
        int[] counts = { 0, 0, 0, 0 };

        counts[0] = weaponInventory.Count;
        counts[1] = armorInventory.Count;
        counts[2] = foodInventory.Count;
        counts[3] = weaponInventory.Count + armorInventory.Count + foodInventory.Count;

        return counts;
    }

    public int GetFoodCount(){
        return foodInventory.Count;
    }

    public Weapon GetWeapon(int i){
        return weaponInventory[i];
    }

    public Armor GetArmor(int i){
        return armorInventory[i];
    }

    public Food GetFood(int i){
        return foodInventory[i];
    }

    public void DisplayWeapons(){

        //clear the panel of previously displayed items
        for (int i = 0; i < itemScroll.transform.childCount; i++){
            Destroy(itemScroll.transform.GetChild(i).gameObject);
        }
        //get the number of items to display and instantiate that many item objects
        int numWeapons = weaponInventory.Count;
        itemsInItemPanel = numWeapons;
        gameManager.GetComponent<GameManager>().itemsOnDisplay = numWeapons;
        if (numWeapons != 0){
            for (int i = 0; i < numWeapons; i++){
                item.GetComponentInChildren<Text>().text = weaponInventory[i]._name;

                Texture2D tex = weaponInventory[i].GetIcon();
                if (tex != null){
                    item.GetComponentInChildren<RawImage>().texture = tex;
                }

                var newObj = GameObject.Instantiate(item);
                newObj.transform.SetParent(itemScroll.transform, false);
            }
            //reset the scrollbar and resize the scroll to fit list
            scrollBar.value = 1;
            if (itemsInItemPanel > 7){
                int difference = itemsInItemPanel - 7;
                scroll.offsetMin = new Vector2(scroll.offsetMin.x, -(difference * 50));
            }
            else
            {
                scroll.offsetMin = new Vector2(scroll.offsetMin.x, 0);
            }
        }
        else{
            userFeedback.text = "No items to display";
            var alertText = GameObject.Instantiate(userFeedback);
            alertText.transform.SetParent(itemScroll.transform, false);
        }
        
    }

    public void DisplayArmor(){

        for (int i = 0; i < itemScroll.transform.childCount; i++) {
            Destroy(itemScroll.transform.GetChild(i).gameObject);
        }

        int numArmor = armorInventory.Count;
        itemsInItemPanel = numArmor;
        gameManager.GetComponent<GameManager>().itemsOnDisplay = numArmor;
        if (numArmor != 0){
            for (int i = 0; i < numArmor; i++){
                item.GetComponentInChildren<Text>().text = armorInventory[i]._name;
                Texture2D tex = armorInventory[i].GetIcon();
                if (tex != null){
                    item.GetComponentInChildren<RawImage>().texture = tex;
                }
                var newObj = GameObject.Instantiate(item);
                newObj.transform.SetParent(itemScroll.transform, false);
            }

            scrollBar.value = 1;
            if (itemsInItemPanel > 7){
                int difference = itemsInItemPanel - 7;
                scroll.offsetMin = new Vector2(scroll.offsetMin.x, -(difference * 50));
            }
            else{
                scroll.offsetMin = new Vector2(scroll.offsetMin.x, 0);
            }
        }
        else{
            userFeedback.text = "No items to display";
            var alertText = GameObject.Instantiate(userFeedback);
            alertText.transform.SetParent(itemScroll.transform, false);
        }
    }

    public void DisplayFood(){
        for (int i = 0; i < itemScroll.transform.childCount; i++) {
            Destroy(itemScroll.transform.GetChild(i).gameObject);
        }

        int numFood = foodInventory.Count;
        itemsInItemPanel = numFood;
        gameManager.GetComponent<GameManager>().itemsOnDisplay = numFood;
        if (numFood != 0){
            for (int i = 0; i < numFood; i++){
                foodInventoryItem.GetComponentInChildren<Text>().text = foodInventory[i]._name;
                foodInventoryItem.GetComponent<Item>().ID = foodInventory[i]._ID;
                Texture2D tex = foodInventory[i].GetIcon();
                if (tex != null){
                    foodInventoryItem.GetComponentInChildren<RawImage>().texture = tex;
                }
                var newObj = GameObject.Instantiate(foodInventoryItem);
                newObj.transform.SetParent(itemScroll.transform, false);
            }
            //reset scrollbar position
            scrollBar.value = 1;
            //if items overflow scroll area get number of overflowing items to calculate how far to extend
            if (itemsInItemPanel > 7){
                int difference = itemsInItemPanel - 7;
                scroll.offsetMin = new Vector2(scroll.offsetMin.x, -(difference * 50));
            }
            else{//else reset
                scroll.offsetMin = new Vector2(scroll.offsetMin.x, 0);
            }
        }
        else{
            userFeedback.text = "No items to display";
            var alertText = GameObject.Instantiate(userFeedback);
            alertText.transform.SetParent(itemScroll.transform, false);
        }
    }

    //compares weapons to weapons; armor to armor; and food to food by comparing string names
    bool IsEqual(Weapon weapon1, Weapon weapon2){
        if (weapon1._name == weapon2._name){
            return true;
        }
        else{
            return false;
        }
    }
    bool IsEqual(Armor armor1, Armor armor2){
        if(armor1._name == armor2._name){
            return true;
        }
        else{
            return false;
        }
    }
    bool IsEqual(Food food1, Food food2){
        if (food1._name == food2._name){
            return true;
        }
        else{
            return false;
        }
    }

    public void AddWeaponToInventory(int ID){

        bool quantityIncreased = false;
        //create item
        Weapon weapon = completeWeaponList[ID];
        //if item is already in the list increment the quantity
        for (int i = 0; i < weaponInventory.Count; i++){
            if (IsEqual(weapon, weaponInventory[i])){
                weaponInventory[i]._quantity++;
                quantityIncreased = true;
                break;
            }
        }
        //if not add new item to list
        if (quantityIncreased == false){
            weaponInventory.Add(weapon);
        }

    }

    public void AddArmorToInventory(int ID){

        bool quantityIncreased = false;
        //create item
        Armor armor = completeArmorList[ID];

        //if item is already in the list increment the quantity
        for (int i = 0; i < armorInventory.Count; i++)
        {
            if (IsEqual(armor, armorInventory[i]))
            {
                armorInventory[i]._quantity++;
                quantityIncreased = true;
                break;
            }
        }
        //if not add new item to list
        if (quantityIncreased == false)
        {
            armorInventory.Add(armor);
        }

    }

    public void AddFoodToInventory(int ID){

        bool quantityIncreased = false;
        //create item
        Food food = completeFoodList[ID];
        //if item is already in the list increment the quantity
        for (int i = 0; i < foodInventory.Count; i++)
        {
            if (IsEqual(food, foodInventory[i]))
            {
                foodInventory[i]._quantity++;
                quantityIncreased = true;
                break;
            }
        }
        //if not add new item to list
        if (quantityIncreased == false)
        {
            foodInventory.Add(food);
        }

    }

    public void RemoveWeaponItem(string name, int quantity){

        //if item is in the list
        for (int i = 0; i < weaponInventory.Count; i++){
            if (name == weaponInventory[i]._name){
                weaponInventory[i]._quantity -= quantity;
                if (weaponInventory[i]._quantity <= 0){
                    weaponInventory.RemoveAt(i);
                }
                return;
            }
        }

        //if item was not found
        Debug.Log("Item was not found");
    }

    public void RemoveArmorItem(string name, int quantity){

        //if item is in the list
        for (int i = 0; i < armorInventory.Count; i++){
            if (name == armorInventory[i]._name){
                armorInventory[i]._quantity -= quantity;
                if (armorInventory[i]._quantity <= 0){
                    armorInventory.RemoveAt(i);
                }
                return;
            }
        }

        //if item was not found
        Debug.Log("Item was not found");
    }

    public void RemoveFoodItem(string name, int quantity){

        //if item is in the list
        for (int i = 0; i < foodInventory.Count; i++){
            if (name == foodInventory[i]._name){
                foodInventory[i]._quantity -= quantity;
                if(foodInventory[i]._quantity <= 0){
                    foodInventory.RemoveAt(i);
                }
                return;
            }
        }

        //if item was not found
        Debug.Log("Item was not found");
    }

    public void DeleteItem(string name){
        for(int i = 0; i < weaponInventory.Count; i++)
        {
            if(weaponInventory[i]._name == name)
            {
                weaponInventory.RemoveAt(i);
                DisplayWeapons();
            }
        }

        for (int i = 0; i < armorInventory.Count; i++)
        {
            if (armorInventory[i]._name == name)
            {
                armorInventory.RemoveAt(i);
                DisplayArmor();
            }
        }

        for (int i = 0; i < foodInventory.Count; i++)
        {
            if (foodInventory[i]._name == name)
            {
                foodInventory.RemoveAt(i);
                DisplayFood();
            }
        }
    }

    public void DeleteFoodItem(string name){
        for (int i = 0; i < foodInventory.Count; i++)
        {
            if (foodInventory[i]._name == name)
            {
                foodInventory.RemoveAt(i);
                DisplayFood();
            }
        }
    }

    public void DeleteFoodItem(int ID) {
        for (int i = 0; i < foodInventory.Count; i++) {
            if (foodInventory[i]._ID == ID) {
                foodInventory.RemoveAt(i);
                DisplayFood();
            }
        }
    }

    public Food GetFoodByID(int ID) {
        for (int i = 0; i < foodInventory.Count; i++) {
            if (foodInventory[i]._ID == ID) {
                return foodInventory[i];
            }
        }
        return foodInventory[0];
    }

    public void ClickRemoveWeaponItem(string name, int quantity){

        //if item is in the list
        for (int i = 0; i < weaponInventory.Count; i++)
        {
            if (name == weaponInventory[i]._name)
            {
                weaponInventory[i]._quantity -= quantity;
                if (weaponInventory[i]._quantity <= 0)
                {
                    weaponInventory.RemoveAt(i);
                }
                return;
            }
        }

        //if item was not found
        Debug.Log("Item was not found");
    }

    public void ClickRemoveArmorItem(string name, int quantity){

        //if item is in the list
        for (int i = 0; i < armorInventory.Count; i++)
        {
            if (name == armorInventory[i]._name)
            {
                armorInventory[i]._quantity -= quantity;
                if (armorInventory[i]._quantity <= 0)
                {
                    armorInventory.RemoveAt(i);
                }
                return;
            }
        }

        //if item was not found
        Debug.Log("Item was not found");
    }

    public void ClickRemoveFoodItem(string name, int quantity){

        //if item is in the list
        for (int i = 0; i < foodInventory.Count; i++)
        {
            if (name == foodInventory[i]._name)
            {
                foodInventory[i]._quantity -= quantity;
                if (foodInventory[i]._quantity <= 0)
                {
                    foodInventory.RemoveAt(i);
                }
                return;
            }
        }

        //if item was not found
        Debug.Log("Item was not found");
    }

    public void EatFoodItem(int ID){
        GameObject playerManager = GameObject.Find("PlayerManager");
        Food tempFood = GetFoodByID(ID);
        playerManager.GetComponent<PlayerManager>().calories += tempFood._calories;
        DeleteFoodItem(ID);
        playerManager.GetComponent<PlayerManager>().UpdateCalories();
    }
}