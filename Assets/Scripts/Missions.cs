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
    public GameObject SpeechAlertObject;

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

        bool notArrived = true;
        string greeting;

        while (notArrived) {
            
            if(sM.currentRoom == 6 && cM.GetCharacter(1000).GetLocation() == 6) {
                greeting = "Alright, finally. " + pM.GetName() + ", you go with Akaysha and Ben. Jasmine and I will take left. Create a perimeter of about 1000 feet. Move it!.";
                GameObject.Find("DecisionManager").GetComponent<DecisionManager>().TalkTo(1000, greeting);
                cM.GetCharacter(1000).SetLocation(9);
                cM.GetCharacter(1001).SetLocation(9);

                notArrived = false;
            }

            yield return new WaitForSeconds(.5f);
        }

        progressMission.transform.GetChild(2).GetComponent<Text>().text = "Create perimeter";
        progressMission.GetComponent<Missions>().progressID = 0;
        var obj = Instantiate(progressMission);
        obj.transform.SetParent(decisionPanel.transform.GetChild(0).GetChild(0).transform, false);

        bool startedPerimeter = false;

        while (startedPerimeter == false) {
            if(progressID == 0) {
                startedPerimeter = true;
                cM.GetCharacter(1002).SetLocation(8);
                cM.GetCharacter(1003).SetLocation(8);
                string text = "You and your companions carefully patrol left, creating a perimeter of 1000 feet. All you can see are trees in all directions. You have not even seen a bit of wildlife. The ground was barron and claimed by only the roots of the tightly packed trees.";
                GameObject.Find("DecisionManager").GetComponent<DecisionManager>().NextRoom(8, text);
            }
            yield return new WaitForSeconds(.5f);
        }

        progressID = 1;
        yield return new WaitForSeconds(15f);

        SpeechAlertObject.GetComponent<SpeechAlertManager>().convoID = 0;
        SpeechAlertObject.GetComponent<SpeechAlertManager>().characterID = 1003;
        SpeechAlertObject.GetComponent<SpeechAlertManager>().text = "I'm scared...";
        SpeechAlertObject.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Ben";
        SpeechAlertObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "I'm scared...";
        var speechObject = Instantiate(SpeechAlertObject);
        speechObject.transform.SetParent(GameObject.Find("SpeechAlertPanel").transform, false);


        cM.GetCharacter(1002).SetGreeting("What...");
        cM.GetCharacter(1003).SetGreeting("What...");

        yield return new WaitForSeconds(20f);

        bool talkedWithAkaysha = false;

        SpeechAlertObject.GetComponent<SpeechAlertManager>().convoID = 2;
        SpeechAlertObject.GetComponent<SpeechAlertManager>().characterID = 1002;
        SpeechAlertObject.GetComponent<SpeechAlertManager>().text = "...";
        SpeechAlertObject.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Akaysha";
        SpeechAlertObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "...";
        speechObject = Instantiate(SpeechAlertObject);
        speechObject.transform.SetParent(GameObject.Find("SpeechAlertPanel").transform, false);
        
        while(talkedWithAkaysha == false) {
            if(cM.GetCharacter(1002).GetConvo() == 0) {
                talkedWithAkaysha = true;
            }
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(30f);

        conversationBackgroundPanel.gameObject.SetActive(false);

        GameObject.Find("MainText").GetComponent<Text>().text = "Then you see it; a deer stood at the top of a hill, barely visible through the trees about 100 feet away. Its ears were perked up and its neck stood straight.";

        yield return new WaitForSeconds(5f);

        SpeechAlertObject.GetComponent<SpeechAlertManager>().convoID = 2;
        SpeechAlertObject.GetComponent<SpeechAlertManager>().characterID = 1002;
        SpeechAlertObject.GetComponent<SpeechAlertManager>().text = "I'm gonna take the shot.";
        SpeechAlertObject.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Akaysha";
        SpeechAlertObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "I'm gonna take the shot.";
        speechObject = Instantiate(SpeechAlertObject);
        speechObject.transform.SetParent(GameObject.Find("SpeechAlertPanel").transform, false);


    }
}
