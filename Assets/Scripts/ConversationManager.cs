using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour {

    public GameObject dialogueDecisionObject;
    public GameObject dialogueOptionsPanel;
    public GameObject characterResponseObject;
    public GameObject playerResponseObject;
    public GameObject conversationScroll;
    public GameObject startMissionDialogueObject;

    public int leftTraining = 0;

    struct PlayerResponse {
        public int ID;
        public string labelText;
        public string dialogueText;
        public int characterResponseID;
    }

    struct CharacterResponse {
        public string text;
        public int numDialogueOptions;
        public int[] dialogueOptions;
        
    }

    CharacterResponse[] characterResponses = new CharacterResponse[10];
    PlayerResponse[] playerResponses = new PlayerResponse[10];

	// Use this for initialization
	void Start () {

        playerResponses[0].labelText = "Ask about current events";
        playerResponses[0].dialogueText = "Anything interesting going on?";
        playerResponses[0].ID = 0;
        playerResponses[0].characterResponseID = 0;

        playerResponses[1].labelText = "Ask about the day";
        playerResponses[1].dialogueText = "What's the day going to be like?";
        playerResponses[1].ID = 1;
        playerResponses[1].characterResponseID = 1;

        playerResponses[2].labelText = "Ask about the leaders of the protectors";
        playerResponses[2].dialogueText = "Who's in command of The Protectors?";
        playerResponses[2].ID = 2;
        playerResponses[2].characterResponseID = 2;

        playerResponses[3].labelText = "Ask about your rank";
        playerResponses[3].dialogueText = "When will I become a protector?";
        playerResponses[3].ID = 3;
        playerResponses[3].characterResponseID = 3;

        playerResponses[4].labelText = "Ask about the new king";
        playerResponses[4].dialogueText = "What is the new king like?";
        playerResponses[4].ID = 4;
        playerResponses[4].characterResponseID = 4;

        playerResponses[5].labelText = "Ask about the character";
        playerResponses[5].dialogueText = "Tell me about yourself";
        playerResponses[5].ID = 10;
        playerResponses[5].characterResponseID = 10;

        playerResponses[6].labelText = "Ask about the weather";
        playerResponses[6].dialogueText = "How's the weather?";
        playerResponses[6].ID = 11;
        playerResponses[6].characterResponseID = 11;

        playerResponses[7].labelText = "Talk about something else";
        playerResponses[7].dialogueText = "There was something else I wanted to talk about";
        playerResponses[7].ID = 12;
        playerResponses[7].characterResponseID = 12;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator DisplayGreeting(int characterID) {

        characterResponseObject.GetComponentsInChildren<Text>()[0].text = GetGreeting(characterID);
        var charResponse = Instantiate(characterResponseObject);
        charResponse.transform.SetParent(conversationScroll.transform, false);

        yield return new WaitForSeconds(0.00001f);
        GameObject placeholder = GameObject.Find("Placeholder");
        conversationScroll.transform.GetChild(conversationScroll.transform.childCount - 1).transform.SetParent(placeholder.transform, false);
        placeholder.transform.GetChild(0).transform.SetParent(conversationScroll.transform, false);

        if (characterID == 1000 &&
            GameObject.FindWithTag("SettingManager").GetComponent<SettingManager>().GetTime()[0] < 11 &&
            GameObject.FindWithTag("SettingManager").GetComponent<SettingManager>().GetTime()[0] > 5) {//if character is gregory highlark and time is right, add training option

            AddDialogueOption(20, characterID);
        }else if (characterID == 1000 && GameObject.FindWithTag("SettingManager").GetComponent<SettingManager>().GetTime()[0] == 12) {
            AddMissionDialogueOption(characterID, 0);
        }
        else {
            AddDialogueOption(0, characterID);
            AddDialogueOption(1, characterID);
            AddDialogueOption(5, characterID);
            AddDialogueOption(6, characterID);
        }
        
    }

    public IEnumerator DisplayGreeting(int characterID, string greeting) {

        characterResponseObject.GetComponentsInChildren<Text>()[0].text = greeting;
        var charResponse = Instantiate(characterResponseObject);
        charResponse.transform.SetParent(conversationScroll.transform, false);

        yield return new WaitForSeconds(0.00001f);
        GameObject placeholder = GameObject.Find("Placeholder");
        conversationScroll.transform.GetChild(conversationScroll.transform.childCount - 1).transform.SetParent(placeholder.transform, false);
        placeholder.transform.GetChild(0).transform.SetParent(conversationScroll.transform, false);

        //if character is gregory highlark and time is right, add training option
        if (characterID == 1000 && 
            GameObject.FindWithTag("SettingManager").GetComponent<SettingManager>().GetTime()[0] < 11 &&
            GameObject.FindWithTag("SettingManager").GetComponent<SettingManager>().GetTime()[0] > 5) {

            AddDialogueOption(20, characterID);
        }
        if (characterID == 1000 &&
            GameObject.FindWithTag("SettingManager").GetComponent<SettingManager>().GetTime()[0] == 12) {

            AddMissionDialogueOption(characterID, 0);
        }

    }

    //called by decision manager when player selects the person to talk to
    public string GetGreeting(int characterID) {
        Character tempChar = GameObject.FindWithTag("CharacterManager").GetComponent<CharactersManager>().GetCharacter(characterID);

        if (tempChar.GetID() == 1000) {
            tempChar.SetPlayerKnowledge(1);
            int[] time = GameObject.FindWithTag("SettingManager").GetComponent<SettingManager>().GetTime();
            if (time[0] < 7) {
                return "Well, actually on time I see, " + GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>().GetName() + ". Let's begin.";
            }
            else if (time[0] >= 7 && time[0] < 11) {
                return "Well it's about time " + GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>().GetName() + "! C'mon you lazy idiot, take out your sword.";
            }
            else if (time[0] < 12) {
                return "Go eat.";
            }
            else if (time[0] >= 12) {
                return "Let's go! Time for hunting!";
            }
        }

        int[] traits = tempChar.GetTraits();
        for (int i = 0; i < 5; i++) {
            if (traits[i] == 0) {
                tempChar.SetPlayerKnowledge(i);
                return "Well helloooooo there!";
            }
            if (traits[i] == 1) {
                tempChar.SetPlayerKnowledge(i);
                if (GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>().gender == 0) {
                    return "Hello there Mr. inquisitive.";
                }
                return "Hello there Ms. inquisitive.";
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

    //display the dialogue message that the player selected
    public void DisplayDialogueChoice(string message, int dialogueID) {
        playerResponseObject.GetComponentInChildren<Text>().text = message;
        var response = GameObject.Instantiate(playerResponseObject);
        response.transform.SetParent(conversationScroll.transform, false);

        //add room if necessary
        //Debug.Log("EXPANING WITH DIALOGUE CHOICE!!!!");
        ExpandConversationScroll();
    }

    //sole perpose is to call DisplayResponse
    public void CallDisplayResponse(int questionID, int characterID) {
        StartCoroutine(DisplayResponseToMessage(questionID, characterID));
    }

    //called by calldisplayresponse because IEnumerator can't be called by an object that will be destroyed
    public IEnumerator DisplayResponseToMessage(int messageID, int characterID) {

        GameObject placeholder = GameObject.Find("Placeholder");

        if(messageID == 0) {//asking about current events
            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "What would you like to know?";
            var charResponse = Instantiate(characterResponseObject);
            charResponse.transform.SetParent(placeholder.transform, false);

            for (int i = 0; i < dialogueOptionsPanel.transform.childCount; i++) {
                Destroy(dialogueOptionsPanel.transform.GetChild(i).gameObject);
            }

            AddDialogueOption(2, characterID);
            AddDialogueOption(7, characterID);
        }
        else if(messageID == 1) {//asking about the day
            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "For recruits such as yourself there is training until 11. Then you hunt until 5. There will be a feast later to celebrate the election of the new king.";
            var charResponse = Instantiate(characterResponseObject);
            charResponse.transform.SetParent(placeholder.transform, false);

            for (int i = 0; i < dialogueOptionsPanel.transform.childCount; i++) {
                Destroy(dialogueOptionsPanel.transform.GetChild(i).gameObject);
            }

            AddDialogueOption(4, characterID);
            AddDialogueOption(7, characterID);
        }
        else if(messageID == 2) {//asking about leaders
            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "They have performed great feats and were deamed worthy by the previous leaders.";
            var charResponse = Instantiate(characterResponseObject);
            charResponse.transform.SetParent(placeholder.transform, false);

            for (int i = 0; i < dialogueOptionsPanel.transform.childCount; i++) {
                Destroy(dialogueOptionsPanel.transform.GetChild(i).gameObject);
            }

            AddDialogueOption(3, characterID);
            AddDialogueOption(7, characterID);
        }
        else if(messageID == 3) {//asking about rank
            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "When the lords deem you ready.";
            var charResponse = Instantiate(characterResponseObject);
            charResponse.transform.SetParent(placeholder.transform, false);
        }
        else if(messageID == 4) {//asking about the king
            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "Most of the protectors believe she cheated her way to the throne and is too young to rule.";
            var charResponse = Instantiate(characterResponseObject);
            charResponse.transform.SetParent(placeholder.transform, false);
        }
        else if (messageID == 10) {//asking about character
            var charResponse = Instantiate(GetTraitResponse(messageID, characterID));
            charResponse.transform.SetParent(placeholder.transform, false);
        }
        else if (messageID == 11) {//asking about weather
            var charResponse = Instantiate(GetWeatherResponse(characterID));
            charResponse.transform.SetParent(placeholder.transform, false);
        }
        else if (messageID == 12) {//talk about something else
            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "Yes?";
            var charResponse = Instantiate(characterResponseObject);
            charResponse.transform.SetParent(placeholder.transform, false);

            for (int i = 0; i < dialogueOptionsPanel.transform.childCount; i++) {
                Destroy(dialogueOptionsPanel.transform.GetChild(i).gameObject);
            }
            AddDialogueOption(0, characterID);
            AddDialogueOption(1, characterID);
            AddDialogueOption(5, characterID);
            AddDialogueOption(6, characterID);
        }
        else if (messageID == 20) {//wanting to begin training
            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "Let's get to it.";
            var charResponse = Instantiate(characterResponseObject);
            charResponse.transform.SetParent(placeholder.transform, false);

            StartCoroutine(GameObject.FindWithTag("DecisionManager").GetComponent<DecisionManager>().DisplayTrainingWindow(characterID));

        }
        else if(messageID == 22) {//accepting arrest

        }
        else if(messageID == 23) {//resist arrest
            GameObject.Find("ConversationBackgroundPanel").SetActive(false);
            int[] IDs = { 1005, 1006 };
            GameObject.Find("DecisionManager").GetComponent<DecisionManager>().AttackPeople(IDs);

            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "Then draw your sword.";
            var charResponse = Instantiate(characterResponseObject);
            charResponse.transform.SetParent(placeholder.transform, false);
        }
        else if (messageID == 30) {
            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "Alright. To the woods beyond the archway over there.";
            var charResponse = Instantiate(characterResponseObject);
            charResponse.transform.SetParent(placeholder.transform, false);
        }

        //pause added so that the gameobject has time to be placed in the placeholder to later be transformed into the conversation panel because otherwise it wont account for the dynamic size
        yield return new WaitForSeconds(0.001f);
        placeholder.transform.GetChild(0).transform.SetParent(conversationScroll.transform, false);
        

        ExpandConversationScroll();
    }

    //type of dialogue option that reveals a trait about the character
    public GameObject GetTraitResponse(int questionID, int characterID) {
        GameObject characterManager = GameObject.FindWithTag("CharacterManager");
        Character tempChar = GameObject.FindWithTag("CharacterManager").GetComponent<CharactersManager>().GetCharacter(characterID);
        int randNum = UnityEngine.Random.Range(0, 5);
        characterResponseObject.GetComponentsInChildren<Text>()[0].text = "Well I...";
        tempChar.SetPlayerKnowledge(randNum);
        characterManager.GetComponent<CharactersManager>().UpdateInfoPanel(characterID);

        return characterResponseObject;

    }

    //display response to question about weather
    public GameObject GetWeatherResponse(int characterID) {
        string weather = GameObject.FindWithTag("SettingManager").GetComponent<SettingManager>().GetWeather();
        if(weather == "rainy") {
            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "It looks a bit " + weather + " out there. You might want to wear a coat.";
        }
        if (weather == "windy") {
            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "It's blowing me away how " + weather + " it is.";
        }
        if (weather == "snowy") {
            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "It's cold out there. Haven't seen this much snow in a while.";
        }
        if (weather == "sunny") {
            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "It's beautiful outside.";
        }
        if (weather == "misty") {
            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "It's strangly misty. Gives me the creeps.";
        }

        return characterResponseObject;
    }

    //add dialogue option based on options ID, pass in character to pass on to the response when the dialogue is used
    public void AddDialogueOption(int optionID, int characterID) {

        GameObject dialogueObj;

        if (optionID == 20) {//if dialogue is to begin training
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Begin training";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "I'm ready to start training";
            dialogueDecisionObject.GetComponent<DialogueAction>().characterID = characterID;
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 20;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
        else if (optionID == 21) {
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Leave";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "I'm leaving";
            dialogueDecisionObject.GetComponent<DialogueAction>().characterID = characterID;
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 21;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
        else if(optionID == 22) {
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Come quietly";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "Alright, I'll cooporate";
            dialogueDecisionObject.GetComponent<DialogueAction>().characterID = characterID;
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 22;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
        else if(optionID == 23) {
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Refuse";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "I'm not going with you";
            dialogueDecisionObject.GetComponent<DialogueAction>().characterID = characterID;
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 23;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
        else{
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = playerResponses[optionID].labelText;
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = playerResponses[optionID].dialogueText;
            dialogueDecisionObject.GetComponent<DialogueAction>().characterID = characterID;
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = playerResponses[optionID].ID;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }

    }

    //add dialogue option based on options ID, pass in character to pass on to the response when the dialogue is used
    public void AddMissionDialogueOption(int characterID, int missionID) {

        GameObject dialogueObj;

        startMissionDialogueObject.GetComponentsInChildren<Text>()[1].text = "Start mission";
        startMissionDialogueObject.GetComponent<DialogueAction>().dialogueMessage = "I'm ready";
        startMissionDialogueObject.GetComponent<DialogueAction>().characterID = characterID;
        startMissionDialogueObject.GetComponent<DialogueAction>().messageID = 30;
        startMissionDialogueObject.GetComponent<Missions>().missionID = missionID;
        dialogueObj = GameObject.Instantiate(startMissionDialogueObject);
        dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);

    }

    public void ExpandConversationScroll() {

        int numItems = conversationScroll.transform.childCount;
        float totalHeight = 0;
        for (int i = 0; i < numItems; i++) {
            totalHeight += conversationScroll.transform.GetChild(i).GetComponent<RectTransform>().rect.height;
        }

        float scrollHeight = conversationScroll.GetComponent<RectTransform>().rect.height;

        if (totalHeight > scrollHeight) {
            //if (scrollHeight > newHeight) {
            //    newHeight = scrollHeight;
            //}
            Debug.Log("Value to be added = " + ((totalHeight - scrollHeight) + 40));
            Debug.Log("totalHeight " + totalHeight);
            Debug.Log("Before" + scrollHeight);
            conversationScroll.GetComponent<RectTransform>().offsetMax = new Vector2(-15, (totalHeight - scrollHeight) + 40);
            Debug.Log("After " + conversationScroll.GetComponent<RectTransform>().rect.height);
        }
        else {//else reset
            conversationScroll.GetComponent<RectTransform>().offsetMax = new Vector2(-15, 0);
        }

    }
}
