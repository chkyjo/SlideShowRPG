using UnityEngine;

public class DialogueAction : MonoBehaviour {

    public string dialogueMessage;
    public int messageID;
    public int characterID;
    public int convoID;
    public int effect;
    public int parameter;

    public void MakeDialogue(){
        //display the players choice
        GameObject.Find("ConversationManager").GetComponent<ConversationManager>().DisplayDialogueChoice(dialogueMessage);
        //if there is an effect to this response
        if (effect != 0) {
            GameObject.Find("ConversationManager").GetComponent<ConversationManager>().CauseEffect(effect, characterID, parameter);
        }
        
        if(!(convoID == -1)) {
            //display character response to choice and add new dialogue options
            GameObject.Find("ConversationManager").GetComponent<ConversationManager>().GetCharacterResponse(convoID, messageID, characterID);
        }
        Destroy(gameObject);
    }
}
