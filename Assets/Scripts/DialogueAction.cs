using UnityEngine;

public class DialogueAction : MonoBehaviour {

    public string dialogueMessage;
    public int messageID;
    public int characterID;

    public void MakeDialogue(){
        GameObject.Find("ConversationManager").GetComponent<ConversationManager>().DisplayDialogueChoice(dialogueMessage, messageID);
        if (messageID < 10000) {
            GameObject.Find("ConversationManager").GetComponent<ConversationManager>().CallDisplayResponse(messageID, characterID);
        }
    }
}
