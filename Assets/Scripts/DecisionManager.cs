using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DecisionManager : MonoBehaviour {

    public GameObject inventoryManager;
    public GameObject settingManager;
    public GameObject roomManager;
    public GameObject characterManager;
    public GameObject userFeedBackObject;
    public Button cancelDecisionButton;
    //public GameObject playerManager;

    public GameObject decisionPanel;
    public GameObject decisionScroller;
    public RectTransform decisionScroll;
    public Scrollbar scrollBar;

    public GameObject throwItemDecisionObject;
    public GameObject throwItemAtDecisionObject;
    public GameObject talkDecisionObject;
    public GameObject talkToDecisionObject;
    public GameObject observeDecisionObject;
    public GameObject nextRoomDecisionObject;
    public GameObject mapPanel;

    public GameObject waitInBedDecisionObject;
    public GameObject waitInSeatingDecisionObject;
    public GameObject dialogueDecisionObject;
    public GameObject attackPersonDecisionObject;
    public GameObject eatMealDecisionObject;
    public GameObject trainingCategoryObject;
    public GameObject swordTrainingDecisionObject;
    public GameObject combatDecisionObject;
    public GameObject openCategoryDecisionObject;

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
    public Scrollbar converstationScrollBar;
    public GameObject conversationScroll;
    public GameObject startMissionDialogue;

    public GameObject eatingBackgroundPanel;
    public Text caloriesConsumedStatus;
    public int eatingMeal = 0;

    public GameObject trainingBackGroundPanel;
    public Text trainingStatusText;
    public int training = 0;
    public int trainerID;

    public GameObject combatBackgroundPanel;

    public GameObject mainTextAreaPanel;
    public GameObject mainTextObject;
    int itemsInDecisionPanel;
    public float newHeight = 0;

    public int warned = 0;

    public GameObject opponentPanel;
    public GameObject opponentObject;
    public Button leaveButton;

    struct PlayerResponse {
        public int ID;
        public string response;
        public int characterResponseID;
    }

    // Use this for initialization
    void Start() {
        itemsInDecisionPanel = 0;
        FillDecisionPanel();

    }

    

    //fill decision panel based on inventory and scene
    private void FillDecisionPanel() {

        SettingManager sM = settingManager.GetComponent<SettingManager>();
        CharactersManager cM = characterManager.GetComponent<CharactersManager>();

        //add exits based on the room
        AddCategory(1);

        //add the observe action
        AddDecision(2);

        
        if (cM.GetNumCharactersInRoom(sM.GetRoom()) > 0) {
            AddDecision(1);//talk decision
            AddCategory(2);//combat category
        }

        //if there are item up for grabs add the take action


        //if there are items available to steal add the steal action

    }

    public void CombatDecisions() {



    }

    void AddCategory(int categoryID) {
        SettingManager sM = settingManager.GetComponent<SettingManager>();
        CharactersManager cM = characterManager.GetComponent<CharactersManager>();
        
        var obj = Instantiate(openCategoryDecisionObject);
        obj.GetComponent<CategoryManager>().categoryToOpenID = categoryID;
        if(categoryID == 1) {//exit
            obj.transform.GetChild(2).GetComponent<Text>().text = "Leave";
            obj.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0.7f, 0, 1);
        }
        else if (categoryID == 2) {//combat
            obj.transform.GetChild(2).GetComponent<Text>().text = "Combat options";
            obj.transform.GetChild(0).GetComponent<Image>().color = new Color(0.7f, 0, 0, 1);
        }

        obj.transform.SetParent(decisionScroller.transform, false);
        
    }

    public void DisplayCombatOptions() {
        ClearDecisionPanel();
        SettingManager sM = settingManager.GetComponent<SettingManager>();
        CharactersManager cM = characterManager.GetComponent<CharactersManager>();

        AddDecision(3);//attack
        if (inventoryManager.GetComponent<Inventory>().GetInventoryCounts()[3] > 0) {
            AddDecision(0);//throw
        }
        
        if(sM.combat == 1) {
            AddDecision(9);
            AddDecision(10);
        }

        //add cancel button
        var canceButton = Instantiate(cancelDecisionButton);
        canceButton.transform.SetParent(decisionScroller.transform, false);
    }

    //add action to panel
    void AddDecision(int index) {

        bool add;
        // 0 = throw something
        if (index == 0) {
            var obj = Instantiate(combatDecisionObject);
            obj.GetComponent<CombatAction>().displayText = "Throw";
            obj.transform.GetChild(2).GetComponent<Text>().text = "Throw";
            obj.GetComponent<CombatAction>().combatActionID = 2;
            obj.transform.SetParent(decisionScroller.transform, false);
        }
        //1 = talk
        else if (index == 1) {
            var obj = Instantiate(talkDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
        }
        //2 = observe
        else if (index == 2) {
            var obj = Instantiate(observeDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
        }
        //3 = attack
        else if (index == 3){
            var obj = Instantiate(combatDecisionObject);
            obj.GetComponent<CombatAction>().displayText = "Attack";
            obj.transform.GetChild(2).GetComponent<Text>().text = "Attack";
            obj.GetComponent<CombatAction>().combatActionID = 1;
            obj.transform.SetParent(decisionScroller.transform, false);
        }
		else if(index == 4){
			
		}
        //5 = wait in bed
        else if (index == 5) {
            add = true;
            for (int i = 0; i < decisionScroller.transform.childCount; i++){
                if (decisionScroller.transform.GetChild(i).gameObject.name == "WaitInBedDecisionObject(Clone)"){
                    add = false;
                }
            }
            if (add == true){
                var obj = GameObject.Instantiate(waitInBedDecisionObject);
                obj.transform.SetParent(decisionScroller.transform, false);
            }
        }
        //6 = wait in seat
        else if (index == 6) {
            add = true;
            for(int i = 0; i < decisionScroller.transform.childCount; i++){
                if (decisionScroller.transform.GetChild(i).gameObject.name == "WaitInSeatingDecisionObject(Clone)"){
                    add = false;
                }
            }
            if (add == true){
                var obj = Instantiate(waitInSeatingDecisionObject);
                obj.transform.SetParent(decisionScroller.transform, false);
            }
        }
        //7 = eat a meal
        else if (index == 7){
            add = true;
            for (int i = 0; i < decisionScroller.transform.childCount; i++){
                if (decisionScroller.transform.GetChild(i).gameObject.name == "EatDecisionObject(Clone)"){
                    add = false;
                }
            }
            if (add == true){
                var obj = Instantiate(eatMealDecisionObject);
                obj.transform.SetParent(decisionScroller.transform, false);
            }
        }
        //8 = train sword fighting
        else if (index == 8){
            var obj = Instantiate(swordTrainingDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
        }
        //9 = run
        else if(index == 9) {
            var obj = Instantiate(combatDecisionObject);
            obj.GetComponent<CombatAction>().displayText = "Retreat";
            obj.transform.GetChild(2).GetComponent<Text>().text = "Retreat";
            obj.GetComponent<CombatAction>().combatActionID = 3;
            obj.transform.SetParent(decisionScroller.transform, false);
        }//10 = hide
        else if (index == 10) {
            var obj = Instantiate(combatDecisionObject);
            obj.GetComponent<CombatAction>().displayText = "Hide";
            obj.transform.GetChild(2).GetComponent<Text>().text = "Hide";
            obj.GetComponent<CombatAction>().combatActionID = 4;
            obj.transform.SetParent(decisionScroller.transform, false);
        }

        scrollBar.value = 1;
        decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, 0);

    }

    void AddExitOption() {
        Room room = roomManager.GetComponent<RoomManager>().GetRoom(settingManager.GetComponent<SettingManager>().currentRoom);
        Debug.Log(room._numExits);
        if (room._numExits > 0) {
            var obj = Instantiate(openCategoryDecisionObject);
            obj.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);
            obj.GetComponent<CategoryManager>().categoryToOpenID = 1;
            obj.transform.SetParent(decisionScroller.transform, false);
        }
    }

    public void DisplayExitOptions() {
        ClearDecisionPanel();
        Room room = roomManager.GetComponent<RoomManager>().GetRoom(settingManager.GetComponent<SettingManager>().currentRoom);

        mapPanel.SetActive(true);
        mapPanel.transform.GetChild(0).gameObject.GetComponent<RawImage>().texture = room.GetRoomImage();

        for (int i = 0; i < room._numExits; i++) {
            //exitButton = Instantiate(nex)
            nextRoomDecisionObject.GetComponentsInChildren<Text>()[0].text = room._exitTexts[i];
            nextRoomDecisionObject.GetComponent<NextRoomAction>().nextRoomID = room._connectedRooms[i];
            nextRoomDecisionObject.GetComponent<NextRoomAction>().leaveText = room._leaveTexts[i];
            var button = Instantiate(nextRoomDecisionObject, new Vector3(room.GetCoord(i)[0], room.GetCoord(i)[1], 0), Quaternion.identity);
            button.transform.SetParent(mapPanel.transform, false);
            itemsInDecisionPanel++;
        }

        //add cancel button
        var canceButton = Instantiate(cancelDecisionButton);
        canceButton.transform.SetParent(decisionScroller.transform, false);
    }

    public void CancelLeaving() {
        mapPanel.SetActive(false);

        for (int i = 2; i < mapPanel.transform.childCount; i++) {
            Destroy(mapPanel.transform.GetChild(i).gameObject);
        }
    }

    void AddRoomOptions() {
        int currentRoom = settingManager.GetComponent<SettingManager>().GetRoom();
        Room room = roomManager.GetComponent<RoomManager>().GetRoom(currentRoom);
        for (int i = 0; i < room._numOptions; i++){
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
        AddRoomOptions();
        int currentRoom = settingManager.GetComponent<SettingManager>().GetRoom();
        UpdateMainText(roomManager.GetComponent<RoomManager>().GetRoomObservation(currentRoom));
        settingManager.GetComponent<SettingManager>().roomObserved = 1;
    }

    //called when the user selects the attack option
    public void Attack(){
        ClearDecisionPanel();

        //add a label for the decision panel
        userFeedBackObject.GetComponentInChildren<Text>().text = "People in the area:";
        var UF = Instantiate(userFeedBackObject);
        UF.transform.SetParent(decisionScroller.transform, false);

        SettingManager sM = settingManager.GetComponent<SettingManager>();

        //allow user to select who to talk to
        Character[] characters = characterManager.GetComponent<CharactersManager>().GetCharactersInRoom(sM.GetRoom());
        for (int i = 0; i < characters.Length; i++){
            if (characters[i].GetStatus() != "Dead") {
                attackPersonDecisionObject.GetComponentsInChildren<Text>()[1].text = characters[i].GetFirstName() + " " + characters[i].GetLastName();
                attackPersonDecisionObject.GetComponent<AttackAction>().characterID = characters[i].GetID();
                var obj = GameObject.Instantiate(attackPersonDecisionObject);
                obj.transform.SetParent(decisionScroller.transform, false);
                itemsInDecisionPanel++;
            }
        }

        var canceButton = Instantiate(cancelDecisionButton);
        canceButton.transform.SetParent(decisionScroller.transform, false);

        scrollBar.value = 1;
        if (itemsInDecisionPanel > 7){
            int difference = itemsInDecisionPanel - 7;
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, -(difference * 30));
        }
        else{//else reset
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, 0);
        }
    }
    //called when the user selects the person to attack
    public void AttackPeople(int[] IDs) {

        for(int i = 0; i < opponentPanel.transform.childCount; i++) {
            Destroy(opponentPanel.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < IDs.Length; i++) {
            combatBackgroundPanel.SetActive(true);
            Character tempChar = characterManager.GetComponent<CharactersManager>().GetCharacter(IDs[i]);
            opponentObject.GetComponent<AttackAction>().characterID = IDs[i];
            opponentObject.transform.GetChild(0).GetComponent<Text>().text = tempChar.GetFirstName() + " " + tempChar.GetLastName();
            opponentObject.transform.GetChild(2).GetComponent<Text>().text = "34% chance for successful attack";
            opponentObject.transform.GetChild(4).GetComponent<Slider>().value = tempChar.GetHealth();
            var obj = Instantiate(opponentObject);
            obj.transform.SetParent(opponentPanel.transform, false);
        }

        RefreshDecisionList();
    }
    public bool AttackAction(int characterID) {
        int randNum = UnityEngine.Random.Range(0, 100);
        Character tempChar = characterManager.GetComponent<CharactersManager>().GetCharacter(characterID);

        if(randNum < 34) {
            tempChar.SubtractHealth(43);
            if(tempChar.GetHealth() == 0) {
                leaveButton.interactable = true;
                for(int i = 0; i < opponentPanel.transform.childCount; i++) {
                    if(characterManager.GetComponent<CharactersManager>().GetCharacter(opponentPanel.transform.GetChild(i).GetComponent<AttackAction>().characterID).GetHealth() != 0) {
                        leaveButton.interactable = false;
                    }
                }
            }
            return true;
        }
        else {
            GameObject.Find("PlayerManager").GetComponent<PlayerManager>().SubtractHealth(5 * opponentPanel.transform.childCount);
        }
        return false;
    }

    //called when user selects the throw action
    public void Throw() {

        ClearDecisionPanel();

        //add a label for the decision panel
        userFeedBackObject.GetComponentInChildren<Text>().text = "Items to throw:";
        var UF = Instantiate(userFeedBackObject);
        UF.transform.SetParent(decisionScroller.transform, false);

        //display available items to throw
        Inventory tempInventory = inventoryManager.GetComponent<Inventory>();
        for (int i = 0; i < tempInventory.GetInventoryCounts()[0]; i++) {
            throwItemDecisionObject.GetComponentsInChildren<Text>()[1].text = tempInventory.GetWeapon(i)._name;
            throwItemDecisionObject.GetComponent<ThrowAction>().itemID = tempInventory.GetWeapon(i).GetID();
            var obj = GameObject.Instantiate(throwItemDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        for (int i = 0; i < tempInventory.GetInventoryCounts()[1]; i++) {
            throwItemDecisionObject.GetComponentsInChildren<Text>()[1].text = tempInventory.GetArmor(i)._name;
            throwItemDecisionObject.GetComponent<ThrowAction>().itemID = tempInventory.GetArmor(i).GetID();
            var obj = GameObject.Instantiate(throwItemDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        for (int i = 0; i < tempInventory.GetInventoryCounts()[2]; i++) {
            throwItemDecisionObject.GetComponentsInChildren<Text>()[1].text = tempInventory.GetFood(i)._name;
            throwItemDecisionObject.GetComponent<ThrowAction>().itemID = tempInventory.GetFood(i).GetID();
            var obj = GameObject.Instantiate(throwItemDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }

        var canceButton = Instantiate(cancelDecisionButton);
        canceButton.transform.SetParent(decisionScroller.transform, false);

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
    public void ThrowItem(int itemID) {

        ClearDecisionPanel();

        //add a label for the decision panel
        userFeedBackObject.GetComponentInChildren<Text>().text = "People in the area:";
        var UF = Instantiate(userFeedBackObject);
        UF.transform.SetParent(decisionScroller.transform, false);

        SettingManager sM = settingManager.GetComponent<SettingManager>();

        //allow user to select who or what they want to throw the item at depending on the scene
        Character[] characters = characterManager.GetComponent<CharactersManager>().GetCharactersInRoom(sM.GetRoom());
        for (int i = 0; i < characters.Length; i++) {
            throwItemAtDecisionObject.GetComponentsInChildren<Text>()[1].text = characters[i].GetFirstName() + " " + characters[i].GetLastName();
            throwItemAtDecisionObject.GetComponent<ThrowAction>().characterID = characters[i].GetID();
            throwItemAtDecisionObject.GetComponent<ThrowAction>().itemID = itemID;
            var obj = GameObject.Instantiate(throwItemAtDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }

        var canceButton = Instantiate(cancelDecisionButton);
        canceButton.transform.SetParent(decisionScroller.transform, false);

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
    public void ThrowItemAt(int characterID, int itemID) {
        string greeting = "What the hell was that for?";
        Character tempChar = characterManager.GetComponent<CharactersManager>().GetCharacter(characterID);
        tempChar.SetRelationship(tempChar.GetRelationship() - 10);
        inventoryManager.GetComponent<Inventory>().RemoveItemByID(itemID);
        TalkTo(characterID, greeting);
        RefreshDecisionList();
    }

    public void Retreat() {
        int randNum = Random.Range(0, 10);
        if(randNum == 0) {
            //retreat failed
        }
        else {
            settingManager.GetComponent<SettingManager>().combat = 0;
            settingManager.GetComponent<SettingManager>().AddTime(5, 30);
        }

        RefreshDecisionList();
    }

    //called when user selects to eat a meal
    public void OpenEatMenuPanel() {
        eatingBackgroundPanel.SetActive(true);
    }
    //called when user clicks start eating button
    public void StartEating() {
        eatingMeal = 1;
        StartCoroutine(EatMeal());
    }
    IEnumerator EatMeal(){

        GameObject playerManager = GameObject.FindWithTag("PlayerManager");
        int caloriesGained = 0;
        settingManager.GetComponent<SettingManager>().SetTimeScale(0.0083f);

        while (eatingMeal == 1){
            if(playerManager.GetComponent<PlayerManager>().calories < 1000) {
                playerManager.GetComponent<PlayerManager>().calories++;
                caloriesConsumedStatus.text = (++caloriesGained).ToString() + " calories";
                playerManager.GetComponent<PlayerManager>().UpdateCalories();
                yield return new WaitForSeconds(0.01f);
            }
            else {
                eatingMeal = 0;
                settingManager.GetComponent<SettingManager>().SetTimeScale(1f);
            }
        }
    }
    public void StopEatingMeal(){
        eatingMeal = 0;
        settingManager.GetComponent<SettingManager>().SetTimeScale(1f);
    }

    //called when user selects the talk action
    public void Talk(){

        ClearDecisionPanel();

        //add a label for the decision panel
        userFeedBackObject.GetComponentInChildren<Text>().text = "People in the area:";
        var UF = Instantiate(userFeedBackObject);
        UF.transform.SetParent(decisionScroller.transform, false);

        SettingManager sM = settingManager.GetComponent<SettingManager>();

        //allow user to select who to talk to
        Character[] characters = characterManager.GetComponent<CharactersManager>().GetCharactersInRoom(sM.GetRoom());
        for (int i = 0; i < characters.Length; i++){
            if (characters[i].GetStatus() != "Dead") {
                talkToDecisionObject.GetComponentsInChildren<Text>()[1].text = characters[i].GetFirstName() + " " + characters[i].GetLastName();
                talkToDecisionObject.GetComponent<TalkToOption>().characterID = characters[i].GetID();
                if (characters[i].GetImportance() == 1) {
                    talkToDecisionObject.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 1);
                }
                else {
                    talkToDecisionObject.transform.GetChild(0).GetComponent<Image>().color = new Color(.3f, .3f, .3f, 1);
                }
                var obj = GameObject.Instantiate(talkToDecisionObject);
                obj.transform.SetParent(decisionScroller.transform, false);
                itemsInDecisionPanel++;
            }
        }

        //add cancel button
        var canceButton = Instantiate(cancelDecisionButton);
        canceButton.transform.SetParent(decisionScroller.transform, false);

        scrollBar.value = 1;
        if (itemsInDecisionPanel > 7){
            int difference = itemsInDecisionPanel - 7;
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, -(difference * 35));
        }
        else{//else reset
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, 0);
        }

    }
    //called when user selects the character to talk to, only called once the conversation is initiated
    public void TalkTo(int characterID){

        //set the conversation panel to active and get the character being talked to
        conversationBackgroundPanel.SetActive(true);
        Character talkingTo = characterManager.GetComponent<CharactersManager>().GetCharacter(characterID);

        //get and display the greeting from the character which updates player knowledge
        ConversationManager convoManager = GameObject.Find("ConversationManager").GetComponent<ConversationManager>();
        StartCoroutine(convoManager.DisplayGreeting(characterID));

        //adds all available convos
        CheckCharacterForConvos(characterID);

        //update character info panel with all characters info
        characterInfoPanel.GetComponentInChildren<Text>().text = talkingTo.GetFirstName() + " " + talkingTo.GetLastName();
        int[] playerKnowledge = talkingTo.GetPlayerKnowledge();
        for (int i = 1; i < 6; i++) {
            if (playerKnowledge[i - 1] == 1) {
                characterInfoPanel.GetComponentsInChildren<Text>()[i].text = characterManager.GetComponent<CharactersManager>().GetTrait(talkingTo.GetTraits()[i - 1]);
            }
            else {
                characterInfoPanel.GetComponentsInChildren<Text>()[i].text = "???";
            }
        }
        for (int i = 6; i < 11; i++){
            if (playerKnowledge[i - 1] == 1) {
                characterInfoPanel.GetComponentsInChildren<Text>()[i].text = characterManager.GetComponent<CharactersManager>().GetSkill(talkingTo.GetSkills()[i - 6]);
            }
            else {
                characterInfoPanel.GetComponentsInChildren<Text>()[i].text = "???";
            }
        }
        for (int i = 11; i < 16; i++){
            if (playerKnowledge[i - 1] == 1) {
                characterInfoPanel.GetComponentsInChildren<Text>()[i].text = characterManager.GetComponent<CharactersManager>().GetGoal(talkingTo.GetSkills()[i - 11]);
            }
            else {
                characterInfoPanel.GetComponentsInChildren<Text>()[i].text = "???";
            }
        }
        relationshipSlider.value = talkingTo.GetRelationship();

        
        CheckCharacterForTraining(characterID);
        CheckCharacterForMissions(characterID);

        RefreshDecisionList();
    }
    //for dynamic greetings and blank slate
    public void TalkTo(int characterID, string greeting) {

        //stop other actions
        if (eatingMeal == 1) {
            GameObject eatingPanel = GameObject.Find("EatingPanel");
            eatingPanel.transform.GetChild(1).gameObject.SetActive(true);
            eatingPanel.transform.GetChild(2).GetComponent<Button>().interactable = true;
            eatingPanel.transform.GetChild(3).gameObject.SetActive(false);
            eatingBackgroundPanel.SetActive(false);
            StopEatingMeal();
        }
        else {
            eatingBackgroundPanel.SetActive(false);
        }

        //set the conversation panel to active and get the character being talked to
        conversationBackgroundPanel.SetActive(true);
        Character talkingTo = characterManager.GetComponent<CharactersManager>().GetCharacter(characterID);

        if(greeting != "") {
            //display the greeting given
            StartCoroutine(GameObject.FindWithTag("ConversationManager").GetComponent<ConversationManager>().DisplayGreeting(characterID, greeting));
        }

        //update character info panel with all characters info
        characterInfoPanel.GetComponentInChildren<Text>().text = talkingTo.GetFirstName() + " " + talkingTo.GetLastName();
        int[] playerKnowledge = talkingTo.GetPlayerKnowledge();
        for (int i = 1; i < 6; i++) {
            if (playerKnowledge[i - 1] == 1) {
                characterInfoPanel.GetComponentsInChildren<Text>()[i].text = characterManager.GetComponent<CharactersManager>().GetTrait(talkingTo.GetTraits()[i - 1]);
            }
            else {
                characterInfoPanel.GetComponentsInChildren<Text>()[i].text = "???";
            }
        }
        for (int i = 6; i < 11; i++) {
            if (playerKnowledge[i - 1] == 1) {
                characterInfoPanel.GetComponentsInChildren<Text>()[i].text = characterManager.GetComponent<CharactersManager>().GetSkill(talkingTo.GetSkills()[i - 6]);
            }
            else {
                characterInfoPanel.GetComponentsInChildren<Text>()[i].text = "???";
            }
        }
        for (int i = 11; i < 16; i++) {
            if (playerKnowledge[i - 1] == 1) {
                characterInfoPanel.GetComponentsInChildren<Text>()[i].text = characterManager.GetComponent<CharactersManager>().GetGoal(talkingTo.GetSkills()[i - 11]);
            }
            else {
                characterInfoPanel.GetComponentsInChildren<Text>()[i].text = "???";
            }
        }
        relationshipSlider.value = talkingTo.GetRelationship();

        RefreshDecisionList();
    }

    public void ClearConversation() {
        //clear previous dialogue
        for (int i = 0; i < conversationScroll.transform.childCount; i++) {
            Destroy(conversationScroll.transform.GetChild(i).gameObject);
        }

        //reset scrollbar and scroll area
        converstationScrollBar.value = 0;
        conversationScroll.GetComponent<RectTransform>().offsetMax = new Vector2(-15, 0);

        //clear previous dialogue options
        for (int i = 0; i < dialogueOptionsPanel.transform.childCount; i++) {
            Destroy(dialogueOptionsPanel.transform.GetChild(i).gameObject);
        }
    }

    public void CheckCharacterForConvos(int characterID) {
        int convoID;
        for (int i = 0; i < 3; i++) {
            convoID = characterManager.GetComponent<CharactersManager>().GetCharacter(characterID).GetConvoList()[i];
            if(convoID != -1) {
                GameObject.Find("ConversationManager").GetComponent<ConversationManager>().AddConversation(convoID, characterID);
            }
        }
   
    }

    public void CheckCharacterForTraining(int characterID) {
        CharactersManager cM = GameObject.Find("CharacterManager").GetComponent<CharactersManager>();

        for(int i = 0; i < 3; i++) {
            if(cM.GetCharacter(characterID).GetTrainings()[i] != 0) {
                int[][] trainingTimes = cM.GetCharacter(characterID).GetTrainingHours();
                int[] time = settingManager.GetComponent<SettingManager>().GetTime();
                if(trainingTimes[i][0] <= (time[0]+1) && time[0] < trainingTimes[i][1]) {
                    dialogueDecisionObject.transform.GetChild(1).GetComponent<Text>().text = "Sword Training";
                    dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "I'm ready to begin training";
                    dialogueDecisionObject.GetComponent<DialogueAction>().convoID = -1;
                    dialogueDecisionObject.GetComponent<DialogueAction>().effect = 10;
                    dialogueDecisionObject.GetComponent<DialogueAction>().characterID = characterID;
                    var obj = Instantiate(dialogueDecisionObject);
                    obj.transform.SetParent(dialogueOptionsPanel.transform, false);
                }
            }
        }
    }
    //called by TalkTo(characterID)
    public void CheckCharacterForMissions(int characterID) {
        CharactersManager cM = GameObject.Find("CharacterManager").GetComponent<CharactersManager>();
        for(int i = 0; i < 3; i++) {
            if(cM.GetCharacter(characterID).GetMissions()[i] != 0) {
                int[][] missionTimes = cM.GetCharacter(characterID).GetMissionTimes();
                int[] time = settingManager.GetComponent<SettingManager>().GetTime();
                if (missionTimes[i][0] <= time[0] && time[0] < missionTimes[i][1]) {
                    startMissionDialogue.GetComponent<DialogueAction>().dialogueMessage = "I'm ready";
                    startMissionDialogue.GetComponent<DialogueAction>().effect = 0;
                    startMissionDialogue.GetComponent<DialogueAction>().convoID = -1;
                    startMissionDialogue.GetComponent<DialogueAction>().characterID = characterID;
                    startMissionDialogue.GetComponent<Missions>().missionID = cM.GetCharacter(characterID).GetMissions()[i];
                    var obj = Instantiate(startMissionDialogue);
                    obj.transform.SetParent(dialogueOptionsPanel.transform, false);
                }
            }
        }
    }

    //called when user selects to wait in a bed
    public void WaitInBed(){

        string text = "Feeling you didn't get enough sleep, you get back in bed.";
        UpdateMainText(text);
        sleepWaitBackgroundPanel.SetActive(true);
    }

    //called from a dialogue option
    public IEnumerator DisplayTrainingWindow() {
        yield return new WaitForSeconds(3f);

        //end the conversation
        if(conversationBackgroundPanel.activeSelf == true) {
            conversationBackgroundPanel.SetActive(false);
            ClearConversation();
        }

        //enable the training window
        trainingBackGroundPanel.SetActive(true);
    }
    public void StartTraining() {
        StartCoroutine(Train());
    }
    public IEnumerator Train() {
        int skillGained = 0;
        training = 1;
        SettingManager sM = settingManager.GetComponent<SettingManager>();
        sM.SetTimeScale(.1f);
        sM.training = 1;
        PlayerManager pM = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();

        while (training == 1) {

            pM.swordSkill++;
            pM.UpdateSwordSkill();
            if(pM.calories > 0){
                pM.calories--;
            }
            pM.UpdateCalories();

            sM.AddTime(1, 0);

            trainingStatusText.text = (++skillGained).ToString() + " skill increased.";

            if(sM.GetTime()[0] > 10) {
                StopTraining();
                GameObject startButton = trainingBackGroundPanel.transform.GetChild(1).GetChild(3).gameObject;
                GameObject stopButton = trainingBackGroundPanel.transform.GetChild(1).GetChild(2).gameObject;
                Button cancelButton = trainingBackGroundPanel.transform.GetChild(1).GetChild(4).GetComponent<Button>();
                startButton.SetActive(true);
                stopButton.SetActive(false);
                cancelButton.interactable = true;
                trainingBackGroundPanel.SetActive(false);
                string greeting = "Alright, training is over for the day. Eat until noon then get in your hunting groups.";
                TalkTo(1000, greeting);
            }

            yield return new WaitForSeconds(0.08f);
        }
    }
    public void StopTraining() {
        training = 0;
        settingManager.GetComponent<SettingManager>().SetTimeScale(1f);
        settingManager.GetComponent<SettingManager>().training = 0;
    }
    //called when users selects cancel button
    public void NotifyTrainer() {
        characterManager.GetComponent<CharactersManager>().ProvokeCharacter(trainerID, 1);
    }

    public IEnumerator StartPrison() {
        yield return new WaitForSeconds(5f);
        string text = "The guards close the door behind you as you walk into your cell. They turn and leave without a word.";
        UpdateMainText(text);

        float randWait = Random.Range(10, 30);
        yield return new WaitForSeconds(randWait);

        TalkTo(1007, "");
        GameObject.Find("ConversationManager").GetComponent<ConversationManager>().StartConversation(1, 1007);

        while (true) {
            if(settingManager.GetComponent<SettingManager>().GetTime()[0] == 8) {
                text = "You hear footsteps at the top of the wide stairway leading down to the prison. Guards appear around the corner at the bottom of the stairs and turn right toward you cell. Without a word they slide a bowl on the floor into your cell. Then they leave.";
                UpdateMainText(text);
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    //called when user selects to wait by sitting
    public void WaitInSeating(){
        UpdateMainText("Feeling lazy you relax on one of the benches.");
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

    //called when user selects an exit option by NextRoomAction.cs
    public void NextRoom(int nextRoomID, string leaveText){

        mapPanel.SetActive(false);

        for(int i = 2; i < mapPanel.transform.childCount; i++) {
            Destroy(mapPanel.transform.GetChild(i).gameObject);
        }

        CharactersManager cM = characterManager.GetComponent<CharactersManager>();
        SettingManager sM = settingManager.GetComponent<SettingManager>();
        Character[] characters = cM.GetCharactersInRoom(sM.GetRoom());
        for(int i = 0; i < characters.Length; i++) {
            if(cM.ProvokeCharacter(characters[i].GetID(), 2)) {
                return;
            }
        }

     

        //update main text with the leaving dialogue from the current room to the selected room
        UpdateMainText(leaveText);
        settingManager.GetComponent<SettingManager>().AddTime(0, 30);
        //update the current room to the setting manager
        settingManager.GetComponent<SettingManager>().currentRoom = nextRoomID;
        settingManager.GetComponent<SettingManager>().DisplayRoom();

        //update the decision list
        for (int i = 0; i < decisionScroller.transform.childCount; i++) {
            Destroy(decisionScroller.transform.GetChild(i).gameObject);
        }
        RefreshDecisionList();
    }

    public void UpdateMainText(string text) {

        Destroy(mainTextAreaPanel.transform.GetChild(0).gameObject);

        var mainText = Instantiate(mainTextObject);
        mainText.transform.GetChild(0).GetComponent<Text>().text = text;
        mainText.transform.SetParent(mainTextAreaPanel.transform, false);
    }

    //called when user completes the selections for an action
    public void RefreshDecisionList() {
        ClearDecisionPanel();
        FillDecisionPanel();
    }
    //called by RefreshDecisionList
    private void ClearDecisionPanel() {
        for (int i = 0; i < decisionScroller.transform.childCount; i++) {
            if (decisionScroller.transform.GetChild(i).gameObject.name != "ProgressMission(Clone)") {
                Destroy(decisionScroller.transform.GetChild(i).gameObject);
            }
        }
        itemsInDecisionPanel = 0;
    }
    //called when player leaves training after being warned not to
    public IEnumerator SendGuardsForPlayer() {
        yield return new WaitForSeconds(Random.Range(15, 30));
        Character guard1 = characterManager.GetComponent<CharactersManager>().GetCharacter(1005);
        Character guard2 = characterManager.GetComponent<CharactersManager>().GetCharacter(1006);
        int location = GameObject.Find("SettingManager").GetComponent<SettingManager>().currentRoom;

        guard1.SetLocation(location);
        guard2.SetLocation(location);

        GameObject.Find("ConversationManager").GetComponent<ConversationManager>().StartConversation(3, 1005);
       
    }
}
