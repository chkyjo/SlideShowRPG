using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DecisionManager : MonoBehaviour {

    public GameObject inventoryManager;
    public GameObject settingManager;
    public GameObject roomManager;
    public GameObject characterManager;
    public GameObject playerManager;

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
    public GameObject waitInSeatingDecisionObject;
    public GameObject dialogueDecisionObject;
    public GameObject eatMealDecisionObject;

    public Slider sleepSlider;
    public Text sleepSliderValueStatus;
    public GameObject fadOutPanel;
    public GameObject sleepWaitBackgroundPanel;
    public GameObject sitWaitBackgroundPanel;
    

    public Slider sitSlider;
    public Text sitSliderValueStatus;

    public GameObject conversationBackgroundPanel;
    public GameObject characterInfoPanel;
    public Slider relationshipSlider;
    public GameObject dialogueOptionsPanel;

    public GameObject eatingBackgroundPanel;
    public Text caloriesConsumedStatus;
    public int eatingMeal = 0;

    public Text mainText;
    int itemsInDecisionPanel;

    // Use this for initialization
    void Start() {

        itemsInDecisionPanel = 0;
        FillDecisionPanel();

    }

    // Update is called once per frame
    void Update() {

    }

    //fill decision panel based on inventory and scene
    private void FillDecisionPanel() {
        //add exits based on the room
        AddRoomExits();

        //add the observe action
        AddDecision(2);

        //if there are people in the area add the talk action
        if (settingManager.GetComponent<SettingManager>().numCharactersInSetting > 0) {
            AddDecision(1);
        }

        //if there is something in the players inventory and there are people to throw things at add the throw action
        if (inventoryManager.GetComponent<Inventory>().GetInventoryCounts()[3] > 0 && settingManager.GetComponent<SettingManager>().numCharactersInSetting > 0) {
            AddDecision(0);
        }

        //if there are living things in the area add the attack action
        if (settingManager.GetComponent<SettingManager>().numCharactersInSetting > 0) {
            AddDecision(3);
        }

        //if there are item up for grabs add the take action


        //if there are items available to steal add the steal action

    }

    public void CombatDecisions() {



    }

    //add action to panel
    void AddDecision(int index) {

        if (index == 0) {
            var obj = GameObject.Instantiate(throwDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        if (index == 1) {
            var obj = GameObject.Instantiate(talkDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        if (index == 2) {
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
        if (index == 5) {
            var obj = GameObject.Instantiate(waitInBedDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        if (index == 6) {
            var obj = GameObject.Instantiate(waitInSeatingDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        if (index == 7){
            var obj = GameObject.Instantiate(eatMealDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }

        scrollBar.value = 1;
        decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, 0);

    }

    void AddRoomExits() {
        Room room = roomManager.GetComponent<RoomManager>().GetRoom(settingManager.GetComponent<SettingManager>().currentRoom);
        for (int i = 0; i < room._numExits; i++) {
            AddExit(room._exitTexts[i], room._connectedRooms[i], room._leaveTexts[i]);
        }
    }

    void AddExit(string exitText, int nextRoomID, string leaveActionText) {
        nextRoomDecisionObject.GetComponentsInChildren<Text>()[1].text = exitText;
        nextRoomDecisionObject.GetComponent<NextRoomAction>().nextRoomID = nextRoomID;
        nextRoomDecisionObject.GetComponent<NextRoomAction>().leaveText = leaveActionText;
        var obj = GameObject.Instantiate(nextRoomDecisionObject);
        obj.transform.SetParent(decisionScroller.transform, false);
        itemsInDecisionPanel++;
    }

    void AddRoomOptions() {
        int currentRoom = settingManager.GetComponent<SettingManager>().GetRoom();
        Room room = roomManager.GetComponent<RoomManager>().GetRoom(currentRoom);
        for (int i = 0; i < room._numOptions; i++) {
            AddDecision(room._options[i]);
        }

        scrollBar.value = 1;
        if (itemsInDecisionPanel > 7)
        {
            int difference = itemsInDecisionPanel - 7;
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, -(difference * 30));
        }
        else {//else reset
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, 0);
        }
    }

    public void ObserveSurroundings() {
        if (settingManager.GetComponent<SettingManager>().roomObserved == 0) {
            AddRoomOptions();
            int currentRoom = settingManager.GetComponent<SettingManager>().GetRoom();
            mainText.text = roomManager.GetComponent<RoomManager>().GetRoom(currentRoom)._observationText;
            settingManager.GetComponent<SettingManager>().roomObserved = 1;
        }
    }

    //called when user selects the throw action
    public void Throw() {

        ClearDecisionPanel();

        //display available items to throw
        Inventory tempInventory = inventoryManager.GetComponent<Inventory>();
        for (int i = 0; i < tempInventory.GetInventoryCounts()[0]; i++) {
            throwItemDecisionObject.GetComponentsInChildren<Text>()[1].text = tempInventory.GetWeapon(i)._name;
            var obj = GameObject.Instantiate(throwItemDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        for (int i = 0; i < tempInventory.GetInventoryCounts()[1]; i++) {
            throwItemDecisionObject.GetComponentsInChildren<Text>()[1].text = tempInventory.GetArmor(i)._name;
            var obj = GameObject.Instantiate(throwItemDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        for (int i = 0; i < tempInventory.GetInventoryCounts()[2]; i++) {
            throwItemDecisionObject.GetComponentsInChildren<Text>()[1].text = tempInventory.GetFood(i)._name;
            var obj = GameObject.Instantiate(throwItemDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }

        scrollBar.value = 1;
        if (itemsInDecisionPanel > 7) {
            int difference = itemsInDecisionPanel - 7;
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, -(difference * 50));
        }
        else {//else reset
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, 0);
        }

    }

    //called when user selects the item they want to throw
    public void ThrowItem() {

        ClearDecisionPanel();

        //allow user to select who or what they want to throw the item at depending on the scene
        int numCharacters = settingManager.GetComponent<SettingManager>().numCharactersInSetting;
        for (int i = 0; i < numCharacters; i++) {
            throwItemAtDecisionObject.GetComponentsInChildren<Text>()[1].text = settingManager.GetComponent<SettingManager>().charactersInSetting[i].GetFirstName()
                + " " + settingManager.GetComponent<SettingManager>().charactersInSetting[i].GetLastName();
            var obj = GameObject.Instantiate(throwItemAtDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        //adjust space on scrollbar
        scrollBar.value = 1;
        if (itemsInDecisionPanel > 7) {
            int difference = itemsInDecisionPanel - 7;
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, -(difference * 30));
        }
        else {//else reset
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, 0);
        }

    }

    //called when user selects who or what to throw item at
    public void ThrowItemAt() {
        RefreshDecisionList();
        Debug.Log("Object thrown at");
    }

    //called when user selects to eat a meal
    public void Eat() {
        eatingBackgroundPanel.SetActive(true);
    }

    //called when user begins eating
    public void StartEating() {
        eatingMeal = 1;
        StartCoroutine(EatMeal());
    }

    IEnumerator EatMeal(){

        int startingCalories = playerManager.GetComponent<PlayerManager>().calories;
        //settingManager.GetComponent<SettingManager>().StopTime();

        while (eatingMeal == 1){

            playerManager.GetComponent<PlayerManager>().calories++;

            caloriesConsumedStatus.text = (playerManager.GetComponent<PlayerManager>().calories - startingCalories).ToString() + " calories";

            if ((playerManager.GetComponent<PlayerManager>().calories - startingCalories) % 50 == 0){
                settingManager.GetComponent<SettingManager>().AddTime(1);
            }

            playerManager.GetComponent<PlayerManager>().UpdateCalories();

            yield return new WaitForSeconds(0.1f);
        }

    }

    public void StopEatingMeal(){
        eatingMeal = 0;
        settingManager.GetComponent<SettingManager>().StartTime();
    }

    
    //called when user selects the talk action
    public void Talk(){

        ClearDecisionPanel();

        //allow user to select who to talk to
        int numCharacters = settingManager.GetComponent<SettingManager>().numCharactersInSetting;
        for (int i = 0; i < numCharacters; i++){
            talkToDecisionObject.GetComponentsInChildren<Text>()[1].text = settingManager.GetComponent<SettingManager>().charactersInSetting[i].GetFirstName()
                + " " + settingManager.GetComponent<SettingManager>().charactersInSetting[i].GetLastName();
            talkToDecisionObject.GetComponent<TalkToOption>().characterID = settingManager.GetComponent<SettingManager>().charactersInSetting[i].GetID();
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

    //called when user selects the character to talk to
    public void TalkTo(int ID){

        conversationBackgroundPanel.SetActive(true);
        Character talkingTo = characterManager.GetComponent<CharactersManager>().GetCharacter(ID);

        characterInfoPanel.GetComponentInChildren<Text>().text = talkingTo.GetFirstName() + " " + talkingTo.GetLastName();
        for (int i = 1; i < 6; i++) {
            characterInfoPanel.GetComponentsInChildren<Text>()[i].text = characterManager.GetComponent<CharactersManager>().GetTrait(talkingTo.GetTraits()[i-1]);
        }
        for (int i = 6; i < 11; i++){
            characterInfoPanel.GetComponentsInChildren<Text>()[i].text = characterManager.GetComponent<CharactersManager>().GetSkill(talkingTo.GetSkills()[i - 6]);
        }
        for (int i = 11; i < 16; i++){
            characterInfoPanel.GetComponentsInChildren<Text>()[i].text = characterManager.GetComponent<CharactersManager>().GetGoal(talkingTo.GetGoals()[i - 11]);
        }
        relationshipSlider.value = talkingTo.GetRelationship();

        dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Ask about current events";
        var dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
        dialogueObj.transform.SetParent(dialogueOptionsPanel.transform);

        RefreshDecisionList();
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
        sleepWaitBackgroundPanel.SetActive(true);
    }

    //called when user selects to wait by sitting
    public void WaitInSeating(){
        mainText.text = "Feeling lazy you relax on one of the benches.";
        sitWaitBackgroundPanel.SetActive(true);
    }

    public void UpdateSliderUI(){
        sleepSliderValueStatus.text = ((int)sleepSlider.value).ToString() + " minutes";
    }

    public void UpdateWaitTextStatus(){
        sitSliderValueStatus.text = ((int)sitSlider.value).ToString() + " minutes";
    }

    public void SleepForMinutes(){
        fadOutPanel.SetActive(true);
    }

    //called when user selects an exit option
    public void NextRoom(int nextRoomID, string leaveText){

        settingManager.GetComponent<SettingManager>().roomObserved = 0;
        //update main text with the leaving dialogue from the current room to the selected room
        mainText.text = leaveText;
        //update the current room to the setting manager
        settingManager.GetComponent<SettingManager>().currentRoom = nextRoomID;
        //find the characters in the room and update the setting manager
        Character[] characters = characterManager.GetComponent<CharactersManager>().GetCharactersInRoom(nextRoomID);
        settingManager.GetComponent<SettingManager>().AddCharactersToSetting(characters);
        //update the decision list
        RefreshDecisionList();

    }
}
