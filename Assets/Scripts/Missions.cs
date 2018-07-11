using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Missions : MonoBehaviour {

    public int missionID;
    public int progressID;
    public GameObject conversationBackgroundPanel;
    public GameObject characterManager;
    public GameObject progressMission;
    public GameObject characterResponseObject;

    public void UseMissionManager() {
        GameObject.Find("MissionManager").GetComponent<Missions>().StartMission(missionID);
    }

    public void StartMission(int ID) {
        switch (ID) {
            case 1:
                StartCoroutine(HuntingMission());
                break;
            case 2:
                break;
        }
    }

    public void SendMissionManagerNewProgress() {
        GameObject.Find("MissionManager").GetComponent<Missions>().UpdateProgress(progressID);
    }

    public void UpdateProgress(int progID) {
        progressID = progID;
    }

    IEnumerator HuntingMission() {

        ConversationManager convoM = GameObject.Find("ConversationManager").GetComponent<ConversationManager>();

        characterResponseObject.GetComponentsInChildren<Text>()[0].text = "Alright. To the woods beyond the archway over there.";
        var charResponse = Instantiate(characterResponseObject);
        charResponse.transform.SetParent(GameObject.Find("Placeholder").transform, false);

        yield return new WaitForSeconds(.01f);
        GameObject.Find("Placeholder").transform.GetChild(0).transform.SetParent(GameObject.Find("ConversationScroll").transform, false);
        convoM.ExpandConversationScroll();

        yield return new WaitForSeconds(3f);
        progressID = -1;

        conversationBackgroundPanel.SetActive(false);
        CharactersManager cM = characterManager.GetComponent<CharactersManager>();
        Character tempChar = cM.GetCharacter(1000);
        tempChar.SetLocation(6);
        tempChar = cM.GetCharacter(1001);
        tempChar.SetLocation(6);
        tempChar = cM.GetCharacter(1002);
        tempChar.SetLocation(6);
        tempChar = cM.GetCharacter(1003);
        tempChar.SetLocation(6);

        SettingManager sM = GameObject.Find("SettingManager").GetComponent<SettingManager>();
        GameObject decisionPanel = GameObject.Find("DecisionPanel");
        PlayerManager pM = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();

        bool huntingMission = true;

        while (huntingMission) {
            
            if(sM.currentRoom == 6 && cM.GetCharacter(1000).GetLocation() == 6) {
                string greeting = "Alright, finally. " + pM.name + ", you go with Akaysha and Ben. Jasmine and I will take left. Create a perimeter of about 1000 feet. Move it!.";
                GameObject.Find("DecisionManager").GetComponent<DecisionManager>().TalkTo(1000, greeting);
                cM.GetCharacter(1000).SetLocation(9);
                cM.GetCharacter(1001).SetLocation(9);
                progressMission.transform.GetChild(2).GetComponent<Text>().text = "Create perimeter";
                progressMission.GetComponent<Missions>().progressID = 0;
                var obj = Instantiate(progressMission);
                obj.transform.SetParent(decisionPanel.transform.GetChild(0).GetChild(0).transform, false);
            }

            if (progressID == -1 && sM.currentRoom == 6) {
                for (int i = 0; i < decisionPanel.transform.GetChild(0).GetChild(0).childCount; i++) {
                    if (decisionPanel.transform.GetChild(0).GetChild(0).GetChild(i).gameObject.name == "ProgressMission(Clone)") {
                        break;
                    }
                    Debug.Log(decisionPanel.transform.GetChild(0).GetChild(0).GetChild(i).gameObject.name);
                    if (i == (decisionPanel.transform.GetChild(0).GetChild(0).childCount - 1)) {
                        progressMission.transform.GetChild(2).GetComponent<Text>().text = "Create perimeter";
                        progressMission.GetComponent<Missions>().progressID = 0;
                        var obj = Instantiate(progressMission);
                        obj.transform.SetParent(decisionPanel.transform.GetChild(0).GetChild(0).transform, false);
                    }
                }
            }

            if (progressID == 0) {
                GameObject.Find("MainText").GetComponent<Text>().text = "You and your companions carefully patrol left, creating a perimeter of 1000 feet. All you can see are trees in all directions. You have not even seen a bit of wildlife. The ground was barron and claimed by only the roots of the tightly packed trees.";
                progressID = 1;
                yield return new WaitForSeconds(10f);
                string greeting = "I'm scared...";
                GameObject.Find("DecisionManager").GetComponent<DecisionManager>().TalkTo(1003, greeting);
                convoM.AddDialogueOption(24, 1003);
                convoM.AddDialogueOption(25, 1003);
                cM.GetCharacter(1003).SetGreeting("What...");
                cM.GetCharacter(1002).SetGreeting("What...");
            }

            /*
            if(GameObject.Find("SettingManager").GetComponent<SettingManager>().GetTime()[1] % 2 == 0) {
                string greeting = "I'm scared...";
                GameObject.Find("DecisionManager").GetComponent<DecisionManager>().TalkTo(1003, greeting);
                GameObject.Find("ConversationManager").GetComponent<ConversationManager>().AddDialogueOption(24, 1003);
                GameObject.Find("ConversationManager").GetComponent<ConversationManager>().AddDialogueOption(25, 1003);
                cM.GetCharacter(1003).SetGreeting("What...");
                cM.GetCharacter(1002).SetGreeting("What...");
            } 
            */

            yield return new WaitForSeconds(.5f);
        }
    }
}
