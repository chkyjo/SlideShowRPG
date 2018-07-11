using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour {

    public GameObject dialogueDecisionObject;
    public GameObject dialogueOptionsPanel;
    public GameObject characterResponseObject;
    public GameObject playerResponseObject;
    public GameObject conversationScroll;
    public GameObject startMissionDialogueObject;
    public GameObject conversationBackgroundPanel;

    CharacterResponseNode[] prisonCharResponses = new CharacterResponseNode[100];
    PlayerResponseNode[] prisonPlayerResponses = new PlayerResponseNode[100];
    float oldHeight;

    struct PlayerResponse {
        public int ID;
        public string labelText;
        public string dialogueText;
        public int characterResponseID;
    }

    PlayerResponse[] responses = new PlayerResponse[10];

    struct CharacterResponse {
        public string text;
        public int numDialogueOptions;
        public int[] dialogueOptions;
    }

    public struct CharacterResponseNode {
        public int responseID;
        public string response;
        public int[] playerOptions;
    }
    public struct PlayerResponseNode {
        public int responseID;
        public string response;
        public int characterResponseID;
    }

	// Use this for initialization
	void Start () {

        InitializeConversations();

        responses[0].labelText = "Ask about current events";
        responses[0].dialogueText = "Anything interesting going on?";
        responses[0].ID = 0;
        responses[0].characterResponseID = 0;

        responses[1].labelText = "Ask about the day";
        responses[1].dialogueText = "What's the day going to be like?";
        responses[1].ID = 1;
        responses[1].characterResponseID = 1;

        responses[2].labelText = "Ask about the leaders of the protectors";
        responses[2].dialogueText = "Who's in command of The Protectors?";
        responses[2].ID = 2;
        responses[2].characterResponseID = 2;

        responses[3].labelText = "Ask about your rank";
        responses[3].dialogueText = "When will I become a protector?";
        responses[3].ID = 3;
        responses[3].characterResponseID = 3;

        responses[4].labelText = "Ask about the new king";
        responses[4].dialogueText = "What is the new king like?";
        responses[4].ID = 4;
        responses[4].characterResponseID = 4;

        responses[5].labelText = "Ask about the character";
        responses[5].dialogueText = "Tell me about yourself";
        responses[5].ID = 10;
        responses[5].characterResponseID = 10;

        responses[6].labelText = "Ask about the weather";
        responses[6].dialogueText = "How's the weather?";
        responses[6].ID = 11;
        responses[6].characterResponseID = 11;

        responses[7].labelText = "Talk about something else";
        responses[7].dialogueText = "There was something else I wanted to talk about";
        responses[7].ID = 12;
        responses[7].characterResponseID = 12;
    }

    void InitializeConversations() {
        StreamReader file = new StreamReader(Application.persistentDataPath + "/PrisonConvo.txt", true);

        if (file == null) {
            Debug.Log("Error file not found");
            return;
        }

        int characterResponseIndex = 0;
        int playerResponseIndex = 0;

        int numChildren = 0;
        int[] childIndexes = new int[5];
        string nextLine;

        //get root data
        string rootString = file.ReadLine();
        prisonCharResponses[0].responseID = 0;
        prisonCharResponses[0].response = rootString.TrimStart('(');

        while (!file.EndOfStream) {
            nextLine = file.ReadLine();
            if (nextLine.StartsWith(":")) {//if child index add to child indexes
                childIndexes[numChildren] = Convert.ToInt16(nextLine.TrimStart(':'));
                numChildren++;
            }
            else if (numChildren != 0) {//if not a child index and numchildren is not 0 add children to character response
                prisonCharResponses[characterResponseIndex].playerOptions = new int[numChildren];
                for (int i = 0; i < numChildren; i++) {
                    prisonCharResponses[characterResponseIndex].playerOptions[i] = childIndexes[i];
                }
                if (characterResponseIndex == 0) {//if root also up the index
                    characterResponseIndex++;
                }
                numChildren = 0;
            }

            if (nextLine.StartsWith(")")) {//if player response add the string
                prisonPlayerResponses[playerResponseIndex].response = nextLine.TrimStart(')');
            }

            if (nextLine.StartsWith("(")) {//if character response add the string
                prisonCharResponses[characterResponseIndex].response = nextLine.TrimStart('(');
            }

            if (nextLine.StartsWith("-")) {//if IDs add Ids and up index for both
                string[] splitByComma = nextLine.TrimStart('-').Split(',');
                prisonPlayerResponses[playerResponseIndex].responseID = Convert.ToInt16(splitByComma[0]);
                prisonPlayerResponses[playerResponseIndex].characterResponseID = prisonPlayerResponses[playerResponseIndex].responseID + 1;
                prisonCharResponses[characterResponseIndex].responseID = Convert.ToInt16(splitByComma[1]);

                playerResponseIndex++;
                characterResponseIndex++;
            }
        }
    }

    public IEnumerator DisplayGreeting(int characterID) {

        characterResponseObject.GetComponentsInChildren<Text>()[0].text = GetGreeting(characterID);
        var charResponse = Instantiate(characterResponseObject);
        charResponse.transform.SetParent(GameObject.Find("Placeholder").transform, false);

        yield return new WaitForSeconds(0.00001f);
        charResponse.transform.SetParent(conversationScroll.transform, false);

        AddDialogueOption(0, characterID);
        AddDialogueOption(1, characterID);
        AddDialogueOption(5, characterID);
        AddDialogueOption(6, characterID);
        
        
    }

    public IEnumerator DisplayGreeting(int characterID, string greeting) {

        characterResponseObject.GetComponentsInChildren<Text>()[0].text = greeting;
        var charResponse = Instantiate(characterResponseObject);
        charResponse.transform.SetParent(GameObject.Find("Placeholder").transform, false);

        yield return new WaitForSeconds(0.001f);
        charResponse.transform.SetParent(conversationScroll.transform, false);
    }

    //called by decision manager when player selects the person to talk to
    public string GetGreeting(int characterID) {
        Character tempChar = GameObject.FindWithTag("CharacterManager").GetComponent<CharactersManager>().GetCharacter(characterID);

        if(tempChar.GetGreeting() != "") {
            return tempChar.GetGreeting();
        }

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
    public void DisplayDialogueChoice(string message) {
        playerResponseObject.GetComponentInChildren<Text>().text = message;
        var response = GameObject.Instantiate(playerResponseObject);
        response.transform.SetParent(conversationScroll.transform, false);

        //add room if necessary
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
        else if(messageID == 13) {//asking what they want

        }
        else if (messageID == 20) {//wanting to begin training
            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "Let's get to it.";
            var charResponse = Instantiate(characterResponseObject);
            charResponse.transform.SetParent(placeholder.transform, false);

            StartCoroutine(GameObject.FindWithTag("DecisionManager").GetComponent<DecisionManager>().DisplayTrainingWindow(characterID));

        }
        else if(messageID == 22) {//accepting arrest
            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "This way...";
            var charResponse = Instantiate(characterResponseObject);
            charResponse.transform.SetParent(placeholder.transform, false);

            StartCoroutine(ArrestPlayer());
        }
        else if(messageID == 23) {//resist arrest
            GameObject.Find("ConversationBackgroundPanel").SetActive(false);
            int[] IDs = { 1005, 1006 };
            GameObject.Find("DecisionManager").GetComponent<DecisionManager>().AttackPeople(IDs);

            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "Then draw your sword.";
            var charResponse = Instantiate(characterResponseObject);
            charResponse.transform.SetParent(placeholder.transform, false);
        }
        else if(messageID == 24) {
            GameObject.Find("CharacterManager").GetComponent<CharactersManager>().GetCharacter(characterID).AddRelationship(10);

            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "Ok...";
            var charResponse = Instantiate(characterResponseObject);
            charResponse.transform.SetParent(placeholder.transform, false);

            for (int i = 0; i < dialogueOptionsPanel.transform.childCount; i++) {
                Destroy(dialogueOptionsPanel.transform.GetChild(i).gameObject);
            }
        }
        else if(messageID == 25) {
            GameObject.Find("CharacterManager").GetComponent<CharactersManager>().GetCharacter(characterID).SubtractRelationship(10);

            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "Like you're not scared!";
            var charResponse = Instantiate(characterResponseObject);
            charResponse.transform.SetParent(placeholder.transform, false);

            for (int i = 0; i < dialogueOptionsPanel.transform.childCount; i++) {
                Destroy(dialogueOptionsPanel.transform.GetChild(i).gameObject);
            }
        }

        //pause added so that the gameobject has time to be placed in the placeholder to later be transformed into the conversation panel because otherwise it wont account for the dynamic size
        yield return new WaitForSeconds(0.001f);
        placeholder.transform.GetChild(0).transform.SetParent(conversationScroll.transform, false);

        ExpandConversationScroll();
    }

    public IEnumerator ArrestPlayer() {
        yield return new WaitForSeconds(3f);
        conversationBackgroundPanel.SetActive(false);

        string leaveText = "The guards arrest you and take you to your prison cell.";
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().NextRoom(7, leaveText);
        StartCoroutine(GameObject.Find("DecisionManager").GetComponent<DecisionManager>().StartPrison());
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

        dialogueDecisionObject.GetComponent<DialogueAction>().convoID = 0;
        dialogueDecisionObject.GetComponent<DialogueAction>().characterID = characterID;

        if (optionID == 13) {
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Question them";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "What do you want?";
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 13;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
        else if (optionID == 20) {//if dialogue is to begin training
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Begin training";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "I'm ready to start training";
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 20;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
        else if (optionID == 21) {
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Leave";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "I'm leaving";
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 21;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
        else if(optionID == 22) {
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Come quietly";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "Alright, I'll cooporate";
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 22;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
        else if(optionID == 23) {
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Refuse";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "I'm not going with you";
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 23;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
        else if(optionID == 24) {//comfort character
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Comfort";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "It's going to be alright";
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 24;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }else if(optionID == 25) {
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Tell off";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "Oh shut up";
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 25;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
        else{
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = responses[optionID].labelText;
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = responses[optionID].dialogueText;
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = responses[optionID].ID;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }

    }

    public string StartConversation(int convoID, int characterID) {

        if(convoID == 1) {
            AddConvoResponses(convoID, 0, characterID);
            return prisonCharResponses[0].response;
        }
        else {
            AddConvoResponses(convoID, 0, characterID);
            return prisonCharResponses[0].response;
        }
    }



    //adds options for what the player can say next based on what the character just said
    public void AddConvoResponses(int convoID, int characterResponseID, int characterID) {

        for(int i = 0; i < dialogueOptionsPanel.transform.childCount; i++) {
            Destroy(dialogueOptionsPanel.transform.GetChild(i).gameObject);
        }

        //which convo
        if (convoID == 1) {
            if(prisonCharResponses[characterResponseID].playerOptions != null) {
                for (int i = 0; i < prisonCharResponses[characterResponseID].playerOptions.Length; i++) {
                    int responseID = prisonCharResponses[characterResponseID].playerOptions[i];
                    dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = prisonPlayerResponses[responseID].response;
                    dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = prisonPlayerResponses[responseID].response;
                    dialogueDecisionObject.GetComponent<DialogueAction>().characterID = characterID;
                    dialogueDecisionObject.GetComponent<DialogueAction>().messageID = prisonPlayerResponses[responseID].characterResponseID;
                    dialogueDecisionObject.GetComponent<DialogueAction>().convoID = convoID;
                    var dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
                    dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
                }
            }
        }
    }

    public void GetCharacterResponse(int convoID, int responseID, int characterID) {
        if(convoID == 1) {
            for(int i = 0; i < prisonCharResponses.Length; i++) {
                if(prisonCharResponses[i].responseID == responseID) {
                    StartCoroutine(DisplayCharacterResponse(prisonCharResponses[i].response));
                    AddConvoResponses(convoID, responseID, characterID);
                }
            }
        }
    }

    public IEnumerator DisplayCharacterResponse(string response) {

        characterResponseObject.GetComponentsInChildren<Text>()[0].text = response;
        var charResponse = Instantiate(characterResponseObject);
        charResponse.transform.SetParent(GameObject.Find("Placeholder").transform, false);

        yield return new WaitForSeconds(0.001f);

        GameObject.Find("Placeholder").transform.GetChild(0).transform.SetParent(conversationScroll.transform, false);
    }

    public void ExpandConversationScroll() {

        int numItems = conversationScroll.transform.childCount;
        float totalHeight = 0;
        for (int i = 0; i < numItems; i++) {
            totalHeight += conversationScroll.transform.GetChild(i).GetComponent<RectTransform>().rect.height;
        }

        float scrollHeight = conversationScroll.GetComponent<RectTransform>().rect.height;

        if(oldHeight != 0) {
            scrollHeight = oldHeight;
        }

        if (totalHeight > scrollHeight) {
            //if (scrollHeight > newHeight) {
            //    newHeight = scrollHeight;
            //}
            Debug.Log("Value to be added = " + ((totalHeight - scrollHeight) + 40));
            Debug.Log("totalHeight " + totalHeight);
            Debug.Log("Before" + scrollHeight);
            //conversationScroll.GetComponent<RectTransform>().offsetMax = new Vector2(-15, (totalHeight - scrollHeight) + 40);
            //Debug.Log("After " + conversationScroll.GetComponent<RectTransform>().rect.height);
            float offset = (totalHeight - scrollHeight) + 40;
            oldHeight = conversationScroll.GetComponent<RectTransform>().rect.height + offset;
            StartCoroutine(ExpandAgain(offset));
        }
        else {//else reset
            conversationScroll.GetComponent<RectTransform>().offsetMax = new Vector2(-15, 0);
        }

    }

    public IEnumerator ExpandAgain(float offset) {
        yield return new WaitForEndOfFrame();
        conversationScroll.GetComponent<RectTransform>().offsetMax = new Vector2(-15, offset);
        Debug.Log("After " + conversationScroll.GetComponent<RectTransform>().rect.height);

    }
}
