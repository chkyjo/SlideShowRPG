using UnityEngine;

public class DialogueAction : MonoBehaviour {

    public string dialogueMessage;
    public int messageID;
    public int characterID;
    public int convoID;

    public void MakeDialogue(){
        GameObject.Find("ConversationManager").GetComponent<ConversationManager>().DisplayDialogueChoice(dialogueMessage);
        if (convoID != 0) {
            GameObject.Find("ConversationManager").GetComponent<ConversationManager>().GetCharacterResponse(convoID, messageID, characterID);
        }
        else if (messageID != -1) {
            GameObject.Find("ConversationManager").GetComponent<ConversationManager>().CallDisplayResponse(messageID, characterID);
        }
        Destroy(this.gameObject);
    }
}
