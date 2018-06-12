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
    public GameObject attackDecisionObject;
    public GameObject attackPersonDecisionObject;
    public GameObject eatMealDecisionObject;
    public GameObject swordTrainingDecisionObject;

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
    public GameObject characterResponse;
    public GameObject playerResponse;
    public GameObject dialoguePanel;
    public Scrollbar converstationScrollBar;
    public GameObject conversationScroll;
    public RectTransform conScrollRectTransform;

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

        bool add;

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
        if (index == 3){
            var obj = GameObject.Instantiate(attackDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        //5 = wait in bed
        if (index == 5) {
            add = true;
            for (int i = 0; i < decisionScroller.transform.childCount; i++){
                if (decisionScroller.transform.GetChild(i).gameObject.name == "WaitInBedDecisionObject(Clone)"){
                    add = false;
                }
            }
            if (add == true){
                var obj = GameObject.Instantiate(waitInBedDecisionObject);
                obj.transform.SetParent(decisionScroller.transform, false);
                itemsInDecisionPanel++;
            }
        }
        //6 = wait in seat
        if (index == 6) {
            add = true;
            for(int i = 0; i < decisionScroller.transform.childCount; i++){
                if (decisionScroller.transform.GetChild(i).gameObject.name == "WaitInSeatingDecisionObject(Clone)"){
                    add = false;
                }
            }
            if (add == true){
                var obj = GameObject.Instantiate(waitInSeatingDecisionObject);
                obj.transform.SetParent(decisionScroller.transform, false);
                itemsInDecisionPanel++;
            }
        }
        //7 = eat a meal
        if (index == 7){
            add = true;
            for (int i = 0; i < decisionScroller.transform.childCount; i++){
                if (decisionScroller.transform.GetChild(i).gameObject.name == "EatDecisionObject(Clone)"){
                    add = false;
                }
            }
            if (add == true){
                var obj = GameObject.Instantiate(eatMealDecisionObject);
                obj.transform.SetParent(decisionScroller.transform, false);
                itemsInDecisionPanel++;
            }
        }
        //8 = train sword fighting
        if (index == 8){
            var obj = GameObject.Instantiate(swordTrainingDecisionObject);
            obj.transform.SetParent(decisionScroller.transform, false);
            itemsInDecisionPanel++;
        }
        //9 = 

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
        mainText.text = roomManager.GetComponent<RoomManager>().GetRoom(currentRoom)._observationText;
        settingManager.GetComponent<SettingManager>().roomObserved = 1;
        
    }

    //called when the user selects the attack option
    public void Attack(){
        ClearDecisionPanel();

        //allow user to select who to talk to
        int numCharacters = settingManager.GetComponent<SettingManager>().numCharactersInSetting;
        for (int i = 0; i < numCharacters; i++){
            attackPersonDecisionObject.GetComponentsInChildren<Text>()[1].text = settingManager.GetComponent<SettingManager>().charactersInSetting[i].GetFirstName()
                + " " + settingManager.GetComponent<SettingManager>().charactersInSetting[i].GetLastName();
            attackPersonDecisionObject.GetComponent<AttackPersonAction>().characterID = settingManager.GetComponent<SettingManager>().charactersInSetting[i].GetID();
            var obj = GameObject.Instantiate(attackPersonDecisionObject);
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

    //called when the user selects the person to attack
    public void AttackPerson(int ID){
        Character character = characterManager.GetComponent<CharactersManager>().GetCharacter(ID);
        Debug.Log("You attacked " + character.GetFirstName() + " " + character.GetLastName());

        RefreshDecisionList();
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

        while (eatingMeal == 1){

            settingManager.GetComponent<SettingManager>().SetTimeScale(0.083f);

            playerManager.GetComponent<PlayerManager>().calories++;

            caloriesConsumedStatus.text = (playerManager.GetComponent<PlayerManager>().calories - startingCalories).ToString() + " calories";

            //if ((playerManager.GetComponent<PlayerManager>().calories - startingCalories) % 50 == 0){
            //    settingManager.GetComponent<SettingManager>().AddTime(1);
            //}

            playerManager.GetComponent<PlayerManager>().UpdateCalories();

            yield return new WaitForSeconds(0.1f);
        }

    }

    public void StopEatingMeal(){
        eatingMeal = 0;
        settingManager.GetComponent<SettingManager>().SetTimeScale(1f);
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
    public void TalkTo(int characterID){

        //set the conversation panel to active and get the character being talked to
        conversationBackgroundPanel.SetActive(true);
        Character talkingTo = characterManager.GetComponent<CharactersManager>().GetCharacter(characterID);

        //clear previous dialogue
        for (int i = 0; i < conversationScroll.transform.childCount; i++) {
            Destroy(conversationScroll.transform.GetChild(i).gameObject);
        }

        //Add greeting from the character, done before adding to info panel to update player knowledge
        characterResponse.GetComponentsInChildren<Text>()[0].text = GetGreeting(characterID);
        var charResponse = GameObject.Instantiate(characterResponse);
        charResponse.transform.SetParent(conversationScroll.transform, false);

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

        //reset scrollbar and scroll area
        converstationScrollBar.value = 0;
        conScrollRectTransform.offsetMax = new Vector2(-15, 0);

        //clear previous dialogue options
        for(int i = 0; i < dialogueOptionsPanel.transform.childCount; i++){
            Destroy(dialogueOptionsPanel.transform.GetChild(i).gameObject);
        }

        //add dialogue options
        AddDialogueOption(0);
        AddDialogueOption(1);

        RefreshDecisionList();
    }

    //called when the user selects from the dialogue options
    public void MakeDialogueChoice(string message, int dialogueID){
        playerResponse.GetComponentInChildren<Text>().text = message;
        var response = GameObject.Instantiate(playerResponse);
        response.transform.SetParent(conversationScroll.transform, false);

        //add room if necessary
        ExpandConversationScroll();

        //Generate characters response
        if (dialogueID < 10000){
            AskQuestion(dialogueID);
        }
        else{
            MakeAComment(dialogueID);
        }
    }

    public string GetGreeting(int characterID){
        Character tempChar = characterManager.GetComponent<CharactersManager>().GetCharacter(characterID);
        int[] traits = tempChar.GetTraits();
        for(int i = 0; i < 5; i++) {
            if (traits[i] == 0) {
                tempChar.SetPlayerKnowledge(i);
                return "Well helloooooo there!";
            }
            if (traits[i] == 1) {
                tempChar.SetPlayerKnowledge(i);
                return "Hello there Mr. inquisitive.";
            }
            if (traits[i] == 2) {
                tempChar.SetPlayerKnowledge(i);
                return "Haha! Fine day isn't it!";
            }
            if (traits[i] == 14) {
                tempChar.SetPlayerKnowledge(i);
                return "What do you want kid?";
            }
            if (traits[i] == 17) {
                tempChar.SetPlayerKnowledge(i);
                return "HEY!";
            }
            if (traits[i] == 19) {
                tempChar.SetPlayerKnowledge(i);
                return "Hello good sir!";
            }
            if (traits[i] == 25) {
                tempChar.SetPlayerKnowledge(i);
                return "...";
            }
        }
        return "Hello child";
    }

    public void ExpandConversationScroll(){
        int numItems = conversationScroll.transform.childCount;
        if (numItems > 5)
        {
            int difference = numItems - 5;
            conScrollRectTransform.offsetMax = new Vector2(-15, (difference * 30));
        }
        else
        {//else reset
            conScrollRectTransform.offsetMax = new Vector2(-15, 0);
        }
    }

    public void MakeAComment(int commentID){

        if (commentID == 0){//player commented on the characters skills

        }

    }

    public void AskQuestion(int playerQuestion){
        //clear dialogue options
        for (int i = 0; i < dialogueOptionsPanel.transform.childCount; i++){
            Destroy(dialogueOptionsPanel.transform.GetChild(i).gameObject);
        }

        if (playerQuestion == 0){//Player is asking about current events{
            characterResponse.GetComponentsInChildren<Text>()[0].text = "What would you like to know?";
            var charResponse = GameObject.Instantiate(characterResponse);
            charResponse.transform.SetParent(conversationScroll.transform, false);

            //add dialogue options
            AddDialogueOption(2);//days events
            AddDialogueOption(3);//protectors leaders

        }
        if (playerQuestion == 1){//Player is asking about the weather
            string weather = settingManager.GetComponent<SettingManager>().GetWeather();
            characterResponse.GetComponentsInChildren<Text>()[0].text = "It seams to be a bit " + weather + ". Might want to wear a coat.";
            var charResponse = GameObject.Instantiate(characterResponse);
            charResponse.transform.SetParent(conversationScroll.transform, false);

            //add dialogue options
            AddDialogueOption(0);//ask about current events
            AddDialogueOption(1);//ask about weather
        }
        if (playerQuestion == 2){//if player asks about days events
            characterResponse.GetComponentsInChildren<Text>()[0].text = "There will be a feast to celebrate the election of the new king.";
            var charResponse = GameObject.Instantiate(characterResponse);
            charResponse.transform.SetParent(conversationScroll.transform, false);

            //add dialogue options
            AddDialogueOption(0);
            AddDialogueOption(1);
        }
        if (playerQuestion == 3) {//if player asks about leaders
            characterResponse.GetComponentsInChildren<Text>()[0].text = "They are the true heroes of this land.";
            var charResponse = GameObject.Instantiate(characterResponse);
            charResponse.transform.SetParent(conversationScroll.transform, false);

            //add dialogue options
            AddDialogueOption(0);
            AddDialogueOption(1);
        }
        if (playerQuestion == 4) {//if player asks about their rank
            characterResponse.GetComponentsInChildren<Text>()[0].text = "When the lords deem you ready.";
            var charResponse = GameObject.Instantiate(characterResponse);
            charResponse.transform.SetParent(conversationScroll.transform, false);

            //add dialogue options
            AddDialogueOption(0);
            AddDialogueOption(1);
        }
        if (playerQuestion == 5) {//if player asks about the new king
            characterResponse.GetComponentsInChildren<Text>()[0].text = "She is no king.";
            var charResponse = GameObject.Instantiate(characterResponse);
            charResponse.transform.SetParent(conversationScroll.transform, false);

            //add dialogue options
            AddDialogueOption(0);
            AddDialogueOption(1);
        }
        if (playerQuestion == 6) {//if player asks about the character
            characterResponse.GetComponentsInChildren<Text>()[0].text = "Well, I am ...";
            var charResponse = GameObject.Instantiate(characterResponse);
            charResponse.transform.SetParent(conversationScroll.transform, false);

            //add dialogue options
            AddDialogueOption(0);
            AddDialogueOption(1);
        }

    }

    public void AddDialogueOption(int optionID){

        GameObject dialogueObj;

        if (optionID == 0){
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Ask about current events";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "Anything interesting going on?";
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 0;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
        if (optionID == 1){
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Ask about the weather";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "How's the weather?";
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 1;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
        if (optionID == 2){
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Ask about the day";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "What's the day going to be like?";
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 2;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
        if (optionID == 3){
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Ask about the leaders of the protectors";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "Who's in command of The Protectors?";
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 3;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
        if (optionID == 4){
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Ask about your rank";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "When will I become a protector?";
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 4;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
        if (optionID == 5){
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Ask about the new king";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "What is the new king like?";
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 5;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
        if (optionID == 6) {
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Ask about them";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "Tell me about yourself";
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 6;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }

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
        settingManager.GetComponent<SettingManager>().SetCharactersInSetting(characters);
        //update the decision list
        RefreshDecisionList();

    }
}
