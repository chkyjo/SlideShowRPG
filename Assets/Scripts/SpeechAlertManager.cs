using UnityEngine;

public class SpeechAlertManager : MonoBehaviour {

    public string text;
    public int characterID;
    public int convoID;

    public void EndAndDestroy() {
        Destroy(gameObject);
    }

    public void OpenConversation() {
        if(convoID != -1) {
            GameObject.Find("DecisionManager").GetComponent<DecisionManager>().TalkTo(characterID, "");
            GameObject.Find("ConversationManager").GetComponent<ConversationManager>().StartConversation(convoID, characterID);
        }
        
        Destroy(gameObject);
    }



}
