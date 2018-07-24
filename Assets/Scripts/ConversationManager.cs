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

    struct ConvoNode {
        public int nodeID;
        public string playerResponse;
        public string responseSummary;
        public string characterResponse;
        public int[] playerOptions;
        public int effect;
        public int parameter1;
    }

    ConvoNode[] prisonConvo = new ConvoNode[100];
    ConvoNode[] huntingConvo = new ConvoNode[100];

    float savedHeight;

	// Use this for initialization
	void Start () {
        InitializeConversations();
    }

    void InitializeConversations() {
        StreamReader file = new StreamReader(Application.persistentDataPath + "/PrisonConvo.txt", true);

        int nodeIndex = 0;

        int numChildren = 0;
        int[] childIndexes = new int[5];
        string nextLine;

        //get root data
        string rootString = file.ReadLine();
        prisonConvo[0].nodeID = 0;
        prisonConvo[0].characterResponse = rootString.TrimStart('(');

        while (!file.EndOfStream) {
            nextLine = file.ReadLine();
            if (nextLine.StartsWith(":")) {//if child index add to child indexes
                childIndexes[numChildren] = Convert.ToInt16(nextLine.TrimStart(':'));
                numChildren++;
            }
            else if (numChildren != 0) {//if line is not a child index and numchildren is not 0
                //add children to character response
                prisonConvo[nodeIndex].playerOptions = new int[numChildren];
                for (int i = 0; i < numChildren; i++) {
                    prisonConvo[nodeIndex].playerOptions[i] = childIndexes[i];
                }
                if (nodeIndex == 0) {//if root also up the index
                    nodeIndex++;
                }
                numChildren = 0;
            }

            if (nextLine.StartsWith("/")) {//if summary
                prisonConvo[nodeIndex].responseSummary = nextLine.TrimStart('/');
            }

            if (nextLine.StartsWith(")")) {//if player response add the string
                prisonConvo[nodeIndex].playerResponse = nextLine.TrimStart(')');
            }

            if (nextLine.StartsWith("(")) {//if character response add the string
                prisonConvo[nodeIndex].characterResponse = nextLine.TrimStart('(');
            }

            if (nextLine.StartsWith(">")) {//if effect
                string[] splitEffect = nextLine.TrimStart('>').Split('-');
                if(splitEffect.Length == 2) {
                    prisonConvo[nodeIndex].effect = Convert.ToInt16(splitEffect[0]);
                    prisonConvo[nodeIndex].parameter1 = Convert.ToInt16(splitEffect[1]);
                }
                else {
                    prisonConvo[nodeIndex].effect = Convert.ToInt16(splitEffect[0]);
                }
                
            }

            if (nextLine.StartsWith("-")) {//if ID
                prisonConvo[nodeIndex].nodeID = Convert.ToInt16(nextLine);
                nodeIndex++;
            }
        }

        file.Close();

        file = new StreamReader(Application.persistentDataPath + "/AkayshaConvo.txt", true);

        nodeIndex = 0;
        huntingConvo[0].characterResponse = file.ReadLine().TrimStart('(');
        huntingConvo[0].nodeID = 0;

        while (!file.EndOfStream) {
            nextLine = file.ReadLine();
            if (nextLine.StartsWith(":")) {//if child index add to child indexes
                childIndexes[numChildren] = Convert.ToInt16(nextLine.TrimStart(':'));
                numChildren++;
            }
            else if (numChildren != 0) {//if line is not a child index and numchildren is not 0
                //add children to character response
                huntingConvo[nodeIndex].playerOptions = new int[numChildren];
                for (int i = 0; i < numChildren; i++) {
                    huntingConvo[nodeIndex].playerOptions[i] = childIndexes[i];
                }
                if (nodeIndex == 0) {//if root also up the index
                    nodeIndex++;
                }
                numChildren = 0;
            }

            if (nextLine.StartsWith("/")) {//if summary
                huntingConvo[nodeIndex].responseSummary = nextLine.TrimStart('/');
            }

            if (nextLine.StartsWith(")")) {//if player response add the string
                huntingConvo[nodeIndex].playerResponse = nextLine.TrimStart(')');
            }

            if (nextLine.StartsWith("(")) {//if character response add the string
                huntingConvo[nodeIndex].characterResponse = nextLine.TrimStart('(');
            }

            if (nextLine.StartsWith(">")) {//if effect
                string[] splitEffect = nextLine.TrimStart('>').Split('-');
                if (splitEffect.Length == 2) {
                    huntingConvo[nodeIndex].effect = Convert.ToInt16(splitEffect[0]);
                    huntingConvo[nodeIndex].parameter1 = Convert.ToInt16(splitEffect[1]);
                }
                else {
                    huntingConvo[nodeIndex].effect = Convert.ToInt16(splitEffect[0]);
                }

            }

            if (nextLine.StartsWith("-")) {//if IDs add ID and increase index
                huntingConvo[nodeIndex].nodeID = Convert.ToInt16(nextLine);
                nodeIndex++;
            }
        }

        file.Close();
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
        var response = GameObject.Instantiate(playerResponseObject);
        response.transform.SetParent(conversationScroll.transform, false);
    }

    //sole perpose is to call DisplayResponse
    public void CallDisplayResponse(int questionID, int characterID) {
        StartCoroutine(DisplayResponseToMessage(questionID, characterID));
    }

    //called by calldisplayresponse because IEnumerator can't be called by an object that will be destroyed
    public IEnumerator DisplayResponseToMessage(int messageID, int characterID) {

        GameObject placeholder = GameObject.Find("Placeholder");

        

        //pause added so that the gameobject has time to be placed in the placeholder to later be transformed into the conversation panel because otherwise it wont account for the dynamic size
        yield return new WaitForSeconds(0.001f);
        placeholder.transform.GetChild(0).transform.SetParent(conversationScroll.transform, false);

        yield return new WaitForEndOfFrame();

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

    //adds initial player response options and returns the string of the characters greeting
    public void StartConversation(int convoID, int characterID) {

        if(convoID == 1) {

            characterResponseObject.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = prisonConvo[0].characterResponse;
            var obj = Instantiate(characterResponseObject);
            obj.transform.SetParent(conversationScroll.transform, false);

            for (int i = 0; i < prisonConvo[0].playerOptions.Length; i++) {
                int responseID = prisonConvo[0].playerOptions[i];
                dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = prisonConvo[responseID].responseSummary;
                dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = prisonConvo[responseID].playerResponse;
                dialogueDecisionObject.GetComponent<DialogueAction>().characterID = characterID;
                dialogueDecisionObject.GetComponent<DialogueAction>().messageID = prisonConvo[responseID].nodeID;
                dialogueDecisionObject.GetComponent<DialogueAction>().convoID = convoID;
                var dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
                dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
            }
            
        }else if (convoID == 2) {
            for (int i = 0; i < huntingConvo[0].playerOptions.Length; i++) {
                int responseID = huntingConvo[0].playerOptions[i];
                dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = huntingConvo[responseID].responseSummary;
                dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = huntingConvo[responseID].playerResponse;
                dialogueDecisionObject.GetComponent<DialogueAction>().characterID = characterID;
                dialogueDecisionObject.GetComponent<DialogueAction>().messageID = huntingConvo[responseID].nodeID;
                dialogueDecisionObject.GetComponent<DialogueAction>().convoID = convoID;
                var dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
                dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
            }
        }

        GameObject.Find("CharacterManager").GetComponent<CharactersManager>().GetCharacter(characterID).SetConvoQueue(0);
    }

    public void GetCharacterResponse(int convoID, int nodeID, int characterID) {

        for(int i = 0; i < dialogueOptionsPanel.transform.childCount; i++) {
            Destroy(dialogueOptionsPanel.transform.GetChild(i).gameObject);
        }

        //depending on the conversation
        if(convoID == 1) {
            //for each character response
            for(int i = 0; i < prisonConvo.Length; i++) {
                //if the character response ID matches the requested ID
                if(prisonConvo[i].nodeID == nodeID) {
                    //display the response
                    StartCoroutine(DisplayCharacterResponse(prisonConvo[i].characterResponse));
                    //if there are player options to respond to the response
                    if (prisonConvo[i].playerOptions != null) {
                        //Get each option and display it
                        for (int j = 0; j < prisonConvo[i].playerOptions.Length; j++) {
                            int playerResponseID = prisonConvo[i].playerOptions[j];
                            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = prisonConvo[nodeID].responseSummary;
                            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = prisonConvo[nodeID].playerResponse;
                            dialogueDecisionObject.GetComponent<DialogueAction>().characterID = characterID;
                            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = nodeID;
                            dialogueDecisionObject.GetComponent<DialogueAction>().convoID = convoID;
                            var dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
                            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
                        }
                    }
                }
            }
        }else if(convoID == 2) {
            for (int i = 0; i < huntingConvo.Length; i++) {
                if (huntingConvo[i].nodeID == nodeID) {
                    StartCoroutine(DisplayCharacterResponse(huntingConvo[i].characterResponse));
                    if (huntingConvo[i].playerOptions != null) {
                        for (int j = 0; j < huntingConvo[i].playerOptions.Length; j++) {
                            int playerResponseID = huntingConvo[i].playerOptions[j];
                            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = huntingConvo[playerResponseID].responseSummary;
                            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = huntingConvo[playerResponseID].playerResponse;
                            dialogueDecisionObject.GetComponent<DialogueAction>().characterID = characterID;
                            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = nodeID;
                            dialogueDecisionObject.GetComponent<DialogueAction>().convoID = convoID;
                            var dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
                            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
                        }
                    }
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

        ExpandConversationScroll();
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
