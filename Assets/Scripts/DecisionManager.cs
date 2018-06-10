using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionManager : MonoBehaviour {

    public GameObject inventoryManager;
    public GameObject settingManager;

    public GameObject decisionPanel;
    public GameObject decisionScroller;
    public RectTransform decisionScroll;
    public Scrollbar scrollBar;

    public GameObject throwDecisionObject;
    public GameObject throwItemDecisionObject;
    public GameObject throwItemAtDecisionObject;
    public GameObject talkDecisionObject;
    public GameObject talkToDecisionObject;
    public GameObject observeDecisionObject;
    public GameObject nextRoomDecisionObject;
    public GameObject waitInBedDecisionObject;

    public Slider slider;
    public GameObject fadOutPanel;
    public GameObject sleepMenuBackgroundPanel;

    Room[] rooms = new Room[10];

    struct Room{
        public int numDecisions;
        public int numExits;
        public int[] otherOptions;
        public string[] exitTexts;
    }

    public Text mainText;
    int itemsInDecisionPanel;

	// Use this for initialization
	void Start () {
        itemsInDecisionPanel = 0;
        rooms[0].numDecisions = 2;
        rooms[0].numExits = 1;
        rooms[0].exitTexts = new string[1];
        rooms[0].exitTexts[0] = "Continue up the stairs";
        rooms[0].otherOptions = new int[1] {5};
        FillDecisionPanel();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //fill decision panel based on inventory and scene
    private void FillDecisionPanel(){
        //if there is something in the players inventory and there are people to throw things at add the throw action
        if(inventoryManager.GetComponent<Inventory>().GetInventoryCounts()[3] > 0 && settingManager.GetComponent<SettingManager>().numCharactersInSetting > 0){
            AddDecision(0);
        }
        //if there are people in the area add the talk action
        if (settingManager.GetComponent<SettingManager>().numCharactersInSetting > 0){
            AddDecision(1);
        }

        //add the observe action
        AddDecision(2);

        //if there are living things in the area add the attack action
        if (settingManager.GetComponent<SettingManager>().numCharactersInSetting > 0){
            AddDecision(3);
        }

        //if there are item up for grabs add the take action


        //if there are items available to steal add the steal action

        

        //add actions based on the room
        AddExitOptions();
        AddRoomOptions();

    }

    public void CombatDecisions(){



    }

    //add action to panel
    void AddDecision(int index){

        if(index == 0){
            var obj = GameObject.Instantiate(throwDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        if (index == 1){
            var obj = GameObject.Instantiate(talkDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        if(index == 2){
            var obj = GameObject.Instantiate(observeDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        /*
        if (index == 3){
            talkDecisionObject.GetComponentInChildren<Text>().text = "Attack someone";
            var obj = GameObject.Instantiate(talkDecisionObject);
            //obj.transform.parent = decisionScroller.transform;
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
            Debug.Log("Added attack action");
        }
        */
        if (index == 5){
            var obj = GameObject.Instantiate(waitInBedDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }

        scrollBar.value = 1;
        decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, 0);

    }

    void AddDecision(int index, string newText){
        if (index == 4){
            nextRoomDecisionObject.GetComponentsInChildren<Text>()[1].text = newText;
            var obj = GameObject.Instantiate(nextRoomDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
    }

    void AddRoomOptions(){
        int roomIndex = settingManager.GetComponent<SettingManager>().GetRoom();
        for (int i = 0; i < rooms[roomIndex].otherOptions.Length; i++){
            AddDecision(rooms[roomIndex].otherOptions[i]);
        }
    }

    void AddExitOptions(){
        int roomIndex = settingManager.GetComponent<SettingManager>().GetRoom();
        for (int i = 0; i < rooms[roomIndex].numExits; i++){
            AddDecision(4, rooms[roomIndex].exitTexts[i]);
        }
    }

    //called when user selects the throw action
    public void Throw(){

        ClearDecisionPanel();

        //display available items to throw
        Inventory tempInventory = inventoryManager.GetComponent<Inventory>();
        for (int i = 0; i < tempInventory.GetInventoryCounts()[0]; i++){
            throwItemDecisionObject.GetComponentsInChildren<Text>()[1].text = tempInventory.GetWeapon(i)._name;
            var obj = GameObject.Instantiate(throwItemDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        for (int i = 0; i < tempInventory.GetInventoryCounts()[1]; i++){
            throwItemDecisionObject.GetComponentsInChildren<Text>()[1].text = tempInventory.GetArmor(i)._name;
            var obj = GameObject.Instantiate(throwItemDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        for (int i = 0; i < tempInventory.GetInventoryCounts()[2]; i++){
            throwItemDecisionObject.GetComponentsInChildren<Text>()[1].text = tempInventory.GetFood(i)._name;
            var obj = GameObject.Instantiate(throwItemDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }

        scrollBar.value = 1;
        if (itemsInDecisionPanel > 7){
            int difference = itemsInDecisionPanel - 7;
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, -(difference * 50));
        }
        else{//else reset
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, 0);
        }

    }

    //called when user selects the item they want to throw
    public void ThrowItem(){

        ClearDecisionPanel();

        //allow user to select who or what they want to throw the item at depending on the scene
        int numCharacters = settingManager.GetComponent<SettingManager>().numCharactersInSetting;
        for (int i = 0; i < numCharacters; i++){
            throwItemAtDecisionObject.GetComponentsInChildren<Text>()[1].text = settingManager.GetComponent<SettingManager>().charactersInSetting[i].GetFirstName()
                + " " + settingManager.GetComponent<SettingManager>().charactersInSetting[i].GetLastName();
            var obj = GameObject.Instantiate(throwItemAtDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        //adjust space on scrollbar
        scrollBar.value = 1;
        if (itemsInDecisionPanel > 7){
            int difference = itemsInDecisionPanel - 7;
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, -(difference * 30));
        }
        else{//else reset
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, 0);
        }

    }

    //called when user selects who or what to throw item at
    public void ThrowItemAt(){
        RefreshDecisionList();
        Debug.Log("Object thrown at");
    }

    
    //called when user selects the eat action
    public void Talk(){

        ClearDecisionPanel();

        //allow user to select who to talk to
        int numCharacters = settingManager.GetComponent<SettingManager>().numCharactersInSetting;
        for (int i = 0; i < numCharacters; i++){
            talkToDecisionObject.GetComponentsInChildren<Text>()[1].text = settingManager.GetComponent<SettingManager>().charactersInSetting[i].GetFirstName()
                + " " + settingManager.GetComponent<SettingManager>().charactersInSetting[i].GetLastName();
            var obj = GameObject.Instantiate(talkToDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }

        scrollBar.value = 1;
        if (itemsInDecisionPanel > 7){
            int difference = itemsInDecisionPanel - 7;
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, -(difference * 30));
        }
        else{//else reset
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, 0);
        }

    }

    //called when user selects the item to eat action
    public void TalkTo(string firstName, string lastName){
        RefreshDecisionList();
        Debug.Log("You talked to : " + firstName + " " + lastName);
    }

    //called when user completes the selections for an action
    public void RefreshDecisionList(){
        ClearDecisionPanel();
        FillDecisionPanel();
    }

    private void ClearDecisionPanel(){
        for (int i = 0; i < itemsInDecisionPanel; i++){
            Destroy(decisionScroller.transform.GetChild(i).gameObject);
        }
        itemsInDecisionPanel = 0;
    }

    //called when user selects to wait in a bed
    public void WaitInBed(){
        mainText.text = "Feeling you didn't get enough sleep, you get back in bed.";
        sleepMenuBackgroundPanel.SetActive(true);
    }

    public void SleepForMinutes(){
        fadOutPanel.SetActive(true);
    }

    //called when user selects the next room option
    public void NextRoom(){



    }
}
