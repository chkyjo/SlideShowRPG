using System.Collections;
using System.Collections.Generic;
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

    public GameObject pickUpDecisionObject;

    public GameObject waitDecisionObject;

    public GameObject dialogueDecisionObject;
    public GameObject attackPersonDecisionObject;
    public GameObject eatMealDecisionObject;
    public GameObject trainingCategoryObject;
    public GameObject swordTrainingDecisionObject;
    public GameObject combatDecisionObject;
    public GameObject openCategoryDecisionObject;

    public GameObject sleepWaitBackgroundPanel;
    public Slider sleepSlider;
    public Text sleepSliderValueStatus;
    public GameObject fadOutPanel;
    
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

    public GameObject chanceDecisionObject;

    public GameObject combatBackgroundPanel;
    public Button attemptCancelButton;
    public GameObject opponentPanel;
    public GameObject characterCombatObject;
    public GameObject allyPanel;
    public Button leaveButton;

    public GameObject mainTextAreaPanel;
    public GameObject mainTextObject;
    int itemsInDecisionPanel;
    public float newHeight = 0;

    int _warned = 0;
    int _chanceSpotDeer = 40;

    

    public GameObject indoorPanel;

    struct PlayerResponse {
        public int ID;
        public string response;
        public int characterResponseID;
    }
    struct Interruption {
        public int actionID;
        public int characterID;
        public string characterText;
    }

    List<Interruption> interruptions;

    // Use this for initialization
    void Start() {
        interruptions = new List<Interruption>();
        itemsInDecisionPanel = 0;
        FillDecisionPanel();

    }

    //fill decision panel based on inventory and scene
    private void FillDecisionPanel() {

        SettingManager sM = settingManager.GetComponent<SettingManager>();
        CharacterManager cM = characterManager.GetComponent<CharacterManager>();
        Room currentRoom = roomManager.GetComponent<RoomManager>().GetRoom(sM.GetRoom());

        //add the observe action
        AddDecision(2);

        
        if (cM.GetNumCharactersInRoom(sM.GetRoom()) > 0) {
            AddDecision(1);//talk decision
            AddCategory(2);//combat category
        }

        //if the room has been observed add room options
        if (currentRoom.GetObserved() == 1) {
            AddRoomOptions();
        }
        

        //if there are item up for grabs add the take action


        //if there are items available to steal add the steal action

    }

    public void CombatDecisions() {



    }

    void AddCategory(int categoryID) {
        SettingManager sM = settingManager.GetComponent<SettingManager>();
        CharacterManager cM = characterManager.GetComponent<CharacterManager>();
        
        var obj = Instantiate(openCategoryDecisionObject);
        obj.GetComponent<CategoryManager>().categoryToOpenID = categoryID;
        if(categoryID == 1) {//exit
            obj.transform.GetChild(0).GetComponentInChildren<Text>().text = "Leave";
            obj.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0.7f, 0, 1);
        }
        else if (categoryID == 2) {//combat
            obj.transform.GetChild(0).GetComponentInChildren<Text>().text = "Combat options";
            obj.transform.GetChild(0).GetComponentInChildren<Text>().color = new Color(1f, 1, 1, 1);
            obj.transform.GetChild(0).GetComponent<Image>().color = new Color(0.7f, 0, 0, 1);
        }

        obj.transform.SetParent(decisionScroller.transform, false);
        
    }

    public void DisplayCombatOptions() {
        ClearDecisionPanel();
        SettingManager sM = settingManager.GetComponent<SettingManager>();
        CharacterManager cM = characterManager.GetComponent<CharacterManager>();

        AddDecision(3);//attack
        if (inventoryManager.GetComponent<InventoryManager>().GetInventoryCounts()[3] > 0) {
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

    

    public void PerformChanceAction(int actionID) {
        if(actionID == 11) {
            DeerTrackingResult();
        }
    }

    public void DeerTrackingResult() {

        RoomManager roomM = roomManager.GetComponent<RoomManager>();
        int currentRoomID = settingManager.GetComponent<SettingManager>().GetRoom();
        int randNum;
        string resultText;

        GameObject.Find("GameManager").GetComponent<GameManager>().SetLastPlayerAction(11);

        //remove the option to follow tracks from the current room
        roomM.RemoveTempOptionFromRoom(currentRoomID, 11);
        
        //chance to fail
        randNum = Random.Range(0, 10);
        if (randNum >  GameObject.Find("PlayerManager").GetComponent<PlayerManager>().GetPerception() / 10) {
            resultText = "You lost the trail.";
            UpdateMainText(resultText);
            return;
        }
        
        //if successfully follows trail chance to find the deer immediately
        randNum = Random.Range(0, 10);
        if(randNum < _chanceSpotDeer) {
            SpotDeer();
            return;
        }

        //if not found immediately get a direction for the tracks
        Room tempRoom;
        int randDir;
        string tempDir;
        int i = currentRoomID % 100;
        int j = currentRoomID / 100;
        int tempRoomID;
        do {
            randDir = Random.Range(0, 4);

            if (randDir == 0 && i != 0) {
                tempRoomID = currentRoomID - 1;
                tempRoom = roomM.GetRoom(tempRoomID);
                tempDir = "west";
            }
            else if (randDir == 1 && j != 0) {
                tempRoomID = currentRoomID - 100;
                tempRoom = roomM.GetRoom(tempRoomID);
                tempDir = "north";
            }
            else if (randDir == 2 && i != 99) {
                tempRoomID = currentRoomID + 1;
                tempRoom = roomM.GetRoom(tempRoomID);
                tempDir = "east";
            }
            else {
                tempRoomID = currentRoomID + 100;
                tempRoom = roomM.GetRoom(tempRoomID);
                tempDir = "south";
            }
        } while (tempRoom.GetRoomType() != "Forest");
        //add the tracking option to the next room to continue tracking
        roomM.AddTempOptionToRoom(tempRoomID, 11);

        resultText = "You follow the tracks and discern they head " + tempDir + ".";
        UpdateMainText(resultText);
        
    }

    void SpotDeer() {

        string text = "You follow the tracks until you spot it ";
        int randDistance = Random.Range(100, 201);
        randDistance -= randDistance % 10;
        text += "about " + randDistance + " feet away. ";

        
        int weight;
        int randGender = Random.Range(0, 2);
        if(randGender == 0) {
            weight = Random.Range(130, 301);
            //text += "a buck. ";
        }
        else {
            weight = Random.Range(90, 200);
            //text += "a female. ";
        }
        

        if(weight < 150) {
            text += "It looked to be less than 150 pounds.";
        }
        else if( weight < 250) {
            text += "It looked to be about 250 pounds.";
        }
        else {
            text += "It looked to be almost 300 pounds.";
        }

        UpdateMainText(text);
    }

    void AddExitOption() {
        Room room = roomManager.GetComponent<RoomManager>().GetRoom(settingManager.GetComponent<SettingManager>().GetRoom());
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
        Room room = roomManager.GetComponent<RoomManager>().GetRoom(settingManager.GetComponent<SettingManager>().GetRoom());

        mapPanel.SetActive(true);
        mapPanel.transform.GetChild(0).gameObject.GetComponent<RawImage>().texture = room.GetRoomImage();

        for (int i = 0; i < room._numExits; i++) {
            //exitButton = Instantiate(nex)
            nextRoomDecisionObject.GetComponentsInChildren<Text>()[0].text = room._exitTexts[i];
            nextRoomDecisionObject.GetComponent<NextRoomAction>().nextRoomID = room._connectedRooms[i];
            nextRoomDecisionObject.GetComponent<NextRoomAction>().leaveText = room._leaveTexts[i];
            int[,] locations = room.GetButtonLocation();
            var button = Instantiate(nextRoomDecisionObject, new Vector3(locations[i,0], locations[i,1], 0), Quaternion.identity);
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

    //observes the room and adds any options that were hidden
    public void ObserveSurroundings() {

        GameObject.Find("GameManager").GetComponent<GameManager>().SetLastPlayerAction(2);
        int currentRoom = settingManager.GetComponent<SettingManager>().GetRoom();
        RoomManager roomM = roomManager.GetComponent<RoomManager>();

        //if the room has not been observed. Resets after an in-game hour
        if (roomM.GetRoom(currentRoom).GetObserved() == 0) {
            roomM.SetRoomObserved(currentRoom, 1);
            AddRoomOptions();
            //chance to find deer tracks
            if (roomManager.GetComponent<RoomManager>().GetRoom(currentRoom).GetRoomType() == "Forest") {
                int randNum = Random.Range(0, 100);
                if (randNum < GameObject.Find("PlayerManager").GetComponent<PlayerManager>().GetPerception()) {
                    //add option to list and room temp options
                    roomM.GetRoom(currentRoom).AddTempOption(11);
                    AddDecision(11);
                }
            }
            //chance there are resources to pick up based on perception
            if (roomManager.GetComponent<RoomManager>().GetRoom(currentRoom).GetRoomType() == "Forest") {
                int randNum = Random.Range(0, 100);
                if (randNum < GameObject.Find("PlayerManager").GetComponent<PlayerManager>().GetPerception()) {
                    roomM.GetRoom(currentRoom).AddTempOption(12);
                    AddDecision(12);
                    AddItemsToRoom();
                }
            }
        }


        UpdateMainText(roomM.GetRoom(currentRoom).GetObservationText());
        
    }

    void AddItemsToRoom() {
        RoomManager roomM = roomManager.GetComponent<RoomManager>();
        SettingManager sM = settingManager.GetComponent<SettingManager>();
        WorldItemsManager wIM = GameObject.Find("WorldItemsManager").GetComponent<WorldItemsManager>();
        int currentRoom = sM.GetRoom();

        //give one free item
        Room room = roomM.GetRoom(currentRoom);
        int randomItem = Random.Range(0, 3);
        int[] guaranteedItems = wIM.GetGuaranteedItems();
        roomM.AddItemToRoom(guaranteedItems[randomItem], currentRoom);

        //three chances to get additional items based on perception
        int randNum = Random.Range(0, 100);
        randomItem = Random.Range(0, 3);
        for (int i = 0; i < 3; i++) {
            if (randNum < GameObject.Find("PlayerManager").GetComponent<PlayerManager>().GetPerception()) {
                roomM.AddItemToRoom(guaranteedItems[randomItem], currentRoom);
            }
            randNum = Random.Range(0, 100);
            randomItem = Random.Range(0, 3);
        }
    }

    void AddRoomOptions() {
        int currentRoom = settingManager.GetComponent<SettingManager>().GetRoom();
        Room room = roomManager.GetComponent<RoomManager>().GetRoom(currentRoom);
        //add the normal options
        if(room._options != null) {
            for (int i = 0; i < room._options.Length; i++) {
                AddDecision(room._options[i]);
            }
        }
        //add the temporary options
        int[] tempOptions = room.GetTempOptions();
        if(tempOptions != null) {
            for (int i = 0; i < tempOptions.Length; i++) {
                AddDecision(tempOptions[i]);
            }
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

    //add action to panel
    public void AddDecision(int index) {

        bool add;
        // 0 = throw something
        if (index == 0) {
            var obj = Instantiate(combatDecisionObject);
            obj.GetComponent<CombatAction>().displayText = "Throw";
            obj.transform.GetChild(0).GetComponentInChildren<Text>().text = "Throw";
            obj.transform.GetChild(0).GetComponentInChildren<Text>().color = new Color(1f, 1, 1);
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
        else if (index == 3) {
            var obj = Instantiate(combatDecisionObject);
            obj.GetComponent<CombatAction>().displayText = "Attack";
            obj.transform.GetChild(0).GetComponentInChildren<Text>().text = "Attack";
            obj.transform.GetChild(0).GetComponentInChildren<Text>().color = new Color(1f, 1, 1);
            obj.GetComponent<CombatAction>().combatActionID = 1;
            obj.transform.SetParent(decisionScroller.transform, false);
        }
        else if (index == 4) {

        }
        //5 = wait in bed
        else if (index == 5) {

            var obj = Instantiate(waitDecisionObject);
            obj.transform.GetChild(0).GetComponentInChildren<Text>().text = "Wait in bed";
            obj.GetComponent<WaitAction>().SetTextUpdate("Feeling you didn't get enough sleep you get back into bed.");
            obj.transform.SetParent(decisionScroller.transform, false);

        }
        //6 = wait in seat
        else if (index == 6) {
            var obj = Instantiate(waitDecisionObject);
            obj.transform.GetChild(0).GetComponentInChildren<Text>().text = "Wait in seat";
            obj.GetComponent<WaitAction>().SetTextUpdate("You decide to sit down.");
            obj.transform.SetParent(decisionScroller.transform, false);
        }
        //7 = eat a meal
        else if (index == 7) {
            add = true;
            for (int i = 0; i < decisionScroller.transform.childCount; i++) {
                if (decisionScroller.transform.GetChild(i).gameObject.name == "EatDecisionObject(Clone)") {
                    add = false;
                }
            }
            if (add == true) {
                var obj = Instantiate(eatMealDecisionObject);
                obj.transform.SetParent(decisionScroller.transform, false);
            }
        }
        //8 = train sword fighting
        else if (index == 8) {
            var obj = Instantiate(swordTrainingDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
        }
        //9 = run
        else if (index == 9) {
            var obj = Instantiate(combatDecisionObject);
            obj.GetComponent<CombatAction>().displayText = "Retreat";
            obj.transform.GetChild(0).GetComponentInChildren<Text>().text = "Retreat";
            obj.GetComponent<CombatAction>().combatActionID = 3;
            obj.transform.SetParent(decisionScroller.transform, false);
        }//10 = hide
        else if (index == 10) {
            var obj = Instantiate(combatDecisionObject);
            obj.GetComponent<CombatAction>().displayText = "Hide";
            obj.transform.GetChild(0).GetComponentInChildren<Text>().text = "Hide";
            obj.GetComponent<CombatAction>().combatActionID = 4;
            obj.transform.SetParent(decisionScroller.transform, false);
        }
        else if (index == 11) {
            var obj = Instantiate(chanceDecisionObject);
            obj.GetComponent<ChanceAction>().SetActionID(11);
            obj.GetComponentInChildren<Image>().color = new Color(0.3f, 0.1f, 0.1f);
            obj.transform.GetChild(0).GetComponentInChildren<Text>().color = new Color(1, 1, 1);
            obj.transform.GetChild(0).GetComponentInChildren<Text>().text = "Follow deer tracks";
            obj.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { DeerTrackingResult(); });
            obj.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { Destroy(obj); });
            obj.transform.SetParent(decisionScroller.transform, false);
        }
        else if(index == 12) {//look at available items to take
            var obj = Instantiate(pickUpDecisionObject);
            obj.transform.GetChild(0).GetComponentInChildren<Text>().text = "Resources";
            obj.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { ListItemsToPickUp(); });
            obj.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { Destroy(obj); });
            obj.transform.SetParent(decisionScroller.transform, false);
        }
        else if(index == 100) {
            AddTutorialOption(100, new Color(0.3f, 0.1f, 0.1f), new Color(1f, 1f, 1f), "Follow deer tracks");
        }
        else if(index == 101) {
            AddTutorialOption(101, new Color(0.7f, 0, 0), new Color(1, 1, 1), "Take the shot");
        }


        scrollBar.value = 1;
        decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, 0);

    }

    public void AddTutorialOption(int actionID, Color backColor, Color textColor, string displayText) {
        var obj = Instantiate(chanceDecisionObject);
        obj.GetComponent<ChanceAction>().SetActionID(actionID);
        obj.GetComponentInChildren<Image>().color = backColor;
        obj.transform.GetChild(0).GetComponentInChildren<Text>().color = textColor;
        obj.transform.GetChild(0).GetComponentInChildren<Text>().text = displayText;
        obj.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("GameManager").GetComponent<GameManager>().SetLastPlayerAction(actionID); });
        obj.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { Destroy(obj); });
        obj.transform.SetParent(decisionScroller.transform, false);
    }

    //------------------------------------------------------------------------functions for attacking
    //called when the user selects the attack option
    public void Attack(){
        ClearDecisionPanel();

        //add a label for the decision panel
        userFeedBackObject.GetComponentInChildren<Text>().text = "People in the area:";
        var UF = Instantiate(userFeedBackObject);
        UF.transform.SetParent(decisionScroller.transform, false);

        SettingManager sM = settingManager.GetComponent<SettingManager>();

        //allow user to select who to talk to
        Character[] characters = characterManager.GetComponent<CharacterManager>().GetCharactersInRoom(sM.GetRoom());
        for (int i = 0; i < characters.Length; i++){
            if (characters[i].GetStatus() != "Dead") {
                attackPersonDecisionObject.transform.GetChild(0).GetComponentInChildren<Text>().text = characters[i].GetFirstName() + " " + characters[i].GetLastName();
                attackPersonDecisionObject.transform.GetChild(0).GetComponentInChildren<Text>().color = new Color(0.7f, 0, 0);
                attackPersonDecisionObject.GetComponent<AttackAction>().characterID = characters[i].GetID();
                var obj = Instantiate(attackPersonDecisionObject);
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

        //clear panel of opponents
        for(int i = 0; i < opponentPanel.transform.childCount; i++) {
            Destroy(opponentPanel.transform.GetChild(i).gameObject);
        }
        attemptCancelButton.interactable = true;

        for (int i = 0; i < IDs.Length; i++) {
            combatBackgroundPanel.SetActive(true);
            AddOpponent(IDs[i]);
            //potentially add more opponents
            ProvokeAttack(IDs[i]);
        }

        RefreshDecisionList();
    }
    public bool AttackAction(int characterID) {
        int randNum = UnityEngine.Random.Range(0, 100);
        Character tempChar = characterManager.GetComponent<CharacterManager>().GetCharacter(characterID);

        if(randNum < 34) {
            tempChar.SubtractHealth(43);
            if(tempChar.GetHealth() == 0) {
                leaveButton.interactable = true;
                for(int i = 0; i < opponentPanel.transform.childCount; i++) {
                    if(characterManager.GetComponent<CharacterManager>().GetCharacter(opponentPanel.transform.GetChild(i).GetComponent<AttackAction>().characterID).GetHealth() != 0) {
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

    //called by the attack action checking the response of other characters in the room
    public void ProvokeAttack(int characterID) {
        SettingManager sM = GameObject.Find("SettingManager").GetComponent<SettingManager>();
        CharacterBehaviorManager cBM = GameObject.Find("CharacterBehaviorManager").GetComponent<CharacterBehaviorManager>();
        CharacterManager cM = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
        Character[] characters = characterManager.GetComponent<CharacterManager>().GetCharactersInRoom(sM.GetRoom());
        MissionManager mM = GameObject.Find("MissionManager").GetComponent<MissionManager>();

        //if there is a mission going on
        int[] involved = cM.GetCharacter(characterID).GetInvolved();
        if(mM.GetCurrentMission() != 0) {
            if (involved != null) {
                for (int i = 0; i < involved.Length; i++) {
                    //if the character being attacked is part of the mission
                    if (involved[i] == mM.GetCurrentMission()) {
                        //stop the mission
                        mM.StopCurrentMission();
                    }
                }
            }
        }
        
        //for each character find out which side their on
        string feelingsAboutPlayer;
        for(int i = 0; i < characters.Length; i++) {
            feelingsAboutPlayer = cBM.GetCharacterFeelingsTowardsPlayer(characters[i]);

            //if the character is in the same faction as the character being attacked
            if(characters[i].GetFaction() == cM.GetCharacter(characterID).GetFaction()) {
                if (feelingsAboutPlayer != "love" || feelingsAboutPlayer != "devotion") {
                    AddOpponent(characters[i].GetID());
                    return;
                }
            }
            else {
                if (feelingsAboutPlayer != "admiration" || feelingsAboutPlayer != "love" || feelingsAboutPlayer != "devotion") {
                    AddOpponent(characters[i].GetID());
                    return;
                }
            }

            AddAlly(characters[i].GetID());

        }
    }

    public void AddOpponent(int characterID) {
        Character tempChar = characterManager.GetComponent<CharacterManager>().GetCharacter(characterID);
        characterCombatObject.GetComponent<AttackAction>().characterID = characterID;
        characterCombatObject.transform.GetChild(0).GetComponent<Text>().text = tempChar.GetFirstName() + " " + tempChar.GetLastName();
        characterCombatObject.transform.GetChild(2).GetComponent<Text>().text = "34% chance for successful attack";
        characterCombatObject.transform.GetChild(4).GetComponent<Slider>().value = tempChar.GetHealth();
        var obj = Instantiate(characterCombatObject);
        obj.transform.SetParent(opponentPanel.transform, false);
    }

    public void AddAlly(int characterID) {
        Character tempChar = characterManager.GetComponent<CharacterManager>().GetCharacter(characterID);
        characterCombatObject.GetComponent<AttackAction>().characterID = characterID;
        characterCombatObject.transform.GetChild(0).GetComponent<Text>().text = tempChar.GetFirstName() + " " + tempChar.GetLastName();
        characterCombatObject.transform.GetChild(2).GetComponent<Text>().text = "34% chance for successful attack";
        characterCombatObject.transform.GetChild(4).GetComponent<Slider>().value = tempChar.GetHealth();
        var obj = Instantiate(characterCombatObject);
        obj.transform.SetParent(allyPanel.transform, false);
    }

    public void AttemptLeaveCombat() {
        PlayerManager pM = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        int chance = pM.GetAgility() - (7 * combatBackgroundPanel.transform.childCount);
        if(chance < 0) {
            chance = 0;
        }

        int randNum = Random.Range(1, 101);

        if(chance > randNum) {
            combatBackgroundPanel.SetActive(false);
        }
        else {
            attemptCancelButton.interactable = false;
        }

    }
    //-------------------------------------------------------------------------functions for attacking

    //called when user selects the throw action
    public void Throw() {

        ClearDecisionPanel();

        //add a label for the decision panel
        userFeedBackObject.GetComponentInChildren<Text>().text = "Items to throw:";
        var UF = Instantiate(userFeedBackObject);
        UF.transform.SetParent(decisionScroller.transform, false);

        //display available items to throw
        InventoryManager tempInventory = inventoryManager.GetComponent<InventoryManager>();
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
        Character[] characters = characterManager.GetComponent<CharacterManager>().GetCharactersInRoom(sM.GetRoom());
        for (int i = 0; i < characters.Length; i++) {
            throwItemAtDecisionObject.GetComponentsInChildren<Text>()[1].text = characters[i].GetFirstName() + " " + characters[i].GetLastName();
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
        Character tempChar = characterManager.GetComponent<CharacterManager>().GetCharacter(characterID);
        tempChar.SetRelationshipLvl(tempChar.GetRelationshipLvl() - 10);
        inventoryManager.GetComponent<InventoryManager>().RemoveItemByID(itemID);
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

        PlayerManager pM = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();
        int caloriesGained = 0;
        settingManager.GetComponent<SettingManager>().SetTimeScale(0.0083f);

        while (eatingMeal == 1){
            if(pM.GetCalories() < 1000) {
                pM.AddCalories(1);
                caloriesConsumedStatus.text = (++caloriesGained).ToString() + " calories";
                pM.UpdateCalories();
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
        Character[] characters = characterManager.GetComponent<CharacterManager>().GetCharactersInRoom(sM.GetRoom());
        for (int i = 0; i < characters.Length; i++){
            if (characters[i].GetStatus() != "Dead") {
                talkToDecisionObject.transform.GetChild(0).GetComponentInChildren<Text>().text = characters[i].GetFirstName() + " " + characters[i].GetLastName();
                talkToDecisionObject.GetComponent<TalkToOption>().characterID = characters[i].GetID();
                if (characters[i].GetImportance() == 1) {
                    talkToDecisionObject.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 1);
                }
                else {
                    talkToDecisionObject.transform.GetChild(0).GetComponent<Image>().color = new Color(.5f, .5f, .5f, 1);
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
        Character talkingTo = characterManager.GetComponent<CharacterManager>().GetCharacter(characterID);

        //get and display the greeting from the character which updates player knowledge
        ConversationManager convoManager = GameObject.Find("ConversationManager").GetComponent<ConversationManager>();
        StartCoroutine(convoManager.DisplayGreeting(characterID));

        //adds all available convos
        CheckCharacterForConvos(characterID);

        //Update character info panel
        characterManager.GetComponent<CharacterManager>().UpdateInfoPanel(characterID);
        
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
        Character talkingTo = characterManager.GetComponent<CharacterManager>().GetCharacter(characterID);

        if(greeting != "") {
            //display the greeting given
            StartCoroutine(GameObject.FindWithTag("ConversationManager").GetComponent<ConversationManager>().DisplayGreeting(characterID, greeting));
        }

        //update character info panel with all characters info
        characterInfoPanel.GetComponentInChildren<Text>().text = talkingTo.GetFirstName() + " " + talkingTo.GetLastName();

        //Update character info panel
        characterManager.GetComponent<CharacterManager>().UpdateInfoPanel(characterID);

        RefreshDecisionList();
    }

    public void ListItemsToPickUp() {
        SettingManager sM = settingManager.GetComponent<SettingManager>();
        RoomManager rM = roomManager.GetComponent<RoomManager>();
        WorldItemsManager wIM = GameObject.Find("WorldItemsManager").GetComponent<WorldItemsManager>();
        int currentRoom = sM.GetRoom();

        ClearDecisionPanel();

        //add a label for the decision panel
        userFeedBackObject.GetComponentInChildren<Text>().text = "Resources:";
        var UF = Instantiate(userFeedBackObject);
        UF.transform.SetParent(decisionScroller.transform, false);

        Room room = rM.GetRoom(currentRoom);
        List<int[]> resources = room.GetResources();
        GameObject item;
        int resourceID;
        for(int i = 0; i < resources.Count; i++) {
            //display each item in the room
            item = Instantiate(pickUpDecisionObject);
            resourceID = resources[i][0];
            item.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { PickUpItem(resourceID, currentRoom); });
            item.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { Destroy(item); });
            item.transform.GetChild(0).GetComponentInChildren<Text>().text = "Pick up " + wIM.GetItemString(resources[i][0]) + ": " + resources[i][1];
            item.transform.GetChild(0).GetComponent<Button>().image.color = new Color(.7f, .2f, 0f);
            item.transform.SetParent(decisionScroller.transform, false);
        }

        //add cancel button
        var canceButton = Instantiate(cancelDecisionButton);
        canceButton.transform.SetParent(decisionScroller.transform, false);

        //adjust scroll bar
        scrollBar.value = 1;
        if (itemsInDecisionPanel > 7) {
            int difference = itemsInDecisionPanel - 7;
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, -(difference * 35));
        }
        else {
            decisionScroll.offsetMin = new Vector2(decisionScroll.offsetMin.x, 0);
        }

    }

    public void PickUpItem(int worldItemID, int roomID) {
        Debug.Log("Picking up item number " + worldItemID);
        WorldItemsManager wIM = GameObject.Find("WorldItemsManager").GetComponent<WorldItemsManager>();
        RoomManager rM = roomManager.GetComponent<RoomManager>();

        //add the item to inventory
        wIM.AddItemToInventory(worldItemID);
        //remove the item from the room
        rM.RemoveItemFromRoom(worldItemID, roomID);
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
            convoID = characterManager.GetComponent<CharacterManager>().GetCharacter(characterID).GetConvoList()[i];
            if(convoID != -1) {
                GameObject.Find("ConversationManager").GetComponent<ConversationManager>().AddConversation(convoID, characterID);
            }
        }
   
    }

    public void CheckCharacterForTraining(int characterID) {
        CharacterManager cM = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();

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
        CharacterManager cM = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
        for(int i = 0; i < 3; i++) {
            if(cM.GetCharacter(characterID).GetMissions()[i] != 0) {
                int[][] missionTimes = cM.GetCharacter(characterID).GetMissionTimes();
                int[] time = settingManager.GetComponent<SettingManager>().GetTime();
                if (missionTimes[i][0] <= time[0] && time[0] < missionTimes[i][1]) {
                    startMissionDialogue.GetComponent<DialogueAction>().dialogueMessage = "I'm ready";
                    startMissionDialogue.GetComponent<DialogueAction>().effect = 0;
                    startMissionDialogue.GetComponent<DialogueAction>().convoID = -1;
                    startMissionDialogue.GetComponent<DialogueAction>().characterID = characterID;
                    startMissionDialogue.GetComponent<MissionManager>().missionID = cM.GetCharacter(characterID).GetMissions()[i];
                    var obj = Instantiate(startMissionDialogue);
                    obj.transform.SetParent(dialogueOptionsPanel.transform, false);
                }
            }
        }
    }

    public void OpenWaitPanel(string update) {
        UpdateMainText(update);
        sleepWaitBackgroundPanel.SetActive(true);
    }

    public void Wait() {
        fadOutPanel.SetActive(true);
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

            pM.AddToSwordSkill(1);
            pM.UpdateSwordSkill();
            if(pM.GetCalories() > 0){
                pM.SubtractCalories(1);
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
        characterManager.GetComponent<CharacterManager>().ProvokeCharacter(trainerID, 1);
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

        RoomManager rM = roomManager.GetComponent<RoomManager>();
        SettingManager sM = settingManager.GetComponent<SettingManager>();
        CharacterManager cM = characterManager.GetComponent<CharacterManager>();
        GameObject.Find("GameManager").GetComponent<GameManager>().SetLastPlayerAction(20);

        //check if there's an interruption
        int interruptionIndex = CheckForInterruption(20, nextRoomID);
        if(interruptionIndex != -1) {
            InterruptAction(interruptionIndex);
            return;
        }

        //check if any characters have a beef with the player leaving
        Character[] characters = cM.GetCharactersInRoom(sM.GetRoom());
        for(int i = 0; i < characters.Length; i++) {
            if(cM.ProvokeCharacter(characters[i].GetID(), 2)) {
                return;
            }
        }

        //clear the room image panel and buttons
        for (int i = 0; i < indoorPanel.transform.childCount; i++) {
            Destroy(indoorPanel.transform.GetChild(i).gameObject);
        }

        //update current room
        Room room = rM.GetRoom(nextRoomID);
        if (room.GetRoomType() == "Building") {
            sM.SetRoom(room.GetTeleport());
            room = rM.GetRoom(sM.GetRoom());
        }
        else {
            sM.SetRoom(nextRoomID);
        }
        sM.DisplayRoom();

        //bring followers
        PlayerManager pM = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        int[] followers = pM.GetFollowers();
        for(int i = 0; i < 10; i++) {
            if(followers[i] != -1) {
                cM.GetCharacter(followers[i]).SetLocation(sM.GetRoom());
            }
        }

        //update coordinates
        int[] coords = room.GetRoomCoordinates();
        sM.UpdatePosition(coords[0], coords[1]);

        //update main text with the leaving dialogue from the current room to the selected room
        UpdateMainText(leaveText);
        if(room.GetRoomType() == "Forest") {
            sM.AddTime(15, 0);
        }
        else {
            sM.AddTime(0, 30);
        }
        
        //update room image and exit buttons
        indoorPanel.GetComponent<RawImage>().texture = room.GetRoomImage();
        GameObject obj;
        int[,] locations = room.GetButtonLocation();
        for (int i = 0; i < room.GetNumExits(); i++) {
            obj = Instantiate(nextRoomDecisionObject, new Vector3(locations[i, 0], locations[i, 1], 0), Quaternion.identity);
            obj.GetComponentsInChildren<Text>()[0].text = room._exitTexts[i];
            obj.GetComponent<NextRoomAction>().nextRoomID = room._connectedRooms[i];
            obj.GetComponent<NextRoomAction>().leaveText = room._leaveTexts[i];
            obj.transform.SetParent(indoorPanel.transform, false);
        }

        //if guide is on, highlight exit buttons to the destination
        MissionManager mM = GameObject.Find("MissionManager").GetComponent<MissionManager>();
        if (mM.GetGuide() == 1) {
            StartCoroutine(UpdateGuide());
        }

        //update the decision list
        for (int i = 0; i < decisionScroller.transform.childCount; i++) {
            Destroy(decisionScroller.transform.GetChild(i).gameObject);
        }
        RefreshDecisionList();
    }

    public int CheckForInterruption(int actionID, int nextRoomID) {

        //if the player has already been interrupted return 0
        //action ID for leaving is 20: 21 leaving west, 22 leaving north, 23 leaving east...
        if(GameObject.Find("GameManager").GetComponent<GameManager>().GetInterrupted() == 1) {
            return -1;
        }

        //first check if there is an interruption for leaving
        for (int i = 0; i < interruptions.Count; i++) {
            if (interruptions[i].actionID == actionID) {
                return i;
            }
        }

        SettingManager sM = GameObject.Find("SettingManager").GetComponent<SettingManager>();

        //if the next room is west
        if (nextRoomID == sM.GetRoom() - 1) {
            for (int i = 0; i < interruptions.Count; i++) {
                if (interruptions[i].actionID == 21) {
                    return i;
                }
            }
        }
        //if next room is north
        else if(nextRoomID == sM.GetRoom() - 100) {
            for (int i = 0; i < interruptions.Count; i++) {
                if (interruptions[i].actionID == 22) {
                    return i;
                }
            }
        }
        //if next room is east
        else if (nextRoomID == sM.GetRoom() + 1) {
            for (int i = 0; i < interruptions.Count; i++) {
                if (interruptions[i].actionID == 23) {
                    return i;
                }
            }
        }
        //if next room is south
        else if (nextRoomID == sM.GetRoom() + 100) {
            for (int i = 0; i < interruptions.Count; i++) {
                if (interruptions[i].actionID == 24) {
                    return i;
                }
            }
        }

        return -1;
    }

    //update that the player has been interrupted and use the talkto function
    public void InterruptAction(int index) {
        GameObject.Find("GameManager").GetComponent<GameManager>().SetInterrupted();
        TalkTo(interruptions[index].characterID, interruptions[index].characterText);
    }

    public void AddInterruption(int actionID, int characterID, string interruptText) {
        Interruption interrupt = new Interruption();
        interrupt.actionID = actionID;
        interrupt.characterID = characterID;
        interrupt.characterText = interruptText;
        interruptions.Add(interrupt);
    }
    public void RemoveInterruption(int actionID) {
        for(int i = 0; i < interruptions.Count; i++) {
            if(interruptions[i].actionID == actionID) {
                interruptions.RemoveAt(i);
            }
        }
        Debug.Log("Attempted to remove an action interrupt that was not there");
    }

    public void UpdateMainText(string text) {

        if(mainTextAreaPanel.transform.childCount > 0) {
            Destroy(mainTextAreaPanel.transform.GetChild(0).gameObject);
        }

        var mainText = Instantiate(mainTextObject);
        mainText.transform.GetChild(0).GetComponent<Text>().text = text;
        mainText.transform.SetParent(mainTextAreaPanel.transform, false);
    }

    IEnumerator UpdateGuide() {
        yield return new WaitForEndOfFrame();
        GameObject.Find("MissionManager").GetComponent<MissionManager>().Guide();
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
        Character guard1 = characterManager.GetComponent<CharacterManager>().GetCharacter(1005);
        Character guard2 = characterManager.GetComponent<CharacterManager>().GetCharacter(1006);
        int location = GameObject.Find("SettingManager").GetComponent<SettingManager>().GetRoom();

        guard1.SetLocation(location);
        guard2.SetLocation(location);

        GameObject.Find("ConversationManager").GetComponent<ConversationManager>().StartConversation(3, 1005);
       
    }
}
