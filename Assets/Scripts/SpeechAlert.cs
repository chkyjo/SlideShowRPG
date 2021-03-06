﻿using UnityEngine;

public class SpeechAlert : MonoBehaviour {

    public string text;
    public int characterID;
    public int convoID;

    public void EndAndDestroy() {
        gameObject.transform.parent.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void OpenConversation() {
        if(convoID != -1) {
            GameObject.Find("DecisionManager").GetComponent<DecisionManager>().TalkTo(characterID, "");
            GameObject.Find("ConversationManager").GetComponent<ConversationManager>().StartConversation(convoID, characterID);
        }

        gameObject.transform.parent.gameObject.SetActive(false);
        Destroy(gameObject);
    }



}
