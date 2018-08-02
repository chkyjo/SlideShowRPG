﻿using System;
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
    public Scrollbar converstationScrollBar;
    public GameObject characterInfoPanel;

    struct ConvoNode {
        public int nodeID;
        public string playerResponse;
        public string responseSummary;
        public string characterResponse;
        public int[] playerOptions;
        public int effect;
        public int parameter1;
    }

    ConvoNode[][] conversations = new ConvoNode[10][];

    float savedHeight;

	// Use this for initialization
	void Start () {
        InitializeConversations();
    }

    void InitializeConversations() {

        string[] textFileNames = new string[10] {"DefaultConvo.txt", "PrisonConvo.txt", "AkayshaConvo.txt", "ArrestConvo.txt", "BenConvo.txt", "TakeTheShotConvo.txt", "", "", "", "" };

        conversations[0] = new ConvoNode[100];
        conversations[1] = new ConvoNode[100];
        conversations[2] = new ConvoNode[100];
        conversations[3] = new ConvoNode[10];
        conversations[4] = new ConvoNode[10];
        conversations[5] = new ConvoNode[10];

        int nodeIndex = 0;

        int numChildren = 0;
        int[] childIndexes = new int[5];
        string nextLine;
        StreamReader file;

        for (int i = 0; i < 6; i++) {
            file = new StreamReader(Application.persistentDataPath + "/" + textFileNames[i], true);

            //get root data
            conversations[i][0].nodeID = 0;
            conversations[i][0].characterResponse = file.ReadLine();

            while (!file.EndOfStream) {
                nextLine = file.ReadLine();
                if (nextLine.StartsWith(":")) {//if child index add to child indexes
                    childIndexes[numChildren] = Convert.ToInt16(nextLine.TrimStart(':'));
                    numChildren++;
                }
                else if (numChildren != 0) {//if line is not a child index and numchildren is not 0
                                            //add children to character response
                    conversations[i][nodeIndex].playerOptions = new int[numChildren];
                    for (int j = 0; j < numChildren; j++) {
                        conversations[i][nodeIndex].playerOptions[j] = childIndexes[j];
                    }
                    if (nodeIndex == 0) {//if root also up the index
                        nodeIndex++;
                    }
                    numChildren = 0;
                }

                if (nextLine.StartsWith("/")) {//if summary
                    conversations[i][nodeIndex].responseSummary = nextLine.TrimStart('/');
                }

                if (nextLine.StartsWith(")")) {//if player response add the string
                    conversations[i][nodeIndex].playerResponse = nextLine.TrimStart(')');
                }

                if (nextLine.StartsWith("(")) {//if character response add the string
                    conversations[i][nodeIndex].characterResponse = nextLine.TrimStart('(');
                }

                if (nextLine.StartsWith(">")) {//if effect
                    string[] splitEffect = nextLine.TrimStart('>').Split('-');
                    if (splitEffect.Length == 2) {
                        conversations[i][nodeIndex].effect = Convert.ToInt16(splitEffect[0]);
                        conversations[i][nodeIndex].parameter1 = Convert.ToInt16(splitEffect[1]);
                    }
                    else {
                        conversations[i][nodeIndex].effect = Convert.ToInt16(splitEffect[0]);
                    }

                }

                if (nextLine.StartsWith("-")) {//if ID
                    conversations[i][nodeIndex].nodeID = Convert.ToInt16(nextLine.TrimStart('-'));
                    nodeIndex++;
                }
            }

            file.Close();
            nodeIndex = 0;
        }
    }

    public IEnumerator DisplayGreeting(int characterID) {

        characterResponseObject.GetComponentsInChildren<Text>()[0].text = GetGreeting(characterID);
        var charResponse = Instantiate(characterResponseObject);
        charResponse.transform.SetParent(GameObject.Find("Placeholder").transform, false);

        yield return new WaitForSeconds(0.00001f);
        charResponse.transform.SetParent(conversationScroll.transform, false);
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
        var response = Instantiate(playerResponseObject);
        response.transform.SetParent(conversationScroll.transform, false);
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

    public void CauseEffect(int effect, int characterID, int parameter1) {
        if (effect == 1) {//resetConvo
            ResetConvo(characterID);
        }
        else if(effect == 2) {//ask about weather
            StartCoroutine(DisplayCharacterResponse(GetWeatherResponse()));
        }
		else if(effect == 3){//addrel
            Character tempChar = GameObject.Find("CharacterManager").GetComponent<CharactersManager>().GetCharacter(characterID);
            tempChar.AddRelationship(parameter1);
            characterInfoPanel.transform.GetChild(17).GetComponent<Slider>().value = tempChar.GetRelationship();
        }
		else if(effect == 4){//subrel
            Character tempChar = GameObject.Find("CharacterManager").GetComponent<CharactersManager>().GetCharacter(characterID);
            tempChar.SubtractRelationship(parameter1);
            characterInfoPanel.transform.GetChild(17).GetComponent<Slider>().value = tempChar.GetRelationship();
        }
        else if(effect == 5) {//make hostile

        }
        else if (effect == 6) {//startMission
            GameObject.Find("MissionManager").GetComponent<Missions>().StartMission(1);
        }
        else if (effect == 7) {//addQuest

        }
        else if (effect == 8) {//arrestPlayer
            StartCoroutine(DisplayCharacterResponse("This way."));
            StartCoroutine(ArrestPlayer());
        }
        else if (effect == 9) {//ask about character
            GetToKnowCharacter(characterID);
        }
        else if(effect == 10) {
            StartCoroutine(GameObject.Find("DecisionManager").GetComponent<DecisionManager>().DisplayTrainingWindow());
        }
        else if(effect == 11) {
            GameObject.Find("MissionManager").GetComponent<Missions>().fireAtDeer = 0;
        }
    }

    public void GetToKnowCharacter(int characterID) {
        int randNum = UnityEngine.Random.Range(0, 5);
        CharactersManager cM = GameObject.Find("CharacterManager").GetComponent<CharactersManager>();
        Character tempChar = cM.GetCharacter(characterID);
        tempChar.SetPlayerKnowledge(randNum);
        characterInfoPanel.GetComponentsInChildren<Text>()[randNum + 1].text = cM.GetTrait(tempChar.GetTraits()[randNum]);

    }

    public void ResetConvo(int characterID) {
        for (int i = 0; i < dialogueOptionsPanel.transform.childCount; i++) {
            Destroy(dialogueOptionsPanel.transform.GetChild(i).gameObject);
        }
        Character tempChar = GameObject.Find("CharacterManager").GetComponent<CharactersManager>().GetCharacter(characterID);
        for(int i = 0; i < 3; i++) {
            if(tempChar.GetConvoList()[i] != -1) {
                AddConversation(tempChar.GetConvoList()[i], characterID);
            } 
            
        }
    }

    public IEnumerator ArrestPlayer() {
        yield return new WaitForSeconds(3f);
        ClearConversation();
        conversationBackgroundPanel.SetActive(false);

        string leaveText = "The guards arrest you and take you to your prison cell.";
        GameObject.Find("DecisionManager").GetComponent<DecisionManager>().NextRoom(7, leaveText);
        StartCoroutine(GameObject.Find("DecisionManager").GetComponent<DecisionManager>().StartPrison());
    }

    //display response to question about weather
    public string GetWeatherResponse() {
        string weather = GameObject.FindWithTag("SettingManager").GetComponent<SettingManager>().GetWeather();
        string response;
        if(weather == "rainy") {
            response = "It looks a bit " + weather + " out there. You might want to wear a coat.";
        }
        else if (weather == "windy") {
            response = "It's blowing me away how " + weather + " it is.";
        }
        else if (weather == "snowy") {
            response = "It's cold out there. Haven't seen this much snow in a while.";
        }
        else if (weather == "sunny") {
            response = "It's beautiful outside.";
        }
        else {
            response = "It's strangly misty. Gives me the creeps.";
        }

        return response;
    }

    public void AddConversation(int convoID, int characterID) {
        
        dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = conversations[convoID][1].responseSummary;
        dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = conversations[convoID][1].playerResponse;
        dialogueDecisionObject.GetComponent<DialogueAction>().characterID = characterID;
        dialogueDecisionObject.GetComponent<DialogueAction>().messageID = conversations[convoID][1].nodeID;
        dialogueDecisionObject.GetComponent<DialogueAction>().convoID = convoID;
        dialogueDecisionObject.GetComponent<DialogueAction>().effect = conversations[convoID][1].effect;
        dialogueDecisionObject.GetComponent<DialogueAction>().parameter = conversations[convoID][1].parameter1;
        var dialogueObj = Instantiate(dialogueDecisionObject);
        dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        
    }

    //adds initial player response options and returns the string of the characters greeting
    public void StartConversation(int convoID, int characterID) {
        StartCoroutine(DisplayCharacterResponse(conversations[convoID][0].characterResponse));
        for (int i = 0; i < conversations[convoID][0].playerOptions.Length; i++) {
            int responseID = conversations[convoID][0].playerOptions[i];
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = conversations[convoID][responseID].responseSummary;
            DialogueAction dA = dialogueDecisionObject.GetComponent<DialogueAction>();
            dA.dialogueMessage = conversations[convoID][responseID].playerResponse;
            dA.characterID = characterID;
            dA.messageID = conversations[convoID][responseID].nodeID;
            dA.convoID = convoID;
            dA.effect = conversations[convoID][responseID].effect;
            dA.parameter = conversations[convoID][responseID].parameter1;
            var dialogueObj = Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
    }

    public void GetCharacterResponse(int convoID, int nodeID, int characterID) {

        for (int i = 0; i < dialogueOptionsPanel.transform.childCount; i++) {
            Destroy(dialogueOptionsPanel.transform.GetChild(i).gameObject);
        }
        
        //display the response
        StartCoroutine(DisplayCharacterResponse(conversations[convoID][nodeID].characterResponse));
        //if there are player options to respond to the response
        if (conversations[convoID][nodeID].playerOptions != null) {
            //Get each option and display it
            for (int j = 0; j < conversations[convoID][nodeID].playerOptions.Length; j++) {
                int playerResponseID = conversations[convoID][nodeID].playerOptions[j];
                dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = conversations[convoID][playerResponseID].responseSummary;
                dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = conversations[convoID][playerResponseID].playerResponse;
                dialogueDecisionObject.GetComponent<DialogueAction>().characterID = characterID;
                dialogueDecisionObject.GetComponent<DialogueAction>().messageID = conversations[convoID][playerResponseID].nodeID;
                dialogueDecisionObject.GetComponent<DialogueAction>().convoID = convoID;
                dialogueDecisionObject.GetComponent<DialogueAction>().effect = conversations[convoID][playerResponseID].effect;
                dialogueDecisionObject.GetComponent<DialogueAction>().parameter = conversations[convoID][playerResponseID].parameter1;
                var dialogueObj = Instantiate(dialogueDecisionObject);
                dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
            }
        }
    }

    public IEnumerator DisplayCharacterResponse(string response) {

        characterResponseObject.GetComponentsInChildren<Text>()[0].text = response;
        var charResponse = Instantiate(characterResponseObject);
        charResponse.transform.SetParent(GameObject.Find("Placeholder").transform, false);

        yield return new WaitForSeconds(0.001f);

        GameObject.Find("Placeholder").transform.GetChild(0).transform.SetParent(conversationScroll.transform, false);

        ExpandConversationScroll();
    }

    public void ClearConversation() {
        for(int i = 0; i < conversationScroll.transform.childCount; i++) {
            Destroy(conversationScroll.transform.GetChild(i).gameObject);
        }

        //reset scrollbar and scroll area
        converstationScrollBar.value = 0;
        conversationScroll.GetComponent<RectTransform>().offsetMax = new Vector2(-15, 0);

        for (int i = 0; i < dialogueOptionsPanel.transform.childCount; i++) {
            Destroy(dialogueOptionsPanel.transform.GetChild(i).gameObject);
        }

    }

    public void ExpandConversationScroll() {

        //number of items in the scroll
        int numItems = conversationScroll.transform.childCount;
        float totalHeight = 0;
        //add up the heights of all the items
        for (int i = 0; i < numItems; i++) {
            totalHeight += conversationScroll.transform.GetChild(i).GetComponent<RectTransform>().rect.height;
        }

        if (totalHeight > 192) {
            float newOffset = (totalHeight - 192) + 10;

            conversationScroll.GetComponent<RectTransform>().offsetMax = new Vector2(-15, newOffset);
        }
        else {//else reset
            savedHeight = 192;
            conversationScroll.GetComponent<RectTransform>().offsetMax = new Vector2(-15, 0);
        }

    }
}
