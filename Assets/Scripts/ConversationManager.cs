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
        characterResponses[0].text = "What would you like to know?";
        characterResponses[0].numDialogueOptions = 3;
        characterResponses[0].dialogueOptions = new int[3] { 1, 2, 5 };

        characterResponses[1].text = "There will be a feast to celebrate the election of the new king.";
        characterResponses[1].numDialogueOptions = 3;
        characterResponses[1].dialogueOptions = new int[3] { 0, 4, 5 };

        characterResponses[2].text = "They are the worlds dealiest fighters, and the true saviors of this land.";
        characterResponses[2].numDialogueOptions = 3;
        characterResponses[2].dialogueOptions = new int[3] { 0, 3, 5 };

        characterResponses[3].text = "When the lords deem you ready.";
        characterResponses[3].numDialogueOptions = 2;
        characterResponses[3].dialogueOptions = new int[2] { 0, 5 };

        characterResponses[4].text = "She is no king.";
        characterResponses[4].numDialogueOptions = 2;
        characterResponses[4].dialogueOptions = new int[2] { 0, 5 };

        //characterResponses[5].text = "Well, I am ...";
        //characterResponses[5].numDialogueOptions = 1;
        //characterResponses[5].dialogueOptions = new int[1] { 0 };

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

        if (characterID == 1000) {//if character is gregory highlark, add training option
            AddDialogueOption(20, characterID);
        }
        AddDialogueOption(0, characterID);
        AddDialogueOption(5, characterID);

    }

    public IEnumerator DisplayGreeting(int characterID, string greeting) {

        characterResponseObject.GetComponentsInChildren<Text>()[0].text = greeting;
        var charResponse = Instantiate(characterResponseObject);
        charResponse.transform.SetParent(conversationScroll.transform, false);

        yield return new WaitForSeconds(0.00001f);
        GameObject placeholder = GameObject.Find("Placeholder");
        conversationScroll.transform.GetChild(conversationScroll.transform.childCount - 1).transform.SetParent(placeholder.transform, false);
        placeholder.transform.GetChild(0).transform.SetParent(conversationScroll.transform, false);

        if (characterID == 1000) {//if character is gregory highlark, add training option
            AddDialogueOption(20, characterID);
        }
        

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

    public void MakeAComment(int commentID) {

        if (commentID == 10000) {//player commented on the characters skills
            
          
            
        }

    }

    //type of dialogue option that reveals a trait about the character
    public GameObject AskAboutCharacter(int questionID, int characterID) {
        GameObject characterManager = GameObject.FindWithTag("CharacterManager");
        Character tempChar = GameObject.FindWithTag("CharacterManager").GetComponent<CharactersManager>().GetCharacter(characterID);
        int randNum = UnityEngine.Random.Range(0, 5);
        characterResponseObject.GetComponentsInChildren<Text>()[0].text = "Well I " + characterManager.GetComponent<CharactersManager>().GetTrait(tempChar.GetTraits()[randNum]) + ".";
        tempChar.SetPlayerKnowledge(randNum);
        characterManager.GetComponent<CharactersManager>().UpdateInfoPanel(characterID);

        AddDialogueOption(0, characterID);
        AddDialogueOption(5, characterID);
        AddDialogueOption(6, characterID);

        return characterResponseObject;

    }

    //sole perpose is to call DisplayResponse
    public void CallDisplayResponse(int questionID, int characterID) {
        StartCoroutine(DisplayResponse(questionID, characterID));
    }

    //called by calldisplayresponse because IEnumerator
    public IEnumerator DisplayResponse(int questionID, int characterID) {

        GameObject placeholder = GameObject.Find("Placeholder");

        //clear dialogue options
        for (int i = 0; i < dialogueOptionsPanel.transform.childCount; i++) {
            Destroy(dialogueOptionsPanel.transform.GetChild(i).gameObject);
        }

        if(questionID == 10) {

            var charResponse = Instantiate(AskAboutCharacter(questionID, characterID));
            charResponse.transform.SetParent(placeholder.transform, false);
        }
        else if (questionID == 11){
            string weather = GameObject.FindWithTag("SettingManager").GetComponent<SettingManager>().GetWeather();
            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "It looks a bit " + weather + " out there. You might want to wear a coat.";
            var charResponse = Instantiate(characterResponseObject);
            charResponse.transform.SetParent(placeholder.transform, false);

            AddDialogueOption(0, characterID);
            AddDialogueOption(5, characterID);
            AddDialogueOption(6, characterID);
        }
        else if (questionID == 20) {
            characterResponseObject.GetComponentsInChildren<Text>()[0].text = "Let's get to it.";
            var charResponse = Instantiate(characterResponseObject);
            charResponse.transform.SetParent(placeholder.transform, false);

            StartCoroutine(GameObject.FindWithTag("DecisionManager").GetComponent<DecisionManager>().DisplayTrainingWindow());

        }
        else {
            //display response
            characterResponseObject.GetComponentsInChildren<Text>()[0].text = characterResponses[questionID].text;
            var charResponse = Instantiate(characterResponseObject);
            charResponse.transform.SetParent(placeholder.transform, false);

            //add dialogue options
            for (int i = 0; i < characterResponses[questionID].numDialogueOptions; i++) {
                AddDialogueOption(characterResponses[questionID].dialogueOptions[i], characterID);
            }
        }

        //pause added so that the gameobject has time to be placed in the scroll to later be transformed out and then back in again because otherwise it wont account for the dynamic size
        yield return new WaitForSeconds(0.001f);
        placeholder.transform.GetChild(0).transform.SetParent(conversationScroll.transform, false);
        //Debug.Log("EXPANDING SCROL!");
        ExpandConversationScroll();
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
            else {
                return "Well it's about time " + GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>().GetName() + "! C'mon you lazy idiot, take out your sword.";
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

    //add dialogue option based on options ID, pass in character to pass on to the response when the dialogue is used
    public void AddDialogueOption(int optionID, int characterID) {

        GameObject dialogueObj;

        if(optionID == 21) {
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Leave";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "I'm leaving";
            dialogueDecisionObject.GetComponent<DialogueAction>().characterID = characterID;
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 21;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
        else if (optionID != 20) {
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = playerResponses[optionID].labelText;
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = playerResponses[optionID].dialogueText;
            dialogueDecisionObject.GetComponent<DialogueAction>().characterID = characterID;
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = playerResponses[optionID].ID;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }
        else {//if dialogue is to begin training
            dialogueDecisionObject.GetComponentsInChildren<Text>()[1].text = "Begin training";
            dialogueDecisionObject.GetComponent<DialogueAction>().dialogueMessage = "I'm ready to start training";
            dialogueDecisionObject.GetComponent<DialogueAction>().characterID = characterID;
            dialogueDecisionObject.GetComponent<DialogueAction>().messageID = 20;
            dialogueObj = GameObject.Instantiate(dialogueDecisionObject);
            dialogueObj.transform.SetParent(dialogueOptionsPanel.transform, false);
        }

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
